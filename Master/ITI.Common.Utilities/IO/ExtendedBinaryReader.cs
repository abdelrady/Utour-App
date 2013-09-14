using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ITI.Common.Utilities.IO.Enms;

namespace ITI.Common.Utilities.IO
{
    /// <summary>
    /// Helper class that extends the functionality of <see cref="BinaryReader"/> for custom serialization.
    /// </summary>
    /// <author>Ahmed Al Amir</author>
    public class ExtendedBinaryReader : BinaryReader
    {
        #region -- Constructor (s) / Destructor --
        /// <summary>
        /// Default Constructor will be hidden in order to enforce factory usage.
        /// </summary>
        /// <param name="s">Stream that will take the payload.</param>
        private ExtendedBinaryReader(Stream s)
            : base(s)
        {
            // Nothing to do.
        }
        #endregion

        #region -- Factory Methods --
        /// <summary>
        /// Initialize the writer with a suitable <see cref="MemoryStream"/> with default buffer of 1K. 
        /// </summary>
        /// <returns>Initialized ExtendedBinaryReader with default buffer of 1K.</returns>
        public static ExtendedBinaryReader Create()
        {
            return new ExtendedBinaryReader(
                new MemoryStream(1024)
                );
        }
        /// <summary>
        /// Initialize the writer with a passed <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="s">Stream that will take payload.</param>
        /// <returns>Initialized ExtendedBinaryReader with the passed <see cref="Stream"/></returns>
        public static ExtendedBinaryReader Create(Stream s)
        {
            return new ExtendedBinaryReader(s);
        }
        #endregion 

        #region -- Overrides --

        /// <summary>
        /// Overrides ReadString method of <see cref="BinaryReader"/> to provide handling of null values.
        /// </summary>
        /// <returns>String read from stream.</returns>
        public override string ReadString()
        {
            ObjectType type = (ObjectType)ReadByte();
            if (type == ObjectType.String) 
                return base.ReadString();
            return null;
        }

        #endregion

        #region -- Public Methods --
        /// <summary>
        /// Reads an array of byte from stream.
        /// </summary>
        /// <returns>Array of bytes from stream.</returns>
        public byte[] ReadByteArray()
        {
            int len = ReadInt32();
            if (len > 0) return ReadBytes(len);
            if (len < 0) return null;
            return new byte[0];
        }

        /// <summary>
        /// Reads a character array from stream.
        /// </summary>
        /// <returns>Array of characters.</returns>
        public char[] ReadCharArray()
        {
            int len = ReadInt32();
            if (len > 0) return ReadChars(len);
            if (len < 0) return null;
            return new char[0];
        }

        /// <summary>
        /// Reads a <see cref="DateTime"/> from stream.
        /// </summary>
        /// <returns>DataTime read from stream.</returns>
        public DateTime ReadDateTime() 
        { 
            return new DateTime(ReadInt64()); 
        }

        /// <summary>
        /// Reads a <see cref="TimeSpan"/> from stream.
        /// </summary>
        /// <returns>TimeSpan read from stream.</returns>
        public TimeSpan ReadTimeSpan()
        {
            return new TimeSpan(ReadInt64());
        }

        /// <summary>
        /// Reads any object from stream.
        /// </summary>
        /// <returns>object read from stream.</returns>
        public object ReadObject()
        {
            ObjectType type = (ObjectType)ReadByte();
            switch (type)
            {
                case ObjectType.Bool: return ReadBoolean();
                case ObjectType.Byte: return ReadByte();
                case ObjectType.UInt16: return ReadUInt16();
                case ObjectType.UInt32: return ReadUInt32();
                case ObjectType.UInt64: return ReadUInt64();
                case ObjectType.SByte: return ReadSByte();
                case ObjectType.Int16: return ReadInt16();
                case ObjectType.Int32: return ReadInt32();
                case ObjectType.Int64: return ReadInt64();
                case ObjectType.Char: return ReadChar();
                case ObjectType.String: return base.ReadString();
                case ObjectType.Single: return ReadSingle();
                case ObjectType.Double: return ReadDouble();
                case ObjectType.Decimal: return ReadDecimal();
                case ObjectType.DateTime: return ReadDateTime();
                case ObjectType.TimeSpan: return ReadTimeSpan();
                case ObjectType.ByteArray: return ReadByteArray();
                case ObjectType.CharArray: return ReadCharArray();
                case ObjectType.Other: return new BinaryFormatter().Deserialize(this.BaseStream);
                default: return null;
            }
        }

        /// <summary>
        /// Reads a generic list from the buffer.
        /// </summary>
        /// <returns> Geneeric List read from stream.</returns>
        public IList<T> ReadList<T>()
        {
            int count = ReadInt32();
            if (count < 0) return null;
            IList<T> d = new List<T>();
            for (int i = 0; i < count; i++) d.Add((T)ReadObject());
            return d;
        }

        /// <summary>
        /// Reads <see cref="ArrayList"/> from stream.
        /// </summary>
        /// <returns>ArrayList read from stream.</returns>
        public ArrayList ReadList()
        {
            int count = ReadInt32();
            if (count < 0) return null;
            ArrayList d = new ArrayList();
            for (int i = 0; i < count; i++) d.Add(ReadObject());
            return d;
        }

        /// <summary>
        /// Reads <see cref="BitArray"/> from stream.
        /// </summary>
        /// <returns>BitArray read from stream.</returns>
        public BitArray ReadBitArray()
        {
            int count = ReadInt32();
            if (count < 0) return null;
            BitArray d = new BitArray(count, false);
            for (int i = 0; i < count; i++) d[i] = ReadBoolean();
            return d;
        }

        /// <summary> 
        /// Reads a generic Dictionary from the buffer. 
        /// </summary>
        /// <returns>IDictionary read from stream.</returns>
        public IDictionary<T, U> ReadDictionary<T, U>()
        {
            int count = ReadInt32();
            if (count < 0) return null;
            IDictionary<T, U> d = new Dictionary<T, U>();
            for (int i = 0; i < count; i++) d[(T)ReadObject()] = (U)ReadObject();
            return d;
        }

        /// <summary>
        /// Reads a <see cref="Guid"/> from stream.
        /// </summary>
        /// <returns>Guid Read from stream.</returns>
        public Guid ReadGuid()
        {
            return new Guid(ReadByteArray());            
        }

        /// <summary>
        /// Reads an array of objects from stream.
        /// </summary>
        /// <returns>object array read from stream.</returns>
        public object[] ReadObjects()
        {
            int count = ReadInt32();
            if (count < 0) return null;
            object[] d = new object[count];
            for (int i = 0; i < count; i++)
                d[i] = ReadObject();
            return d;
        }
        #endregion
    }
}
