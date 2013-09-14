using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ITI.Common.Utilities.ServiceModel.Faults.Logger.Data;

namespace ITI.Common.Utilities.ServiceModel.Faults.Logger
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class LogbookManager : ILogger
    {
        [OperationBehavior(TransactionScopeRequired = true)]
        public void LogEntry(Entry entry)
        {
            new bzLogEntries().LogEntry(entry);
        }
        [OperationBehavior(TransactionScopeRequired = true)]
        public void Clear()
        {
            new bzLogEntries().DeleteEntries();
        }
        [OperationBehavior(TransactionScopeRequired = true)]
        public Entry[] GetEntries()
        {
            return new bzLogEntries().GetAllEntries();
        }
    }
}
