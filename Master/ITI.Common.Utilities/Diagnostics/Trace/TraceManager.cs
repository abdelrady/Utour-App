using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;



namespace ITI.Common.Utilities.Diagnostics.Trace
{
    /// <summary>
    /// Trace helper for application's logging
    /// </summary>
    public sealed class TraceManager
        : ITraceManager
    {
        #region -- Local Varaibles --

        private TraceSource m_Source;

        #endregion

        #region  -- Constructor --

        /// <summary>
        /// Create a new instance of this trace manager
        /// </summary>
        public TraceManager()
        {
            // Create default source
            m_Source = new TraceSource("ITI.Common.DefaultTrace");
        }

        #endregion

        #region -- Private Methods --

        /// <summary>
        /// Trace internal message in configured listeners
        /// </summary>
        /// <param name="eventType">Event type to trace</param>
        /// <param name="message">Message of event</param>
        void TraceInternal(TraceEventType eventType, string message)
        {
            if (m_Source != null)
            {
                try
                {
                    m_Source.TraceEvent(eventType, (int)eventType, message);
                }
                catch (SecurityException)
                {
                    //Cannot access to file listener or cannot have
                    //privileges to write in event log
                    //do not propagete this :-(
                }
            }
        }
        #endregion

        #region -- Public Methods --

        /// <summary>
        /// <see cref="ITI.Common.Utilities.Diagnostics.Trace.ITraceManager"/>
        /// </summary>
        /// <param name="operationName"><see cref="ITI.Common.Utilities.Diagnostics.Trace.ITraceManager"/></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2135:SecurityRuleSetLevel2MethodsShouldNotBeProtectedWithLinkDemandsFxCopRule"),
        SecurityPermission(SecurityAction.LinkDemand)]
        public void TraceStartLogicalOperation(string operationName)
        {
            if (String.IsNullOrEmpty(operationName))
                throw new ArgumentNullException("operationName", Messages.exception_InvalidTraceMessage);

            System.Diagnostics.Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            System.Diagnostics.Trace.CorrelationManager.StartLogicalOperation(operationName);
        }

        /// <summary>
        /// <see cref="ITI.Common.Utilities.Diagnostics.Trace.ITraceManager"/>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2135:SecurityRuleSetLevel2MethodsShouldNotBeProtectedWithLinkDemandsFxCopRule"),
        SecurityPermission(SecurityAction.LinkDemand)]
        public void TraceStopLogicalOperation()
        {
            try
            {
                System.Diagnostics.Trace.CorrelationManager.StopLogicalOperation();
            }
            catch (InvalidOperationException)
            {
                //stack empty
            }
        }
        /// <summary>
        /// <see cref="ITI.Common.Utilities.Diagnostics.Trace.ITraceManager"/>
        /// </summary>
        public void TraceStart()
        {
            TraceInternal(TraceEventType.Start, ITI.Common.Utilities.Diagnostics.Trace.Messages.constant_START);
        }
        /// <summary>
        ///<see cref="ITI.Common.Utilities.Diagnostics.Trace.ITraceManager"/>
        /// </summary>
        public void TraceStop()
        {

            TraceInternal(TraceEventType.Start, ITI.Common.Utilities.Diagnostics.Trace.Messages.constant_STOP);

        }
        /// <summary>
        /// <see cref="ITI.Common.Utilities.Diagnostics.Trace.ITraceManager"/>
        /// </summary>
        /// <param name="message"><see cref="ITI.Common.Utilities.Diagnostics.Trace.ITraceManager"/></param>
        public void TraceInfo(string message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException("message", Messages.exception_InvalidTraceMessage);

            TraceInternal(TraceEventType.Information, message);

        }
        /// <summary>
        /// <see cref="ITI.Common.Utilities.Diagnostics.Trace.ITraceManager"/>
        /// </summary>
        /// <param name="message"><see cref="ITI.Common.Utilities.Diagnostics.Trace.ITraceManager"/></param>
        public void TraceWarning(string message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException("message", Messages.exception_InvalidTraceMessage);

            TraceInternal(TraceEventType.Warning, message);

        }
        /// <summary>
        /// <see cref="ITI.Common.Utilities.Diagnostics.Trace.ITraceManager"/>
        /// </summary>
        /// <param name="message"><see cref="ITI.Common.Utilities.Diagnostics.Trace.ITraceManager"/></param>
        public void TraceError(string message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException("message", Messages.exception_InvalidTraceMessage);

            TraceInternal(TraceEventType.Error, message);

        }
        /// <summary>
        /// <see cref="ITI.Common.Utilities.Diagnostics.Trace.ITraceManager"/>
        /// </summary>
        /// <param name="message"><see cref="ITI.Common.Utilities.Diagnostics.Trace.ITraceManager"/></param>
        public void TraceCritical(string message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException("message", Messages.exception_InvalidTraceMessage);

            TraceInternal(TraceEventType.Critical, message);
        }

        #endregion

    }
}
