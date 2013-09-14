
namespace ITI.Common.Utilities.IO.Compressions.Zlib
{
    /// <summary>
    /// Adler-32 is a checksum algorithm.    
    /// </summary>
    /// <remarks>
    /// This program is based on zlib-1.1.3, so all credit should go authors
    /// Jean-loup Gailly(jloup@gzip.org) and Mark Adler(madler@alumni.caltech.edu)
    /// and contributors of zlib.
    /// </remarks>
    internal sealed class Adler32
    {
        #region -- Constants --
        /// <summary>
        /// largest prime smaller than 65536
        /// </summary>
        private const int BASE = 65521;
        /// <summary>
        ///  NMAX is the largest n such that 255n(n+1)/2 + (n+1)(BASE-1) <= 2^32-1
        /// </summary>
        private const int NMAX = 5552;
        #endregion

        #region -- Internal Methods --
        /// <summary>
        /// Adler-32 checksum algorithm implementation.
        /// </summary>
        /// <param name="adler"></param>
        /// <param name="buf"></param>
        /// <param name="index"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        internal long adler32(long adler, byte[] buf, int index, int len)
        {
            if (buf == null) { return 1L; }

            long s1 = adler & 0xffff;
            long s2 = (adler >> 16) & 0xffff;
            int k;

            while (len > 0)
            {
                k = len < NMAX ? len : NMAX;
                len -= k;
                while (k >= 16)
                {
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    s1 += buf[index++] & 0xff; s2 += s1;
                    k -= 16;
                }
                if (k != 0)
                {
                    do
                    {
                        s1 += buf[index++] & 0xff; s2 += s1;
                    }
                    while (--k != 0);
                }
                s1 %= BASE;
                s2 %= BASE;
            }
            return (s2 << 16) | s1;
        }
        #endregion

    }
}
