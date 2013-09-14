using System;
using System.Xml;
using System.Xml.XPath;

namespace ITI.Common.Utilities.Threading.Scheduler
{
    /// <summary>
    /// Null event strorage disables error recovery by returning now for the last time an event fired.
    /// </summary>
    public class NullEventStorage : IEventStorage
    {
        #region -- Constructor (s) --
        public NullEventStorage()
        {
        }
        #endregion

        #region -- Public Methods --

        public void RecordLastTime(DateTime Time)
        {
        }

        public DateTime ReadLastTime()
        {
            return DateTime.Now;
        }

        #endregion
    }

    /// <summary>
    /// Local event strorage keeps the last time in memory so that skipped events are not recovered.
    /// </summary>
    public class LocalEventStorage : IEventStorage
    {
        #region -- Local Variables --
        private DateTime m_LastTime;
        #endregion

        #region -- Constructor (s) --
        public LocalEventStorage()
        {
            m_LastTime = DateTime.MaxValue;
        }
        #endregion

        #region -- Public Methods --

        public void RecordLastTime(DateTime Time)
        {
            m_LastTime = Time;
        }

        public DateTime ReadLastTime()
        {
            if (m_LastTime == DateTime.MaxValue)
                m_LastTime = DateTime.Now;
            return m_LastTime;
        }

        #endregion
    }

    /// <summary>
    /// FileEventStorage saves the last time in an XmlDocument so that recovery will include periods that the 
    /// process is shutdown.
    /// </summary>
    public class FileEventStorage : IEventStorage
    {
        #region -- Local Variables --
        private string _FileName;
        private string _XPath;
        private XmlDocument _Doc = new XmlDocument();
        #endregion

        #region -- Constructor (s) --
        public FileEventStorage(string FileName, string XPath)
        {
            _FileName = FileName;
            _XPath = XPath;
        }
        #endregion

        #region -- Public Methods --

        public void RecordLastTime(DateTime Time)
        {
            _Doc.SelectSingleNode(_XPath).Value = Time.ToString();
            _Doc.Save(_FileName);
        }

        public DateTime ReadLastTime()
        {
            _Doc.Load(_FileName);
            string Value = _Doc.SelectSingleNode(_XPath).Value;
            if (Value == null || Value == string.Empty)
                return DateTime.Now;
            return DateTime.Parse(Value);
        }

        #endregion
    }
}
