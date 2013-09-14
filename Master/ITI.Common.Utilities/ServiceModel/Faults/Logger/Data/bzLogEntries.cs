using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace ITI.Common.Utilities.ServiceModel.Faults.Logger.Data
{
    public class bzLogEntries
    {
        #region -- Local Variables --
        private dmLogEntries m_DataAccess;
        #endregion

        #region -- Constructor(s) --
        public bzLogEntries()
        {
            this.m_DataAccess = new dmLogEntries();
        }
        #endregion

        #region -- Private Methods --
        private IEnumerable<Entry> Convert()
        {
            LogEntries.EntriesDataTable data = this.m_DataAccess.GetAllEntries();

            foreach (LogEntries.EntriesRow row in data.Rows)
            {
                yield return new Entry(row.MachineName, row.HostName, row.EntryDate, row.EntryTime, row.AssemblyName, row.FileName, row.LineNumber, row.TypeName
                    , row.MemberAccessed, row.ExceptionName, row.ExceptionMessage, row.ProvidedFault, row.ProvidedMessage, row.Event);
            }
        }
        #endregion

        #region -- Public Methods --
        public void LogEntry(Entry entry)
        {
            this.m_DataAccess.LogEntry(entry);
        }
        public void DeleteEntries()
        {
            this.m_DataAccess.DeleteEntries();
        }
        public Entry[] GetAllEntries()
        {
            return this.Convert().ToArray();          
        }

        
        #endregion        
    }
}
