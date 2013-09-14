using System;
using System.Collections;
using System.Reflection;
using System.Timers;


namespace ITI.Common.Utilities.Threading.Scheduler
{
    /// <summary>
    /// Timer job groups a schedule, syncronization data, a result filter, method information and an enabled state so that multiple jobs
    /// can be managed by the same timer.  Each one operating independently of the others with different syncronization and recovery settings.
    /// </summary>
    public class TimerJob
    {
        #region -- Local Varaibles --
        private ExecuteHandler m_ExecuteHandler;
        #endregion

        #region -- Global Variables --
        public IScheduledItem Schedule;
        public bool SyncronizedEvent = true;
        public IResultFilter Filter;
        public IMethodCall Method;
        public bool Enabled = true;
        #endregion

        #region -- Delegates --

        private delegate void ExecuteHandler(object sender, DateTime EventTime, ExceptionEventHandler Error);

        #endregion

        #region -- Constructor (s) --
        public TimerJob(IScheduledItem schedule, IMethodCall method)
        {
            Schedule = schedule;
            Method = method;
            m_ExecuteHandler = new ExecuteHandler(ExecuteInternal);
        }
        #endregion

        #region -- Public Methods --

        public DateTime NextRunTime(DateTime time, bool IncludeStartTime)
        {
            if (!Enabled)
                return DateTime.MaxValue;
            return Schedule.NextRunTime(time, IncludeStartTime);
        }

        public void Execute(object sender, DateTime Begin, DateTime End, ExceptionEventHandler Error)
        {
            if (!Enabled)
                return;

            ArrayList EventList = new ArrayList();
            Schedule.AddEventsInInterval(Begin, End, EventList);

            if (Filter != null)
                Filter.FilterResultsInInterval(Begin, End, EventList);

            foreach (DateTime EventTime in EventList)
            {
                if (SyncronizedEvent)
                    m_ExecuteHandler(sender, EventTime, Error);
                else
                    m_ExecuteHandler.BeginInvoke(sender, EventTime, Error, null, null);
            }
        }

        private void ExecuteInternal(object sender, DateTime EventTime, ExceptionEventHandler Error)
        {
            try
            {
                TimerParameterSetter Setter = new TimerParameterSetter(EventTime, sender);
                Method.Execute(Setter);
            }
            catch (Exception ex)
            {
                if (Error != null)
                    try { Error(this, new ExceptionEventArgs(EventTime, ex)); }
                    catch { }
            }
        }

        #endregion
    }

    /// <summary>
    /// Timer job manages a group of timer jobs.
    /// </summary>
    public class TimerJobList
    {
        #region -- Local Variables --
        private ArrayList _List;
        #endregion

        #region -- Global Varaibles --
        public TimerJob[] Jobs
        {
            get { return (TimerJob[])_List.ToArray(typeof(TimerJob)); }
        }
        #endregion

        #region -- Constructor (s) --
        public TimerJobList()
        {
            _List = new ArrayList();
        }
        #endregion

        #region -- Public Methods --

        public void Add(TimerJob Event)
        {
            _List.Add(Event);
        }

        public void Clear()
        {
            _List.Clear();
        }

        /// <summary>
        /// Gets the next time any of the jobs in the list will run.  Allows matching the exact start time.  If no matches are found the return
        /// is DateTime.MaxValue;
        /// </summary>
        /// <param name="time">The starting time for the interval being queried.  This time is included in the interval</param>
        /// <returns>The first absolute date one of the jobs will execute on.  If none of the jobs needs to run DateTime.MaxValue is returned.</returns>
        public DateTime NextRunTime(DateTime time)
        {
            DateTime next = DateTime.MaxValue;
            //Get minimum datetime from the list.
            foreach (TimerJob Job in _List)
            {
                DateTime Proposed = Job.NextRunTime(time, true);
                next = (Proposed < next) ? Proposed : next;
            }
            return next;
        }
        #endregion
    }

    /// <summary>
    /// The timer job allows delegates to be specified with unbound parameters.  This ParameterSetter assigns all unbound datetime parameters
    /// with the specified time and all unbound object parameters with the calling object.
    /// </summary>
    public class TimerParameterSetter : IParameterSetter
    {
        #region -- Local Variables --
        DateTime m_time;
        object m_sender;
        #endregion

        #region -- Constructor (s) --
        /// <summary>
        /// Initalize the ParameterSetter with the time to pass to unbound time parameters and object to pass to unbound object parameters.
        /// </summary>
        /// <param name="time">The time to pass to the unbound DateTime parameters</param>
        /// <param name="sender">The object to pass to the unbound object parameters</param>
        public TimerParameterSetter(DateTime time, object sender)
        {
            m_time = time;
            m_sender = sender;
        }
        #endregion

        #region -- Public Methods --
        public void reset()
        {
        }
        public bool GetParameterValue(ParameterInfo pi, int ParameterLoc, ref object parameter)
        {
            switch (pi.ParameterType.Name.ToLower())
            {
                case "datetime":
                    parameter = m_time;
                    return true;
                case "object":
                    parameter = m_sender;
                    return true;
                case "scheduledeventargs":
                    parameter = new ScheduledEventArgs(m_time);
                    return true;
                case "eventargs":
                    parameter = new ScheduledEventArgs(m_time);
                    return true;
            }
            return false;
        }
        #endregion
        
    }
}
