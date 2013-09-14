using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ITI.Common.Utilities.Threading
{
    /// <summary>
    /// Module which runs a specific process on it's own thread
    /// Developed by Ramy El-Zawahry
    /// </summary>
    public abstract class ProcessBase
    {
        #region Declarations

        private bool m_IsStopped = true; public bool IsStopped { get { return m_IsStopped; } }
        private Thread m_process = null; public Thread Worker { get { return m_process; } }

        #endregion Declarations

        #region Constructors

        public ProcessBase() { }

        #endregion Constructors

        #region Private Methods

        private Thread InitiateProcess()
        {
            Thread t = new Thread(new ThreadStart(DoProcess)); t.Start();
            return t;
        }

        #endregion Private Methods

        #region Protected Methods
        /// <summary>
        /// override initializations
        /// </summary>
        protected abstract void Initialize();
        /// <summary>
        /// override dispose to remove non more usable objects from memory
        /// </summary>
        protected abstract void Dispose();
        /// <summary>
        /// override the actual action of the process
        /// </summary>
        protected abstract void DoProcess();

        #endregion Protected Methods

        #region public Methods

        /// <summary>
        /// Start the process
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            try
            {
                if (m_IsStopped)
                {
                    Initialize();
                    this.m_process = InitiateProcess();
                    this.m_IsStopped = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // log error
                this.m_IsStopped = true;
                return false;
            }
        }

        public bool Restart()
        {
            Stop();
            return this.Start();
        }

        public void Stop()
        {
            try
            {
                if (!this.m_IsStopped)
                {
                    this.Dispose();
                    this.m_process.Abort();
                    this.m_process = null;
                }
            }
            catch (Exception ex)
            {
                // log error

            }
        }

        #endregion public Methods

    }
    
}
