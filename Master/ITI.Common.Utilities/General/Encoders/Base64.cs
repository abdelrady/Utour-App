using System;
using System.IO;
using ITI.Common.Utilities.General.String;

namespace ITI.Common.Utilities.General.Encoders
{
    public sealed class Base64
    {
        #region -- Constructor --
        private Base64()
        {
        }
        #endregion

        #region -- Static Methods --
        /// <summary>
        /// Encodes the input data producing a base 64 encoded byte array.        
        /// </summary>
        /// <param name="data"></param>
        /// <returns>A byte array containing the base 64 encoded data.</returns>
        public static byte[] Encode(byte[] data)
        {
            string s = Convert.ToBase64String(data, 0, data.Length);
            return Strings.ToAsciiByteArray(s);
        }

        /// <summary>
        /// Encodes the byte data to base 64 writing it to the given output stream.        
        /// </summary>
        /// <param name="data"></param>
        /// <param name="outStream"></param>
        /// <returns>the number of bytes produced.</returns>
        public static int Encode(byte[] data, Stream outStream)
        {
            string s = Convert.ToBase64String(data, 0, data.Length);
            byte[] encoded = Strings.ToAsciiByteArray(s);
            outStream.Write(encoded, 0, encoded.Length);
            return encoded.Length;
        }

        /// <summary>
        /// Encode the byte data to base 64 writing it to the given output stream.      
        /// </summary>
        /// <param name="data"></param>
        /// <param name="off"></param>
        /// <param name="length"></param>
        /// <param name="outStream"></param>
        /// <returns>The number of bytes produced.</returns>
        public static int Encode(byte[] data, int off, int length, Stream outStream)
        {
            string s = Convert.ToBase64String(data, off, length);
            byte[] encoded = Strings.ToAsciiByteArray(s);
            outStream.Write(encoded, 0, encoded.Length);
            return encoded.Length;
        }

        /// <summary>
        /// Decodes the base 64 encoded input data. It is assumed the input data is valid.        
        /// </summary>
        /// <param name="data"></param>
        /// <returns>A byte array representing the decoded data.</returns>
        public static byte[] Decode(byte[] data)
        {
            string s = Strings.FromAsciiByteArray(data);
            return Convert.FromBase64String(s);
        }

        /// <summary>
        /// Decodes the base 64 encoded string data - whitespace will be ignored.        
        /// </summary>
        /// <param name="data"></param>
        /// <returns>A byte array representing the decoded data.</returns>
        public static byte[] Decode(string data)
        {
            return Convert.FromBase64String(data);
        }

        /// <summary>
        /// Decodes the base 64 encoded string data writing it to the given output stream, whitespace characters will be ignored.       
        /// </summary>
        /// <param name="data"></param>
        /// <param name="outStream"></param>
        /// <returns>the number of bytes produced.</returns>
        public static int Decode(string data, Stream outStream)
        {
            byte[] decoded = Decode(data);
            outStream.Write(decoded, 0, decoded.Length);
            return decoded.Length;
        } 
        #endregion
    }
}
