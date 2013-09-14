using System;
using System.IO;

namespace ITI.Common.Utilities.IO.Streams
{
    /// <summary>
    /// Overflow stream exception.
    /// </summary>
    public class StreamOverflowException : IOException
    {
        #region -- Constructors --
        public StreamOverflowException()
            : base()
        {
        }

        public StreamOverflowException(
            string message)
            : base(message)
        {
        }

        public StreamOverflowException(
            string message,
            Exception exception)
            : base(message, exception)
        {
        }
        #endregion
    }
}
