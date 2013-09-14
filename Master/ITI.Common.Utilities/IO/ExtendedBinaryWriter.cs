using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ITI.Common.Utilities.IO.Enms;

namespace ITI.Common.Utilities.IO
{
    /// <summary>
    /// Helper class that extends the functionality of <see cref="BinaryWriter"/> for custom serialization.
    /// </summary>
    /// <author>Ahmed Al Amir</author>
    public class ExtendedBinaryWriter : BinaryWriter
    {
        #region -- Constructor (s) / Destructor --
        /// <summary>
        /// Default Constructor will be hidden in order to enforce factory usage.
        /// </summary>
        /// <param name="s">Stream that will take the payload.</param>
        private ExtendedBinaryWriter(Stream s)
            : base(s)
        {
            // Nothing to do.
        }
        #endregion

        #region -- Factory Methods --
        /// <summary>
        /// Initialize the writer with a passed <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="s">Stream that will take payload.</param>
        /// <returns>Initialized ExtendedSerializationWriter with the passed <see cref="Stream"/></returns>
        public static ExtendedBinaryWriter Create(Stream s)
        {
            return new ExtendedBinaryWriter(s);
        }        
        #endregion 

        #region -- Overrides --
        /// <summary>
        /// Overrides Write method of <see cref="BinaryWriter"/> by writting byte before string then writes a length-prefixed string to this stream in the current encoding of
        /// <see cref="BinaryWriter"/>, and advances the current position of the stream
        /// in accordance with the encoding used and the specific characters being written
        /// to the stream.
        /// 
        /// </summary>
        /// <param name="str">String To be written.</param>
        public override void Write(string str)
        {
            if (str == null)
            {
                Write((byte)ObjectType.Null);
            }
            else
            {
                Write((byte)ObjectType.String);
                base.Write(str);
            }
        }

        /// <summary>
        /// Overrides Write method of <see cref="BinaryWriter"/> by writting array length then actual data.
        /// </summary>
        /// <param name="b">Byte array to be written.</param>        
        public override void Write(byte[] b)
        {
            if (b == null)
            {
                Write(-1);
            }
            else
            {
                Write(b.Length);
                if (b.Length > 0) base.Write(b);
            }
        }

        /// <summary>
        /// Overrides Write method of <see cref="BinaryWriter"/> by writting array length then actual data.
        /// </summary>
        /// <param name="c">Character array to be written.</param>
        public override void Write(char[] c)
        {
            if (c == null)
            {
                Write(-1);
            }
            else
            {                
                Write(c.Length);
                if (c.Length > 0) base.Write(c);
            }
        }

       
        #endregion

        #region -- Public Methods --
        /// <summary>
        /// Overrides Write method of <see cref="BinaryWriter"/> by writting <see cref="DateTime"/> ticks only.
        /// </summary>
        /// <param name="dt">DataTime to be written.</param>
        public void Write(DateTime dt)
        {
            Write(dt.Ticks);
        }

        /// <summary>
        /// Overrides Write method of <see cref="BinaryWriter"/> by writting <see cref="TimeSpan"/> ticks only.
        /// </summary>
        /// <param name="ts">TimeSpan to be written.</param>
        public void Write(TimeSpan ts)
        {
            Write(ts.Ticks);
        }

        /// <summary>
        /// Writes a generic ICollection  to the buffer.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="c">Collection to be written.</param>
        public void Write<T>(ICollection<T> c)
        {
            if (c == null)
            {
                Write(-1);
            }
            else
            {
                Write(c.Count);
                foreach (T item in c) WriteObject(item);
            }
        }

        /// <summary>
        /// Writes <see cref="ArrayList"/> of objects to stream.
        /// </summary>
        /// <param name="c">ArrayList to be written.</param>
        public void Write(ArrayList c)
        {
            if (c == null)
            {
                Write(-1);
            }
            else
            {
                Write(c.Count);
                foreach (object item in c) WriteObject(item);
            }
        }

