using System.IO;
using System.Diagnostics;

namespace ITI.Common.Utilities.IO.Streams
{
    /// <summary>
    /// InputStream proxy that transparently writes a copy of all bytes read from the proxied stream to a given OutputStream.
    /// </summary>
    public class TeeInputStream : BaseInputStream
    {
        #region -- Local Variables --
        private readonly Stream input, tee;
        #endregion

        #region -- Constructor --
        public TeeInputStream(Stream input, Stream tee)
        {
            Debug.Assert(input.CanRead);
            Debug.Assert(tee.CanWrite);

            this.input = input;
            this.tee = tee;
        }
        #endregion

        #region -- Overrides --
        public override void Close()
        {
            input.Close();
            tee.Close();
        }

        public override int Read(byte[] buf, int off, int len)
        {
            int i = input.Read(buf, off, len);

            if (i > 0)
            {
                tee.Write(buf, off, i);
            }

            return i;
        }

        public override int ReadByte()
        {
            int i = input.ReadByte();

            if (i >= 0)
            {
                tee.WriteByte((byte)i);
            }

            return i;
        }
        #endregion
    }
}
