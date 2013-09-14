using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ITI.Common.Utilities.ServiceModel.Faults.Logger
{
    [ServiceContract]
    public interface ILogger
    {
        [OperationContract(IsOneWay = true)]
        void LogEntry(Entry entry);

        [OperationContract(IsOneWay = true)]
        void Clear();

        //[OperationContract]
        //Entry[] GetEntries();
    }
}
