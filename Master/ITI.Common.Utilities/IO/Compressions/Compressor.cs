using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.Common.Utilities.IO.Compressions
{
    /// <summary>
    /// Compress or Decompress images
    /// Developed by Ramy El-Zawahry
    /// </summary>
    public class Compressor
    {
        /// <summary>
        /// compress method that returns <see cref=""/>
        /// </summary>
        /// <param name="compressedData"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] compressedData)
        {
            System.IO.MemoryStream decompressedStream = new System.IO.MemoryStream(compressedData);
            System.IO.Compression.GZipStream gzip = new System.IO.Compression.GZipStream(decompressedStream, System.IO.Compression.CompressionMode.Decompress);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (byte[])f.Deserialize(gzip);
        }


    }
}
