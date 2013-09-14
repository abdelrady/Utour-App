using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ITI.Common.Utilities.General
{
    /// <summary>
    /// public Hashing Methods Like MD5 , SHA1
    /// </summary>
    public static class HashingMethods
    {
        /// <summary>
        /// to be implemented correctly
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5Hash(this string input)
        {
            return MD5HashHelper.GetMd5Hash(MD5.Create() , input);
        }
        public static bool VerifyMD5Hash(this string input, string hash)
        {
            return MD5HashHelper.VerifyMd5Hash(MD5.Create(), input, hash);
        }

        /// <summary>
        /// to be implemented correctly
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetSHA1Hash(this string input)
        {
            return SHA1HashHelper.GetSHA1Hash(sha1Hash: new SHA1CryptoServiceProvider(), input: input );
        }
        public static bool VerifySha1Hash(this string input, string hash)
        {
            return SHA1HashHelper.VerifySha1Hash(sha1Hash: new SHA1CryptoServiceProvider(), input: input , hash: hash);
        }

        
        /// <summary>
        /// to be implemented correctly
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetHash(this string input)
        {
            return GetMD5Hash(input);
        }
    }

    internal class SHA1HashHelper
    {
        public static string GetSHA1Hash(SHA1 sha1Hash, string input)
        {
            return Encoding.UTF8.GetString(sha1Hash.ComputeHash(buffer: Encoding.UTF8.GetBytes(input)));
        }

        public static bool VerifySha1Hash(SHA1 sha1Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetSHA1Hash(sha1Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    class MD5HashHelper
    {
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        
        /// <summary>
        /// Verify a hash against a string.
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
