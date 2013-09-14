using System;
using System.ServiceModel.Channels;
using System.IO.Compression;
using System.IO;
using System.Security.Cryptography;

namespace ITI.Common.Utilities.Runtime.Serialization.Encoders.GZip
{
    //This class is used to create the custom encoder (GZipMessageEncoder)
    public class GZipMessageEncoderFactory : MessageEncoderFactory
    {
        MessageEncoder encoder;

        //The GZip encoder wraps an inner encoder
        //We require a factory to be passed in that will create this inner encoder
        public GZipMessageEncoderFactory(MessageEncoderFactory messageEncoderFactory)
        {
            if (messageEncoderFactory == null)
                throw new ArgumentNullException("messageEncoderFactory", "A valid message encoder factory must be passed to the GZipEncoder");
            encoder = new GZipMessageEncoder(messageEncoderFactory.Encoder);

        }

        //The service framework uses this property to obtain an encoder from this encoder factory
        public override MessageEncoder Encoder
        {
            get { return encoder; }
        }

        public override MessageVersion MessageVersion
        {
            get { return encoder.MessageVersion; }
        }

        //This is the actual GZip encoder
        class GZipMessageEncoder : MessageEncoder
        {
            static string GZipContentType = "application/x-gzip";

            //This implementation wraps an inner encoder that actually converts a WCF Message
            //into textual XML, binary XML or some other format. This implementation then compresses the results.
            //The opposite happens when reading messages.
            //This member stores this inner encoder.
            MessageEncoder innerEncoder;

            //We require an inner encoder to be supplied (see comment above)
            internal GZipMessageEncoder(MessageEncoder messageEncoder)
                : base()
            {
                if (messageEncoder == null)
                    throw new ArgumentNullException("messageEncoder", "A valid message encoder must be passed to the GZipEncoder");
                innerEncoder = messageEncoder;
            }

            //public override string CharSet
            //{
            //    get { return ""; }
            //}

            public override string ContentType
            {
                get { return GZipContentType; }
            }

            public override string MediaType
            {
                get { return GZipContentType; }
            }

            //SOAP version to use - we delegate to the inner encoder for this
            public override MessageVersion MessageVersion
            {
                get { return innerEncoder.MessageVersion; }
            }

            //Helper method to compress an array of bytes
            static ArraySegment<byte> CompressBuffer(ArraySegment<byte> buffer, BufferManager bufferManager, int messageOffset)
            {
                MemoryStream memoryStream = new MemoryStream();
                memoryStream.Write(buffer.Array, 0, messageOffset);

                using (GZipStream gzStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzStream.Write(buffer.Array, messageOffset, buffer.Count);
                }


                byte[] compressedBytes = memoryStream.ToArray();
                byte[] bufferedBytes = bufferManager.TakeBuffer(compressedBytes.Length);

                Array.Copy(compressedBytes, 0, bufferedBytes, 0, compressedBytes.Length);

                bufferManager.ReturnBuffer(buffer.Array);
                ArraySegment<byte> byteArray = new ArraySegment<byte>(bufferedBytes, messageOffset, bufferedBytes.Length - messageOffset);

                return byteArray;
            }

            //Helper method to decompress an array of bytes
            static ArraySegment<byte> DecompressBuffer(ArraySegment<byte> buffer, BufferManager bufferManager)
            {
                
                MemoryStream memoryStream = new MemoryStream(buffer.Array, buffer.Offset, buffer.Count - buffer.Offset);
                MemoryStream decompressedStream = new MemoryStream();
                int totalRead = 0;
                int blockSize = 1024;
                byte[] tempBuffer = bufferManager.TakeBuffer(blockSize);
                using (GZipStream gzStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    while (true)
                    {
                        int bytesRead = gzStream.Read(tempBuffer, 0, blockSize);
                        if (bytesRead == 0)
                            break;
                        decompressedStream.Write(tempBuffer, 0, bytesRead);
                        totalRead += bytesRead;
                    }
                }
                bufferManager.ReturnBuffer(tempBuffer);

                byte[] decompressedBytes = decompressedStream.ToArray();
                byte[] bufferManagerBuffer = bufferManager.TakeBuffer(decompressedBytes.Length + buffer.Offset);
                Array.Copy(buffer.Array, 0, bufferManagerBuffer, 0, buffer.Offset);
                Array.Copy(decompressedBytes, 0, bufferManagerBuffer, buffer.Offset, decompressedBytes.Length);

                ArraySegment<byte> byteArray = new ArraySegment<byte>(bufferManagerBuffer, buffer.Offset, decompressedBytes.Length);
                bufferManager.ReturnBuffer(buffer.Array);

                return byteArray;
            }


