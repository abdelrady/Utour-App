using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ITI.Common.Utilities.Threading.Scheduler
{
    #region -- Delegates --
    delegate object Exec();
    delegate object Exec2(IParameterSetter Params);
    #endregion

    #region -- Interfaces --
    /// <summary>
    /// IParameterSetter represents a serialized parameter list.  This is used to provide a partial specialized
    /// method call.  This is useful for remote invocation of method calls.  For example if you have a method with
    /// 3 parameters.  The first 2 might represent static data such as a report and a storage location.  The third
    /// might be the time that the report is invoked, which is only known when the method is invoked.  Using this,
    /// you just pass the method and the first 2 parameters to a timer object, which supplies the 3rd parameter.
    /// Without these objects, you would have to generate a custom object type for each method you wished to 
    /// execute in this manner and store the static parameters as instance variables.  
    /// </summary>
    public interface IParameterSetter
    {
        #region -- Methods --
        /// <summary>
        /// This resets the setter to the beginning.  It is used for setters that rely on positional state
        /// information.  It is called prior to setting any method values.
        /// </summary>
        void reset();
        /// <summary>
        /// This method is used to both query support for setting a parameter and actually set the value.
        /// True is returned if the parameter passed in is updated.
        /// </summary>
        /// <param name="pi">The reflection information about this parameter.</param>
        /// <param name="ParameterLoc">The location of the prameter in the parameter list.</param>
        /// <param name="parameter">The parameter object</param>
        /// <returns>true if the parameter is matched and false otherwise</returns>
        bool GetParameterValue(ParameterInfo pi, int ParameterLoc, ref object parameter);
        #endregion
    }

    /// <summary>
    /// IMethodCall represents a partially specified parameter data list and a method.  This allows methods to be 
    /// dynamically late invoked for things like timers and other event driven frameworks.
    /// </summary>
    public interface IMethodCall
    {
        #region -- Properties --
        ParameterSetterList ParamList { get; }
        #endregion

        #region -- Public Methods --
        object Execute();
        object Execute(IParameterSetter Params);
        void EventHandler(object obj, EventArgs e);
        IAsyncResult BeginExecute(AsyncCallback callback, object obj);
        IAsyncResult BeginExecute(IParameterSetter Params, AsyncCallback callback, object obj);
        #endregion
    }
    #endregion

    #region -- Classes --

    /// <summary>
    /// This setter object takes a simple object array full of parameter data.  It applys the objects in order
    /// to the method parameter list.
    /// </summary>
    public class OrderParameterSetter : IParameterSetter
    {
        #region -- Local Varaibles --
        private object[] m_ParamList;
        private int m_Counter;
        #endregion 

        #region -- Constructor (s) --
        public OrderParameterSetter(params object[] _Params)
        {
            m_ParamList = _Params;
        }
        #endregion

        #region -- Public Methods --
        public void reset()
        {
            m_Counter = 0;
        }
        public bool GetParameterValue(ParameterInfo pi, int ParameterLoc, ref object parameter)
        {
            if (m_Counter >= m_ParamList.Length)
                return false;
            parameter = m_ParamList[m_Counter++];
            return true;
        }
        #endregion


    }

    /// <summary>
    /// This setter object stores the parameter data in a Hashtable and uses the hashtable keys to match 
    /// the parameter names of the method to the parameter data.  This allows methods to be called like 
    /// stored procedures, with the parameters being passed in independent of order.
    /// </summary>
    public class NamedParameterSetter : IParameterSetter
    {
        #region -- Local Varaibles --
        private Hashtable _Params;
        #endregion

        #region -- Constructor (s) --
        public NamedParameterSetter(Hashtable Params)
        {
            _Params = Params;
        }
        #endregion

        #region -- Public Methods --
        public void reset()
        {
        }
        public bool GetParameterValue(ParameterInfo pi, int ParameterLoc, ref object parameter)
        {
            string ParamName = pi.Name;
            if (!_Params.ContainsKey(ParamName))
                return false;
            parameter = _Params[ParamName];
            return true;
        }
        #endregion

    }

    /// <summary>
    /// ParameterSetterList maintains a collection of IParameterSetter objects and applies them in order to each
    /// parameter of the method.  Each time a match occurs the next parameter is tried starting with the first 
    /// setter object until it is matched.
    /// </summary>
    public class ParameterSetterList
    {
        #region -- Local Varaibles --
        private ArrayList _List = new ArrayList();
        #endregion

        #region -- Public Methods --
        public void Add(IParameterSetter setter)
        {
            _List.Add(setter);
        }

        public IParameterSetter[] ToArray()
        {
            return (IParameterSetter[])_List.ToArray(typeof(IParameterSetter));
        }

        public void reset()
        {
            foreach (IParameterSetter Setter in _List)
                Setter.reset();
        }

        public object[] GetParameters(MethodInfo Method)
        {
            ParameterInfo[] Params = Method.GetParameters();
            object[] Values = new object[Params.Length];
            //TODO: Update to iterate backwards
            for (int i = 0; i < Params.Length; ++i)
                SetValue(Params[i], i, ref Values[i]);

            return Values;
        }

        public object[] GetParameters(MethodInfo Method, IParameterSetter LastSetter)
        {
            ParameterInfo[] Params = Method.GetParameters();
            object[] Values = new object[Params.Length];
            //TODO: Update to iterate backwards
            for (int i = 0; i < Params.Length; ++i)
            {
                if (!SetValue(Params[i], i, ref Values[i]))
                    LastSetter.GetParameterValue(Params[i], i, ref Values[i]);
            }
            return Values;
        }

        bool SetValue(ParameterInfo Info, int i, ref object Value)
        {
            foreach (IParameterSetter Setter in _List)
            {
                if (Setter.GetParameterValue(Info, i, ref Value))
                    return true;
            }
            return false;
        }
        #endregion
    }

    /// <summary>
    /// Method call captures the data required to do a defered method call.
    /// </summary>
    public class DelegateMethodCall : MethodCallBase, IMethodCall
    {
        #region -- Local Varaibles --
        Delegate m_f;
        Exec m_exec;
        #endregion

        #region -- Properties --
        public Delegate f
        {
            get { return m_f; }
            set { m_f = value; }
        }

        public MethodInfo Method
        {
            get { return m_f.Method; }
        }

        #endregion

        #region -- Constructor (s) --
        public DelegateMethodCall(Delegate f)
        {
            m_f = f;
        }

        public DelegateMethodCall(Delegate f, params object[] Params)
        {
            if (f.Method.GetParameters().Length < Params.Length)
                throw new ArgumentException("Too many parameters specified for delegate", "f");

            m_f = f;
            ParamList.Add(new OrderParameterSetter(Params));
        }

        public DelegateMethodCall(Delegate f, IParameterSetter Params)
        {
            m_f = f;
            ParamList.Add(Params);
        }
        #endregion

        #region -- Public Methods --

        public object Execute()
        {
            return f.DynamicInvoke(GetParameterList(Method));
        }

        public object Execute(IParameterSetter Params)
        {
            return f.DynamicInvoke(GetParameterList(Method, Params));
        }

        public void EventHandler(object obj, EventArgs e)
        {
            Execute();
        }

        public IAsyncResult BeginExecute(AsyncCallback callback, object obj)
        {
            m_exec = new Exec(Execute);
            return m_exec.BeginInvoke(callback, obj);
        }

        public IAsyncResult BeginExecute(IParameterSetter Params, AsyncCallback callback, object obj)
        {
            Exec2 exec = new Exec2(Execute);
            return exec.BeginInvoke(Params, callback, obj);
        }

        #endregion
    }

    public class DynamicMethodCall : MethodCallBase, IMethodCall
    {
        #region -- Local Varaibles --
        object m_obj;
        MethodInfo m_method;
        Exec m_exec;
        #endregion

        #region -- Properties --
        public MethodInfo Method
        {
            get { return m_method; }
            set { m_method = value; }
        }
        #endregion

        #region -- Constructor (s) --
        public DynamicMethodCall(MethodInfo method)
        {
            m_obj = null;
            m_method = method;
        }

        public DynamicMethodCall(object obj, MethodInfo method)
        {
            m_obj = obj;
            m_method = method;
        }

        public DynamicMethodCall(object obj, MethodInfo method, IParameterSetter setter)
        {
            m_obj = obj;
            m_method = method;
            ParamList.Add(setter);
        }

        #endregion

        #region -- Public Methods --
        public object Execute()
        {
            return m_method.Invoke(m_obj, GetParameterList(Method));
        }

        public object Execute(IParameterSetter Params)
        {
            return m_method.Invoke(m_obj, GetParameterList(Method, Params));
        }

        public void EventHandler(object obj, EventArgs e)
        {
            Execute();
        }


        public IAsyncResult BeginExecute(AsyncCallback callback, object obj)
        {
            m_exec = new Exec(Execute);
            return m_exec.BeginInvoke(callback, null);
        }

        public IAsyncResult BeginExecute(IParameterSetter Params, AsyncCallback callback, object obj)
        {
            Exec2 exec = new Exec2(Execute);
            return exec.BeginInvoke(Params, callback, null);
        }
        #endregion
    }

    /// <summary>
    /// This is a base class that handles the Parameter list management for the 2 dynamic method call methods.
    /// </summary>
    public class MethodCallBase
    {
        #region -- Local Varaibles --
        ParameterSetterList _ParamList = new ParameterSetterList();
        #endregion

        #region -- Properties --
        public ParameterSetterList ParamList
        {
            get { return _ParamList; }
        }
        #endregion

        #region -- Protected Methods --

        protected object[] GetParameterList(MethodInfo Method)
        {
            ParamList.reset();
            object[] Params = ParamList.GetParameters(Method);
            return Params;
        }

        protected object[] GetParameterList(MethodInfo Method, IParameterSetter Params)
        {
            ParamList.reset();
            object[] objParams = ParamList.GetParameters(Method, Params);
            return objParams;
        }

        #endregion
    }
    #endregion
}
