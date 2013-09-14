using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITI.Common.Utilities.ServiceModel.Faults.Interfaces;
using System.Configuration;
using System.Diagnostics;

namespace ITI.Common.Utilities.ServiceModel.Faults
{
    public class EventLogErrorHandler : ICustomErrorHandler
    {
        private static string EventSource = ConfigurationManager.AppSettings["ErrorLogSource"];
        #region ICustomErrorHandler Members

        public void HandleError(Exception error)
        {
            EventLog.WriteEntry(EventSource,error.ToString(),EventLogEntryType.Error);            
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            ErrorHandlerHelper.PromoteException(error,version,ref fault);            
        }

        #endregion
    }
}
