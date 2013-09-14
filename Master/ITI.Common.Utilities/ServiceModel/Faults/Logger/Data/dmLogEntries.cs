using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ITI.Common.Utilities.ServiceModel.Faults.Logger.Data
{
    public partial class dmLogEntries : Component
    {
        #region -- Constructor(s) --
        public dmLogEntries()
        {
            InitializeComponent();
        }

        public dmLogEntries(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #endregion

        #region -- Public Methods --
        public void LogEntry(Entry entry)
        {
            try
            {
                if (entry == null)
                    return;
                LogEntries ds = new LogEntries();
                ds.Entries.AddEntriesRow(entry.MachineName, entry.HostName, entry.AssemblyName, entry.FileName, entry.LineNumber,
                    entry.TypeName, entry.MemberAccessed, entry.Date, entry.Time, entry.ExceptionName, entry.ExceptionMessage,
                    entry.ProvidedFault, entry.ProvidedMessage, entry.Event);

                this.daLogEntries.InsertCommand.Connection = this.sqlConn;
                this.daLogEntries.Update(ds.Entries);
            }
            catch (Exception)
            {

                throw;
            }            
        }
        public void DeleteEntries()
        {
            try
            {
                this.daLogEntries.DeleteCommand.Connection = this.sqlConn;
                this.daLogEntries.DeleteCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }            
        }
        public LogEntries.EntriesDataTable GetAllEntries()
        {
            try
            {
                LogEntries.EntriesDataTable dtResult = new LogEntries.EntriesDataTable();
                this.daLogEntries.SelectCommand.Connection = this.sqlConn;
                this.daLogEntries.Fill(dtResult);
                return dtResult;
            }
            catch (Exception)
            {

                throw;
            }           
        }


        #endregion            
    }
}
