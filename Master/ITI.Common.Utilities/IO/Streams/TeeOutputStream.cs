using System.Diagnostics;
using System.IO;

namespace ITI.Common.Utilities.IO.Streams
{
    /// <summary>
    ///  Named after the unix 'tee' command. It allows a stream to be branched off so there are now two streams.
    /// </summary>
    public class TeeOutputStream : BaseOutputStream
    {
        #region -- Local Variables --
        private readonly Stream output, tee;
        #endregion

        #region -- Constructor --
        public TeeOutputStream(Stream output, Stream tee)
        {
            Debug.Assert(output.CanWrite);
            Debug.Assert(tee.CanWrite);

            this.output = output;
            this.tee = tee;
        }
        #endregion

        #region -- Overrides --
        public override void Close()
        {
            output.Close();
            tee.Close();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            output.Write(buffer, offset, count);
            tee.Write(buffer, offset, count);
        }

        public override void WriteByte(byte b)
        {
            output.WriteByte(b);
            tee.WriteByte(b);
        }
        #endregion
    }
}
