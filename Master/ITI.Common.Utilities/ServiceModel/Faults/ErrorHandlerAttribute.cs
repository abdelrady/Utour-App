using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Collections.ObjectModel;
using ITI.Common.Utilities.ServiceModel.Faults.Logger;
using System.Configuration;
using ITI.Common.Utilities.ServiceModel.Faults.Interfaces;

namespace ITI.Common.Utilities.ServiceModel.Faults
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ErrorHandlerBehaviorAttribute : Attribute, IErrorHandler, IServiceBehavior
    {

        ErrorHandlersConfig errorHandlers = null;
        List<ICustomErrorHandler> m_RegisteredHandlers = null;

        protected Type ServiceType
        {
            get;
            set;
        }


        public ErrorHandlerBehaviorAttribute()
        {
            errorHandlers = ConfigurationManager.GetSection("ErrorHandlersConfig") as ErrorHandlersConfig;
            if (errorHandlers != null && errorHandlers.ErrorHandlerCollection.Count > 0)
            {
                m_RegisteredHandlers = new List<ICustomErrorHandler>();
                foreach (Handler handler in errorHandlers.ErrorHandlerCollection)
                {
                    Type t = Type.GetType(handler.Type, true, true);
                    Console.WriteLine(t.FullName);
                    if ((typeof(ICustomErrorHandler).IsAssignableFrom(t)))
                    {
                        m_RegisteredHandlers.Add((ICustomErrorHandler)Activator.CreateInstance(t));
                        Console.WriteLine(t.FullName + " implements interface ICustomeErrorHandler");
                    }
                    else
                    {
                        Console.WriteLine(t.FullName + " doesn't implement interface ICustomeErrorHandler");
                    }
                }
            }
            else
            {
                Console.WriteLine("No registered handlers found !!");
            }
        }


        void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase host)
        {
        }
        void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase host, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
        { }
        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase host)
        {
            ServiceType = description.ServiceType;
            foreach (ChannelDispatcher dispatcher in host.ChannelDispatchers)
            {
                dispatcher.ErrorHandlers.Add(this);
            }
        }

        bool IErrorHandler.HandleError(Exception error)
        {
            //Console.WriteLine(error);
            //ErrorHandlerHelper.LogError(error);
            if (m_RegisteredHandlers != null)
            {
                foreach (ICustomErrorHandler handler in m_RegisteredHandlers)
                {
                    handler.HandleError(error);
                }
            }
            return false;
        }
        void IErrorHandler.ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (m_RegisteredHandlers != null)
            {
                foreach (ICustomErrorHandler handler in m_RegisteredHandlers)
                {
                    handler.ProvideFault(error, version, ref fault);
                }
            }
            //Console.WriteLine(error);
            //ErrorHandlerHelper.PromoteException(ServiceType, error, version, ref fault);
        }
    }
}
