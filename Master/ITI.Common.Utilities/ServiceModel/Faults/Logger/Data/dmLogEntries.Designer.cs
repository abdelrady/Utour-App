namespace ITI.Common.Utilities.ServiceModel.Faults.Logger.Data
{
    partial class dmLogEntries
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dmLogEntries));
            this.daLogEntries = new System.Data.SqlClient.SqlDataAdapter();
            this.cmdDeleteEntry = new System.Data.SqlClient.SqlCommand();
            this.sqlConn = new System.Data.SqlClient.SqlConnection();
            this.cmdInsertEntry = new System.Data.SqlClient.SqlCommand();
            this.cmdSelectAllEntries = new System.Data.SqlClient.SqlCommand();
            // 
            // daLogEntries
            // 
            this.daLogEntries.DeleteCommand = this.cmdDeleteEntry;
            this.daLogEntries.InsertCommand = this.cmdInsertEntry;
            this.daLogEntries.SelectCommand = this.cmdSelectAllEntries;
            this.daLogEntries.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "", new System.Data.Common.DataColumnMapping[0])});
            // 
            // cmdDeleteEntry
            // 
            this.cmdDeleteEntry.CommandText = "DELETE FROM Entries";
            this.cmdDeleteEntry.Connection = this.sqlConn;
            // 
            // sqlConn
            // 
            this.sqlConn.FireInfoMessageEventOnUserErrors = false;
            // 
            // cmdInsertEntry
            // 
            this.cmdInsertEntry.CommandText = resources.GetString("cmdInsertEntry.CommandText");
            this.cmdInsertEntry.Connection = this.sqlConn;
            this.cmdInsertEntry.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@MachineName", System.Data.SqlDbType.VarChar, 50, "MachineName"),
            new System.Data.SqlClient.SqlParameter("@HostName", System.Data.SqlDbType.VarChar, 50, "HostName"),
            new System.Data.SqlClient.SqlParameter("@AssemblyName", System.Data.SqlDbType.VarChar, 50, "AssemblyName"),
            new System.Data.SqlClient.SqlParameter("@FileName", System.Data.SqlDbType.VarChar, 50, "FileName"),
            new System.Data.SqlClient.SqlParameter("@LineNumber", System.Data.SqlDbType.Int, 4, "LineNumber"),
            new System.Data.SqlClient.SqlParameter("@TypeName", System.Data.SqlDbType.VarChar, 50, "TypeName"),
            new System.Data.SqlClient.SqlParameter("@MemberAccessed", System.Data.SqlDbType.VarChar, 50, "MemberAccessed"),
            new System.Data.SqlClient.SqlParameter("@EntryDate", System.Data.SqlDbType.VarChar, 50, "EntryDate"),
            new System.Data.SqlClient.SqlParameter("@EntryTime", System.Data.SqlDbType.VarChar, 50, "EntryTime"),
            new System.Data.SqlClient.SqlParameter("@ExceptionName", System.Data.SqlDbType.VarChar, 100, "ExceptionName"),
            new System.Data.SqlClient.SqlParameter("@ExceptionMessage", System.Data.SqlDbType.VarChar, 300, "ExceptionMessage"),
            new System.Data.SqlClient.SqlParameter("@ProvidedFault", System.Data.SqlDbType.VarChar, 100, "ProvidedFault"),
            new System.Data.SqlClient.SqlParameter("@ProvidedMessage", System.Data.SqlDbType.VarChar, 100, "ProvidedMessage"),
            new System.Data.SqlClient.SqlParameter("@Event", System.Data.SqlDbType.VarChar, 100, "Event")});
            // 
            // cmdSelectAllEntries
            // 
            this.cmdSelectAllEntries.CommandText = resources.GetString("cmdSelectAllEntries.CommandText");
            this.cmdSelectAllEntries.Connection = this.sqlConn;

        }

        #endregion

        private System.Data.SqlClient.SqlDataAdapter daLogEntries;
        private System.Data.SqlClient.SqlCommand cmdDeleteEntry;
        private System.Data.SqlClient.SqlConnection sqlConn;
        private System.Data.SqlClient.SqlCommand cmdInsertEntry;
        private System.Data.SqlClient.SqlCommand cmdSelectAllEntries;
    }
}