        /// <summary>
        /// Writes <see cref="BitArray"/> of objects to stream.
        /// </summary>
        /// <param name="c">BitArray to be written</param>
        public void Write(BitArray c)
        {
            if (c == null)
            {
                Write(-1);
            }
            else
            {
                Write(c.Count);
                foreach (bool item in c) this.Write(item);
            }
        }

        /// <summary>
        /// Writes an array of objects.
        /// </summary>
        /// <param name="objs">Object array to be written.</param>
        public void Write(Object[] objs)
        {
            if (objs == null)
            {
                Write(-1);
            }
            else
            {
                Write(objs.Length);
                foreach (object item in objs) WriteObject(item);
            }
        }

        /// <summary>
        /// Writes a generic IDictionary to the buffer. 
        /// </summary>       
        /// <param name="d">IDictionary to be written.</param>        
        public void Write<T, U>(IDictionary<T, U> d)
        {
            if (d == null)
            {
                Write(-1);
            }
            else
            {
                Write(d.Count);
                foreach (KeyValuePair<T, U> kvp in d)
                {
                    WriteObject(kvp.Key);
                    WriteObject(kvp.Value);
                }
            }
        }

        /// <summary>
        /// Writes a <see cref="Guid"/> into the stream.
        /// </summary>
        /// <param name="guid">Guid to be written.</param>
        public void Write(Guid guid)
        { 
            if (guid == null) { Write(-1); }
            else
            {
                byte[] guidbytes = guid.ToByteArray();
                Write(guidbytes.Length);
                if (guidbytes.Length > 0) base.Write(guidbytes);
            }
        }

        /// <summary>
        /// Writes an any object to the stream
        /// </summary>
        /// <param name="obj">Object to be written.</param>
        public void WriteObject(object obj)
        {
            if (obj == null)
            {
                Write((byte)ObjectType.Null);
            }
            else
            {
                switch (obj.GetType().Name)
                {

                    case "Boolean": Write((byte)ObjectType.Bool);
                        Write((bool)obj);
                        break;

                    case "Byte": Write((byte)ObjectType.Byte);
                        Write((byte)obj);
                        break;

                    case "UInt16": Write((byte)ObjectType.UInt16);
                        Write((ushort)obj);
                        break;

                    case "UInt32": Write((byte)ObjectType.UInt32);
                        Write((uint)obj);
                        break;

                    case "UInt64": Write((byte)ObjectType.UInt64);
                        Write((ulong)obj);
                        break;

                    case "SByte": Write((byte)ObjectType.SByte);
                        Write((sbyte)obj);
                        break;

                    case "Int16": Write((byte)ObjectType.Int16);
                        Write((short)obj);
                        break;

                    case "Int32": Write((byte)ObjectType.Int32);
                        Write((int)obj);
                        break;

                    case "Int64": Write((byte)ObjectType.Int64);
                        Write((long)obj);
                        break;

                    case "Char": Write((byte)ObjectType.Char);
                        base.Write((char)obj);
                        break;

                    case "String": Write((byte)ObjectType.String);
                        base.Write((string)obj);
                        break;

                    case "Single": Write((byte)ObjectType.Single);
                        Write((float)obj);
                        break;

                    case "Double": Write((byte)ObjectType.Double);
                        Write((double)obj);
                        break;

                    case "Decimal": Write((byte)ObjectType.Decimal);
                        Write((decimal)obj);
                        break;

                    case "DateTime": Write((byte)ObjectType.DateTime);
                        Write((DateTime)obj);
                        break;

                    case "TimeSpan": Write((byte)ObjectType.TimeSpan);
                        Write((TimeSpan)obj);
                        break;

                    case "Byte[]": Write((byte)ObjectType.ByteArray);
                        base.Write((byte[])obj);
                        break;

                    case "Char[]": Write((byte)ObjectType.CharArray);
                        base.Write((char[])obj);
                        break;

                    default: Write((byte)ObjectType.Other);                        
                        new BinaryFormatter().Serialize(this.BaseStream, obj);
                        break;

                }
            } 
        }  
        #endregion
    }
}
