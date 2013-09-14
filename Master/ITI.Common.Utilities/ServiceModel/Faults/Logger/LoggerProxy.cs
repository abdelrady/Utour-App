using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ITI.Common.Utilities.ServiceModel.Faults.Logger
{
    public class LoggerProxy : ClientBase<ILogger>, ILogger
    {
        public LoggerProxy()
        { }

        public LoggerProxy(string endpointConfigurationName)
            : base(endpointConfigurationName)
        { }

        public LoggerProxy(string endpointConfigurationName, string remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        { }

        public LoggerProxy(string endpointConfigurationName, EndpointAddress remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        { }

        public LoggerProxy(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        { }

        public void LogEntry(Entry entry)
        {
            this.Channel.LogEntry(entry);
        }

        public void Clear()
        {
            this.Channel.Clear();
        }

        //public Entry[] GetEntries()
        //{
        //    return this.Channel.GetEntries();
        //}
    }
}
