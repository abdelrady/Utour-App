using System.IO;
using ITI.Common.Utilities.General.String;

namespace ITI.Common.Utilities.General.Encoders
{
    /// <summary>
    /// Class to decode and encode Hex.
    /// </summary>
    public sealed class Hex
    {
        #region -- Local Variables --
        private static readonly IEncoder encoder = new HexEncoder();
        #endregion

        #region -- Constructor --
        private Hex()
        {
        }
        #endregion

        #region -- Public Methods --
        public static string ToHexString(byte[] data)
        {
            byte[] hex = Encode(data, 0, data.Length);
            return Strings.FromAsciiByteArray(hex);
        }

        public static string ToHexString(byte[] data, int off, int length)
        {
            byte[] hex = Encode(data, off, length);
            return Strings.FromAsciiByteArray(hex);
        }

        /// <summary>
        /// Encodes the input data producing a Hex encoded byte array.        
        /// </summary>
        /// <param name="data"></param>
        /// <returns>a byte array containing the Hex encoded data.</returns>
        public static byte[] Encode(byte[] data)
        {
            return Encode(data, 0, data.Length);
        }

        /// <summary>
        /// Encodes the input data producing a Hex encoded byte array.        
        /// </summary>
        /// <param name="data"></param>
        /// <param name="off"></param>
        /// <param name="length"></param>
        /// <returns>A byte array containing the Hex encoded data.</returns>
        public static byte[] Encode(byte[] data, int off, int length)
        {
            MemoryStream bOut = new MemoryStream(length * 2);

            encoder.Encode(data, off, length, bOut);

            return bOut.ToArray();
        }

        /// <summary>
        /// Hex encode the byte data writing it to the given output stream.        
        /// </summary>
        /// <param name="data"></param>
        /// <param name="outStream"></param>
        /// <returns>the number of bytes produced.</returns>
        public static int Encode(byte[] data, Stream outStream)
        {
            return encoder.Encode(data, 0, data.Length, outStream);
        }

        /// <summary>
        /// Hex encode the byte data writing it to the given output stream.        
        /// </summary>
        /// <param name="data"></param>
        /// <param name="off"></param>
        /// <param name="length"></param>
        /// <param name="outStream"></param>
        /// <returns>The number of bytes produced.</returns>
        public static int Encode(byte[] data, int off, int length, Stream outStream)
        {
            return encoder.Encode(data, off, length, outStream);
        }

        /// <summary>
        /// Decodes the Hex encoded input data. It is assumed the input data is valid.        
        /// </summary>
        /// <param name="data"></param>
        /// <returns>A byte array representing the decoded data.</returns>
        public static byte[] Decode(byte[] data)
        {
            MemoryStream bOut = new MemoryStream((data.Length + 1) / 2);

            encoder.Decode(data, 0, data.Length, bOut);

            return bOut.ToArray();
        }

        /// <summary>
        /// Decodes the Hex encoded string data - whitespace will be ignored.         
        /// </summary>
        /// <param name="data"></param>
        /// <returns>A byte array representing the decoded data.</returns>
        public static byte[] Decode(string data)
        {
            MemoryStream bOut = new MemoryStream((data.Length + 1) / 2);

            encoder.DecodeString(data, bOut);

            return bOut.ToArray();
        }

        /// <summary>
        /// Decodes the Hex encoded string data writing it to the given output stream, whitespace characters will be ignored.       
        /// </summary>
        /// <param name="data"></param>
        /// <param name="outStream"></param>
        /// <returns>the number of bytes produced.</returns>
        public static int Decode(string data, Stream outStream)
        {
            return encoder.DecodeString(data, outStream);
        } 
        #endregion
    }
}
