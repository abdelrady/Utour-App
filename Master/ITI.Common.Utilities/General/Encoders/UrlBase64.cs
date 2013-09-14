
using System.IO;
using System;
namespace ITI.Common.Utilities.General.Encoders
{
    /// <summary>
    /// Convert binary data to and from UrlBase64 encoding.  This is identical to Base64 encoding, except that the padding character is "." and the other 
    /// non-alphanumeric characters are "-" and "_" instead of "+" and "/".
    /// <p>
    /// The purpose of UrlBase64 encoding is to provide a compact encoding of binary
    /// data that is safe for use as an URL parameter. Base64 encoding does not
    /// produce encoded values that are safe for use in URLs, since "/" can be 
    /// interpreted as a path delimiter; "+" is the encoded form of a space; and
    /// "=" is used to separate a name from the corresponding value in an URL 
    /// parameter.
    /// </p>
    /// </summary>
    public class UrlBase64
    {
        #region -- Local Variables --
        private static readonly IEncoder encoder = new UrlBase64Encoder();
        #endregion

        #region -- Public Methods --
        /// <summary>
        /// Encodes the input data producing a URL safe base 64 encoded byte array.       
        /// </summary>
        /// <param name="data"></param>
        /// <returns>a byte array containing the URL safe base 64 encoded data.</returns>
        public static byte[] Encode(byte[] data)
        {
            MemoryStream bOut = new MemoryStream();

            try
            {
                encoder.Encode(data, 0, data.Length, bOut);
            }
            catch (IOException e)
            {
                throw new Exception("exception encoding URL safe base64 string: " + e.Message, e);
            }

            return bOut.ToArray();
        }

        /// <summary>
        /// Encodes the byte data writing it to the given output stream.       
        /// </summary>
        /// <param name="data"></param>
        /// <param name="outStr"></param>
        /// <returns>the number of bytes produced.</returns>
        public static int Encode(byte[] data, Stream outStr)
        {
            return encoder.Encode(data, 0, data.Length, outStr);
        }

        /// <summary>
        ///  Decodes the URL safe base 64 encoded input data - white space will be ignored.       
        /// </summary>
        /// <param name="data"></param>
        /// <returns>a byte array representing the decoded data.</returns>
        public static byte[] Decode(byte[] data)
        {
            MemoryStream bOut = new MemoryStream();

            try
            {
                encoder.Decode(data, 0, data.Length, bOut);
            }
            catch (IOException e)
            {
                throw new Exception("exception decoding URL safe base64 string: " + e.Message, e);
            }

            return bOut.ToArray();
        }

        /// <summary>
        /// Decodes the URL safe base 64 encoded byte data writing it to the given output stream, whitespace characters will be ignored.        
        /// </summary>
        /// <param name="data"></param>
        /// <param name="outStr"></param>
        /// <returns>The number of bytes produced.</returns>
        public static int Decode(byte[] data, Stream outStr)
        {
            return encoder.Decode(data, 0, data.Length, outStr);
        }

        /// <summary>
        /// Decodes the URL safe base 64 encoded string data - whitespace will be ignored.        
        /// </summary>
        /// <param name="data"></param>
        /// <returns>a byte array representing the decoded data.</returns>
        public static byte[] Decode(string data)
        {
            MemoryStream bOut = new MemoryStream();

            try
            {
                encoder.DecodeString(data, bOut);
            }
            catch (IOException e)
            {
                throw new Exception("exception decoding URL safe base64 string: " + e.Message, e);
            }

            return bOut.ToArray();
        }

        /// <summary>
        /// Decode the URL safe base 64 encoded string data writing it to the given output stream, whitespace characters will be ignored.        
        /// </summary>
        /// <param name="data"></param>
        /// <param name="outStr"></param>
        /// <returns>the number of bytes produced.</returns>
        public static int Decode(string data, Stream outStr)
        {
            return encoder.DecodeString(data, outStr);
        } 
        #endregion
    }
}
