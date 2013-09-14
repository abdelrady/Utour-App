using System;
using System.IO;

namespace ITI.Common.Utilities.IO.Streams
{
    /// <summary>
    /// Push Back Stream.
    /// </summary>
    public class PushbackStream : FilterStream
    {
        #region -- Local Variables --
        private int buf = -1;
        #endregion

        #region -- Constructors --
        public PushbackStream(Stream s)
            : base(s)
        {
        }
        #endregion

        #region -- Overrides --
        public override int ReadByte()
        {
            if (buf != -1)
            {
                int tmp = buf;
                buf = -1;
                return tmp;
            }

            return base.ReadByte();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buf != -1 && count > 0)
            {
                // TODO Can this case be made more efficient?
                buffer[offset] = (byte)buf;
                buf = -1;
                return 1;
            }

            return base.Read(buffer, offset, count);
        }
        #endregion

        #region -- Virtuals --
        public virtual void Unread(int b)
        {
            if (buf != -1)
                throw new InvalidOperationException("Can only push back one byte");

            buf = b & 0xFF;
        }
        #endregion
    }
}
