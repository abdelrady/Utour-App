using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using System.Diagnostics;

namespace ITI.Common.Utilities.ServiceModel.Faults.Logger
{
    public class Entry
    {
        #region -- Local Variables --
        [DataMember]
        private readonly string m_MachineName;
        [DataMember]
        private readonly string m_HostName;
        [DataMember]
        private readonly string m_AssemblyName;
        [DataMember]
        private readonly string m_FileName;
        [DataMember]
        private readonly int m_LineNumber;
        [DataMember]
        private readonly string m_TypeName;
        [DataMember]
        private readonly string m_MemberAccessed;
        [DataMember]
        private readonly string m_Date;
        [DataMember]
        private readonly string m_Time;
        [DataMember]
        private readonly string m_ExceptionName;
        [DataMember]
        private readonly string m_ExceptionMessage;
        [DataMember]
        private readonly string m_ProvidedFault;
        [DataMember]
        private readonly string m_ProvidedMessage;
        [DataMember]
        private readonly string m_Event;
        #endregion

        #region -- Properties --
        public string MachineName
        {
            get { return m_MachineName; }
        }
        public string HostName
        {
            get { return m_HostName; }
        }
        public string AssemblyName
        {
            get { return m_AssemblyName; }
        }
        public string FileName
        {
            get { return m_FileName; }
        }
        public int LineNumber
        {
            get { return m_LineNumber; }
        }
        public string TypeName
        {
            get { return m_TypeName; }
        }
        public string MemberAccessed
        {
            get { return m_MemberAccessed; }
        }
        public string Date
        {
            get { return m_Date; }
        }
        public string Time
        {
            get { return m_Time; }
        }
        public string ExceptionName
        {
            get { return m_ExceptionName; }
        }
        public string ExceptionMessage
        {
            get { return m_ExceptionMessage; }
        }
        public string ProvidedFault
        {
            get { return m_ProvidedFault; }
        }
        public string ProvidedMessage
        {
            get { return m_ProvidedMessage; }
        }
        public string Event
        {
            get { return m_Event; }
        }
        #endregion

        #region -- Constructor(s) --
        public Entry(string assemblyName, string fileName, int lineNumber, string typeName, string methodName, string exceptionName, string exceptionMessage, string providedFault, string providedMessage, string eventDescription)
            : this(assemblyName, fileName, lineNumber, typeName, methodName, exceptionName, exceptionMessage, providedFault, providedMessage)
        {
            m_Event = eventDescription;
        }
        public Entry(string assemblyName, string fileName, int lineNumber, string typeName, string methodName, string exceptionName, string exceptionMessage)
            : this(assemblyName, fileName, lineNumber, typeName, methodName, exceptionName, exceptionMessage, String.Empty, String.Empty)
        { }
        public Entry(string machineName, string hostName, string date, string time, string assemblyName, string fileName, int lineNumber, string typeName, string methodName, string exceptionName, string exceptionMessage, string providedFault, string providedMessage, string eventDescription)
            : this(assemblyName, fileName, lineNumber, typeName, methodName, exceptionName, exceptionMessage, providedFault, providedMessage, eventDescription)
        {
            m_MachineName = machineName;
            m_HostName = hostName;
            m_Date = date;
            m_Time = time;
            m_Event = eventDescription;
        }
        public Entry(string assemblyName, string fileName, int lineNumber, string typeName, string methodName, string exceptionName, string exceptionMessage, string providedFault, string providedMessage)
        {
            m_MachineName = Environment.MachineName;
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly == null)
            {
                m_HostName = Process.GetCurrentProcess().MainModule.ModuleName;
            }
            else
            {
                m_HostName = entryAssembly.GetName().Name;
            }
            m_AssemblyName = assemblyName;
            m_FileName = fileName;
            m_LineNumber = lineNumber;
            m_TypeName = typeName;
            m_MemberAccessed = methodName;
            m_Date = DateTime.Now.ToShortDateString();
            m_Time = DateTime.Now.ToLongTimeString();
            m_ExceptionName = exceptionName;
            m_ExceptionMessage = exceptionMessage;
            m_ProvidedFault = providedFault;
            m_ProvidedMessage = providedMessage;
            m_Event = String.Empty;
        }
        #endregion
    }
}