            static byte[] Encrypt(byte[] Data, byte[] Key)
            {
                try
                {
                    // Create a MemoryStream that is going to accept the encrypted bytes 

                    MemoryStream ms = new MemoryStream();



                    // Create a symmetric algorithm. 

                    // We are going to use Rijndael because it is strong and available on all platforms. 

                    // You can use other algorithms, to do so substitute the next line with something like 

                    //                      TripleDES alg = TripleDES.Create(); 

                    Rijndael alg = Rijndael.Create();
                    alg.Padding = PaddingMode.PKCS7;


                    // Now set the key and the IV. 

                    // We need the IV (Initialization Vector) because the algorithm is operating in its default 

                    // mode called CBC (Cipher Block Chaining). The IV is XORed with the first block (8 byte) 

                    // of the data before it is encrypted, and then each encrypted block is XORed with the 

                    // following block of plaintext. This is done to make encryption more secure. 

                    // There is also a mode called ECB which does not need an IV, but it is much less secure. 
                    byte[] Salt = { 178, 191, 214, 46, 220, 83, 160, 73, 40, 39, 201, 155, 19, 202, 3, 11 };

                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Key, Salt, 1000);

                    alg.Key = pdb.GetBytes(32);

                    alg.IV = pdb.GetBytes(16);



                    // Create a CryptoStream through which we are going to be pumping our data. 

                    // CryptoStreamMode.Write means that we are going to be writing data to the stream 

                    // and the output will be written in the MemoryStream we have provided. 

                    CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);



                    // Write the data and make it do the encryption 

                    cs.Write(Data, 0, Data.Length);



                    // Close the crypto stream (or do FlushFinalBlock). 

                    // This will tell it that we have done our encryption and there is no more data coming in, 

                    // and it is now a good time to apply the padding and finalize the encryption process. 

                    cs.Close();



                    // Now get the encrypted data from the MemoryStream. 

                    // Some people make a mistake of using GetBuffer() here, which is not the right way. 

                    byte[] encryptedData = ms.ToArray();



                    return encryptedData;

                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }



            static byte[] Decrypt(byte[] cipherData, byte[] Key)
            {

                // Create a MemoryStream that is going to accept the decrypted bytes 

                MemoryStream ms = new MemoryStream();



                // Create a symmetric algorithm. 

                // We are going to use Rijndael because it is strong and available on all platforms. 

                // You can use other algorithms, to do so substitute the next line with something like 

                //                      TripleDES alg = TripleDES.Create(); 

                Rijndael alg = Rijndael.Create();

                alg.Padding = PaddingMode.PKCS7;

                // Now set the key and the IV. 

                // We need the IV (Initialization Vector) because the algorithm is operating in its default 

                // mode called CBC (Cipher Block Chaining). The IV is XORed with the first block (8 byte) 

                // of the data after it is decrypted, and then each decrypted block is XORed with the previous 

                // cipher block. This is done to make encryption more secure. 

                // There is also a mode called ECB which does not need an IV, but it is much less secure. 

                byte[] Salt = { 178, 191, 214, 46, 220, 83, 160, 73, 40, 39, 201, 155, 19, 202, 3, 11 };

                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Key, Salt, 1000);

                alg.Key = pdb.GetBytes(32);

                alg.IV = pdb.GetBytes(16);



                // Create a CryptoStream through which we are going to be pumping our data. 

                // CryptoStreamMode.Write means that we are going to be writing data to the stream 

                // and the output will be written in the MemoryStream we have provided. 

                CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);



                // Write the data and make it do the decryption 

                cs.Write(cipherData, 0, cipherData.Length);



                // Close the crypto stream (or do FlushFinalBlock). 

                // This will tell it that we have done our decryption and there is no more data coming in, 

                // and it is now a good time to remove the padding and finalize the decryption process. 

                cs.Close();



                // Now get the decrypted data from the MemoryStream. 

                // Some people make a mistake of using GetBuffer() here, which is not the right way. 

                byte[] decryptedData = ms.ToArray();

                return decryptedData;
            }

            //One of the two main entry points into the encoder. Called by WCF to decode a buffered byte array into a Message.
            public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
            {
                //Decompress the buffer
                ArraySegment<byte> decompressedBuffer = DecompressBuffer(buffer, bufferManager);
                //Use the inner encoder to decode the decompressed buffer
                Message returnMessage = innerEncoder.ReadMessage(decompressedBuffer, bufferManager);
                returnMessage.Properties.Encoder = this;
                return returnMessage;
            }

            //One of the two main entry points into the encoder. Called by WCF to encode a Message into a buffered byte array.
            public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
            {
                //Use the inner encoder to encode a Message into a buffered byte array
                ArraySegment<byte> buffer = innerEncoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);
                //Compress the resulting byte array
                return CompressBuffer(buffer, bufferManager, messageOffset);
            }

            public override Message ReadMessage(System.IO.Stream stream, int maxSizeOfHeaders, string contentType)
            {
                GZipStream gzStream = new GZipStream(stream, CompressionMode.Decompress, true);
                return innerEncoder.ReadMessage(gzStream, maxSizeOfHeaders);
            }

            public override void WriteMessage(Message message, System.IO.Stream stream)
            {
                using (GZipStream gzStream = new GZipStream(stream, CompressionMode.Compress, true))
                {
                    innerEncoder.WriteMessage(message, gzStream);
                }

                // innerEncoder.WriteMessage(message, gzStream) depends on that it can flush data by flushing 
                // the stream passed in, but the implementation of GZipStream.Flush will not flush underlying
                // stream, so we need to flush here.
                stream.Flush();
            }
        }
    }
}
