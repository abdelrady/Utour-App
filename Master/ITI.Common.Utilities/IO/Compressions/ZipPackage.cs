using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;

namespace ITI.Common.Utilities.IO.Compressions
{
    #region -- Enums --
    /// <summary>
    /// Represents the compression methods.
    /// </summary>
    public enum ZipCompression
    {
        /// <summary>
        /// This is the defaul compression method.
        /// </summary>
        Default = -1,

        /// <summary>
        /// This is the no-compression method.
        /// </summary>
        Stored = 0,

        /// <summary>
        /// This is the fastest compression method.
        /// </summary>
        BestSpeed = 1,

        /// <summary>
        /// This is a custom compression method.
        /// </summary>
        Method2 = 2,

        /// <summary>
        /// This is a custom compression method.
        /// </summary>
        Method3 = 3,

        /// <summary>
        /// This is a custom compression method.        
        /// </summary>
        Method4 = 4,

        /// <summary>
        /// This is a custom compression method.
        /// </summary>
        Method5 = 5,

        /// <summary>
        /// This is a custom compression method.
        /// </summary>
        Method6 = 6,

        /// <summary>
        /// This is a custom compression method.
        /// </summary>
        Method7 = 7,

        /// <summary>
        /// This is the the best compression method.
        /// </summary>
        Deflated = 8,

        /// <summary>
        /// This is the the best compression method.
        /// </summary>
        Deflate64 = 9
    }
    #endregion

    #region -- Internal Classes & Interfaces --

    #region -- Interfaces --
    internal interface IValueUpdater
    {
        long UpdateValue(long value, byte[] buf, int index, int len);
    }
    #endregion

    #region -- Classes
    internal class Adler32 : IValueUpdater
    {
        private long adler = 1;

        public long UpdateValue(long value, byte[] buffer, int index, int length)
        {
            if ((value == 1) || (buffer == null))
            {
                this.adler = 1;
            }
            if (buffer != null)
            {
                if (buffer == null)
                {
                    throw new ArgumentNullException("buffer");
                }

                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException("index", "cannot be negative");
                }

                if (length < 0)
                {
                    throw new ArgumentOutOfRangeException("length", "cannot be negative");
                }

                if (index >= buffer.Length)
                {
                    throw new ArgumentOutOfRangeException("index", "not a valid index into buffer");
                }

                if (index + length > buffer.Length)
                {
                    throw new ArgumentOutOfRangeException("index", "exceeds buffer size");
                }

                long num = this.adler & 65535;
                long num2 = (this.adler >> 16) & 65535;
                while (length > 0)
                {
                    int num3 = (length < 5552) ? length : 5552;
                    length -= num3;
                    while (num3 != 0)
                    {
                        num2 += num += buffer[index] & 255;
                        index++;
                        num3--;
                    }
                    num = num % 65521;
                    num2 = num2 % 65521;
                }
                this.adler = (num2 << 16) | num;
            }
            return this.adler;
        }
    }
    internal class CRC32 : IValueUpdater
    {
        private static readonly uint[] table = new uint[] 
        { 
            0, 0x77073096, 0xee0e612c, 0x990951ba, 0x76dc419, 0x706af48f, 0xe963a535, 0x9e6495a3, 0xedb8832, 0x79dcb8a4, 0xe0d5e91e, 0x97d2d988, 0x9b64c2b, 0x7eb17cbd, 0xe7b82d07, 0x90bf1d91, 
            0x1db71064, 0x6ab020f2, 0xf3b97148, 0x84be41de, 0x1adad47d, 0x6ddde4eb, 0xf4d4b551, 0x83d385c7, 0x136c9856, 0x646ba8c0, 0xfd62f97a, 0x8a65c9ec, 0x14015c4f, 0x63066cd9, 0xfa0f3d63, 0x8d080df5, 
            0x3b6e20c8, 0x4c69105e, 0xd56041e4, 0xa2677172, 0x3c03e4d1, 0x4b04d447, 0xd20d85fd, 0xa50ab56b, 0x35b5a8fa, 0x42b2986c, 0xdbbbc9d6, 0xacbcf940, 0x32d86ce3, 0x45df5c75, 0xdcd60dcf, 0xabd13d59, 
            0x26d930ac, 0x51de003a, 0xc8d75180, 0xbfd06116, 0x21b4f4b5, 0x56b3c423, 0xcfba9599, 0xb8bda50f, 0x2802b89e, 0x5f058808, 0xc60cd9b2, 0xb10be924, 0x2f6f7c87, 0x58684c11, 0xc1611dab, 0xb6662d3d, 
            0x76dc4190, 0x1db7106, 0x98d220bc, 0xefd5102a, 0x71b18589, 0x6b6b51f, 0x9fbfe4a5, 0xe8b8d433, 0x7807c9a2, 0xf00f934, 0x9609a88e, 0xe10e9818, 0x7f6a0dbb, 0x86d3d2d, 0x91646c97, 0xe6635c01, 
            0x6b6b51f4, 0x1c6c6162, 0x856530d8, 0xf262004e, 0x6c0695ed, 0x1b01a57b, 0x8208f4c1, 0xf50fc457, 0x65b0d9c6, 0x12b7e950, 0x8bbeb8ea, 0xfcb9887c, 0x62dd1ddf, 0x15da2d49, 0x8cd37cf3, 0xfbd44c65, 
            0x4db26158, 0x3ab551ce, 0xa3bc0074, 0xd4bb30e2, 0x4adfa541, 0x3dd895d7, 0xa4d1c46d, 0xd3d6f4fb, 0x4369e96a, 0x346ed9fc, 0xad678846, 0xda60b8d0, 0x44042d73, 0x33031de5, 0xaa0a4c5f, 0xdd0d7cc9, 
            0x5005713c, 0x270241aa, 0xbe0b1010, 0xc90c2086, 0x5768b525, 0x206f85b3, 0xb966d409, 0xce61e49f, 0x5edef90e, 0x29d9c998, 0xb0d09822, 0xc7d7a8b4, 0x59b33d17, 0x2eb40d81, 0xb7bd5c3b, 0xc0ba6cad, 
            0xedb88320, 0x9abfb3b6, 0x3b6e20c, 0x74b1d29a, 0xead54739, 0x9dd277af, 0x4db2615, 0x73dc1683, 0xe3630b12, 0x94643b84, 0xd6d6a3e, 0x7a6a5aa8, 0xe40ecf0b, 0x9309ff9d, 0xa00ae27, 0x7d079eb1, 
            0xf00f9344, 0x8708a3d2, 0x1e01f268, 0x6906c2fe, 0xf762575d, 0x806567cb, 0x196c3671, 0x6e6b06e7, 0xfed41b76, 0x89d32be0, 0x10da7a5a, 0x67dd4acc, 0xf9b9df6f, 0x8ebeeff9, 0x17b7be43, 0x60b08ed5, 
            0xd6d6a3e8, 0xa1d1937e, 0x38d8c2c4, 0x4fdff252, 0xd1bb67f1, 0xa6bc5767, 0x3fb506dd, 0x48b2364b, 0xd80d2bda, 0xaf0a1b4c, 0x36034af6, 0x41047a60, 0xdf60efc3, 0xa867df55, 0x316e8eef, 0x4669be79, 
            0xcb61b38c, 0xbc66831a, 0x256fd2a0, 0x5268e236, 0xcc0c7795, 0xbb0b4703, 0x220216b9, 0x5505262f, 0xc5ba3bbe, 0xb2bd0b28, 0x2bb45a92, 0x5cb36a04, 0xc2d7ffa7, 0xb5d0cf31, 0x2cd99e8b, 0x5bdeae1d, 
            0x9b64c2b0, 0xec63f226, 0x756aa39c, 0x26d930a, 0x9c0906a9, 0xeb0e363f, 0x72076785, 0x5005713, 0x95bf4a82, 0xe2b87a14, 0x7bb12bae, 0xcb61b38, 0x92d28e9b, 0xe5d5be0d, 0x7cdcefb7, 0xbdbdf21, 
            0x86d3d2d4, 0xf1d4e242, 0x68ddb3f8, 0x1fda836e, 0x81be16cd, 0xf6b9265b, 0x6fb077e1, 0x18b74777, 0x88085ae6, 0xff0f6a70, 0x66063bca, 0x11010b5c, 0x8f659eff, 0xf862ae69, 0x616bffd3, 0x166ccf45, 
            0xa00ae278, 0xd70dd2ee, 0x4e048354, 0x3903b3c2, 0xa7672661, 0xd06016f7, 0x4969474d, 0x3e6e77db, 0xaed16a4a, 0xd9d65adc, 0x40df0b66, 0x37d83bf0, 0xa9bcae53, 0xdebb9ec5, 0x47b2cf7f, 0x30b5ffe9, 
            0xbdbdf21c, 0xcabac28a, 0x53b39330, 0x24b4a3a6, 0xbad03605, 0xcdd70693, 0x54de5729, 0x23d967bf, 0xb3667a2e, 0xc4614ab8, 0x5d681b02, 0x2a6f2b94, 0xb40bbe37, 0xc30c8ea1, 0x5a05df1b, 0x2d02ef8d
        };

        private long crc;

        public long CrcValue
        {
            get
            {
                return this.crc;
            }
            set
            {
                this.crc = (uint)value;
            }
        }

        public long UpdateValue(long value, byte[] buffer, int index, int length)
        {
            if (value == 1 || buffer == null)
            {
                this.crc = 0;
            }
            if (buffer != null)
            {
                if (buffer == null)
                {
                    throw new ArgumentNullException("buffer");
                }

                if (length < 0)
                {
                    throw new ArgumentOutOfRangeException("length", "Length cannot be less than zero");
                }

                if (index < 0 || index + length > buffer.Length)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                this.crc ^= 0xFFFFFFFF;
                for (int i = 0; i < length; i++)
                {
                    this.crc = table[(((int)this.crc) ^ buffer[index + i]) & 255] ^ (this.crc >> 8);
                }
                this.crc ^= 0xFFFFFFFF;
            }
            return this.crc;
        }
    }
    internal class InfCodes
    {
        private static int[] inflateMask = new int[] 
        { 
            0, 1, 3, 7, 15, 0x1f, 0x3f, 0x7f, 0xff, 0x1ff, 0x3ff, 0x7ff, 0xfff, 0x1fff, 0x3fff, 0x7fff, 
            0xffff
        };
        private byte dipBits;
        private int dist;
        private int[] dipTree;
        private int dtreeIndex;
        private int get;
        private byte lipBits;
        private int len;
        private int lit;
        private int[] lipTree;
        private int ltreeIndex;
        private int mode;
        private int need;
        private int[] tree;
        private int treeIndex;

        internal InfCodes(int bl, int bd, int[] tl, int tilIndex, int[] td, int tidIndex)
        {
            this.mode = 0;
            this.lipBits = (byte)bl;
            this.dipBits = (byte)bd;
            this.lipTree = tl;
            this.ltreeIndex = tilIndex;
            this.dipTree = td;
            this.dtreeIndex = tidIndex;
        }

        internal InfCodes(int bl, int bd, int[] tl, int[] td)
        {
            this.mode = 0;
            this.lipBits = (byte)bl;
            this.dipBits = (byte)bd;
            this.lipTree = tl;
            this.ltreeIndex = 0;
            this.dipTree = td;
            this.dtreeIndex = 0;
        }

        internal int ProcessStream(InfBlocks infBlocks, ZipBaseStream zipBaseStream, int r)
        {
            int get;
            int currentDistBlocks;
            int bitb = 0;
            int bitk = 0;
            int nextInIndex = 0;
            nextInIndex = zipBaseStream.NextInIndex;
            int availableIn = zipBaseStream.AvailIn;
            bitb = infBlocks.BitB;
            bitk = infBlocks.BitK;
            int write = infBlocks.Write;
            int availableBlocks = (write < infBlocks.Read) ? ((infBlocks.Read - write) - 1) : (infBlocks.End - write);
        Label_0051:
            switch (this.mode)
            {
                case 0:
                    if ((availableBlocks < 0x102) || (availableIn < 10))
                    {
                        break;
                    }
                    infBlocks.BitB = bitb;
                    infBlocks.BitK = bitk;
                    zipBaseStream.AvailIn = availableIn;
                    zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
                    zipBaseStream.NextInIndex = nextInIndex;
                    infBlocks.Write = write;
                    r = FastInflate(this.lipBits, this.dipBits, this.lipTree, this.ltreeIndex, this.dipTree, this.dtreeIndex, infBlocks, zipBaseStream);
                    nextInIndex = zipBaseStream.NextInIndex;
                    availableIn = zipBaseStream.AvailIn;
                    bitb = infBlocks.BitB;
                    bitk = infBlocks.BitK;
                    write = infBlocks.Write;
                    availableBlocks = (write < infBlocks.Read) ? ((infBlocks.Read - write) - 1) : (infBlocks.End - write);
                    if (r == 0)
                    {
                        break;
                    }
                    this.mode = (r == 1) ? 7 : 9;
                    goto Label_0051;

                case 1:
                    goto Label_0198;

                case 2:
                    get = this.get;
                    while (bitk < get)
                    {
                        if (availableIn != 0)
                        {
                            r = 0;
                        }
                        else
                        {
                            infBlocks.BitB = bitb;
                            infBlocks.BitK = bitk;
                            zipBaseStream.AvailIn = availableIn;
                            zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
                            zipBaseStream.NextInIndex = nextInIndex;
                            infBlocks.Write = write;
                            return infBlocks.InflateFlush(zipBaseStream, r);
                        }
                        availableIn--;
                        bitb |= (zipBaseStream.NextIn[nextInIndex++] & 0xff) << bitk;
                        bitk += 8;
                    }
                    this.len += bitb & inflateMask[get];
                    bitb = bitb >> get;
                    bitk -= get;
                    this.need = this.dipBits;
                    this.tree = this.dipTree;
                    this.treeIndex = this.dtreeIndex;
                    this.mode = 3;
                    goto Label_0410;

                case 3:
                    goto Label_0410;

                case 4:
                    get = this.get;
                    while (bitk < get)
                    {
                        if (availableIn != 0)
                        {
                            r = 0;
                        }
                        else
                        {
                            infBlocks.BitB = bitb;
                            infBlocks.BitK = bitk;
                            zipBaseStream.AvailIn = availableIn;
                            zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
                            zipBaseStream.NextInIndex = nextInIndex;
                            infBlocks.Write = write;
                            return infBlocks.InflateFlush(zipBaseStream, r);
                        }
                        availableIn--;
                        bitb |= (zipBaseStream.NextIn[nextInIndex++] & 0xff) << bitk;
                        bitk += 8;
                    }
                    this.dist += bitb & inflateMask[get];
                    bitb = bitb >> get;
                    bitk -= get;
                    this.mode = 5;
                    goto Label_0633;

                case 5:
                    goto Label_0633;

                case 6:
                    if (availableBlocks == 0)
                    {
                        if ((write == infBlocks.End) && (infBlocks.Read != 0))
                        {
                            write = 0;
                            availableBlocks = (write < infBlocks.Read) ? ((infBlocks.Read - write) - 1) : (infBlocks.End - write);
                        }
                        if (availableBlocks == 0)
                        {
                            infBlocks.Write = write;
                            r = infBlocks.InflateFlush(zipBaseStream, r);
                            write = infBlocks.Write;
                            availableBlocks = (write < infBlocks.Read) ? ((infBlocks.Read - write) - 1) : (infBlocks.End - write);
                            if ((write == infBlocks.End) && (infBlocks.Read != 0))
                            {
                                write = 0;
                                availableBlocks = (write < infBlocks.Read) ? ((infBlocks.Read - write) - 1) : (infBlocks.End - write);
                            }
                            if (availableBlocks == 0)
                            {
                                infBlocks.BitB = bitb;
                                infBlocks.BitK = bitk;
                                zipBaseStream.AvailIn = availableIn;
                                zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
                                zipBaseStream.NextInIndex = nextInIndex;
                                infBlocks.Write = write;
                                return infBlocks.InflateFlush(zipBaseStream, r);
                            }
                        }
                    }
                    r = 0;
                    infBlocks.Window[write++] = (byte)this.lit;
                    availableBlocks--;
                    this.mode = 0;
                    goto Label_0051;

                case 7:
                    if (bitk > 7)
                    {
                        bitk -= 8;
                        availableIn++;
                        nextInIndex--;
                    }
                    infBlocks.Write = write;
                    r = infBlocks.InflateFlush(zipBaseStream, r);
                    write = infBlocks.Write;
                    availableBlocks = (write < infBlocks.Read) ? ((infBlocks.Read - write) - 1) : (infBlocks.End - write);
                    if (infBlocks.Read != infBlocks.Write)
                    {
                        infBlocks.BitB = bitb;
                        infBlocks.BitK = bitk;
                        zipBaseStream.AvailIn = availableIn;
                        zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
                        zipBaseStream.NextInIndex = nextInIndex;
                        infBlocks.Write = write;
                        return infBlocks.InflateFlush(zipBaseStream, r);
                    }
                    this.mode = 8;
                    goto Label_0992;

                case 8:
                    goto Label_0992;

                case 9:
                    r = -3;
                    infBlocks.BitB = bitb;
                    infBlocks.BitK = bitk;
                    zipBaseStream.AvailIn = availableIn;
                    zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
                    zipBaseStream.NextInIndex = nextInIndex;
                    infBlocks.Write = write;
                    return infBlocks.InflateFlush(zipBaseStream, r);

                default:
                    r = -2;
                    infBlocks.BitB = bitb;
                    infBlocks.BitK = bitk;
                    zipBaseStream.AvailIn = availableIn;
                    zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
                    zipBaseStream.NextInIndex = nextInIndex;
                    infBlocks.Write = write;
                    return infBlocks.InflateFlush(zipBaseStream, r);
            }
            this.need = this.lipBits;
            this.tree = this.lipTree;
            this.treeIndex = this.ltreeIndex;
            this.mode = 1;
        Label_0198:
            get = this.need;
            while (bitk < get)
            {
                if (availableIn != 0)
                {
                    r = 0;
                }
                else
                {
                    infBlocks.BitB = bitb;
                    infBlocks.BitK = bitk;
                    zipBaseStream.AvailIn = availableIn;
                    zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
                    zipBaseStream.NextInIndex = nextInIndex;
                    infBlocks.Write = write;
                    return infBlocks.InflateFlush(zipBaseStream, r);
                }
                availableIn--;
                bitb |= (zipBaseStream.NextIn[nextInIndex++] & 0xff) << bitk;
                bitk += 8;
            }
            int index = (this.treeIndex + (bitb & inflateMask[get])) * 3;
            bitb = bitb >> this.tree[index + 1];
            bitk -= this.tree[index + 1];
            int currentTreeNode = this.tree[index];
            if (currentTreeNode == 0)
            {
                this.lit = this.tree[index + 2];
                this.mode = 6;
                goto Label_0051;
            }
            if ((currentTreeNode & 0x10) != 0)
            {
                this.get = currentTreeNode & 15;
                this.len = this.tree[index + 2];
                this.mode = 2;
                goto Label_0051;
            }
            if ((currentTreeNode & 0x40) == 0)
            {
                this.need = currentTreeNode;
                this.treeIndex = (index / 3) + this.tree[index + 2];
                goto Label_0051;
            }
            if ((currentTreeNode & 0x20) != 0)
            {
                this.mode = 7;
                goto Label_0051;
            }
            this.mode = 9;
            r = -3;
            infBlocks.BitB = bitb;
            infBlocks.BitK = bitk;
            zipBaseStream.AvailIn = availableIn;
            zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
            zipBaseStream.NextInIndex = nextInIndex;
            infBlocks.Write = write;
            return infBlocks.InflateFlush(zipBaseStream, r);
        Label_0410:
            get = this.need;
            while (bitk < get)
            {
                if (availableIn != 0)
                {
                    r = 0;
                }
                else
                {
                    infBlocks.BitB = bitb;
                    infBlocks.BitK = bitk;
                    zipBaseStream.AvailIn = availableIn;
                    zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
                    zipBaseStream.NextInIndex = nextInIndex;
                    infBlocks.Write = write;
                    return infBlocks.InflateFlush(zipBaseStream, r);
                }
                availableIn--;
                bitb |= (zipBaseStream.NextIn[nextInIndex++] & 0xff) << bitk;
                bitk += 8;
            }
            index = (this.treeIndex + (bitb & inflateMask[get])) * 3;
            bitb = bitb >> this.tree[index + 1];
            bitk -= this.tree[index + 1];
            currentTreeNode = this.tree[index];
            if ((currentTreeNode & 0x10) != 0)
            {
                this.get = currentTreeNode & 15;
                this.dist = this.tree[index + 2];
                this.mode = 4;
                goto Label_0051;
            }
            if ((currentTreeNode & 0x40) == 0)
            {
                this.need = currentTreeNode;
                this.treeIndex = (index / 3) + this.tree[index + 2];
                goto Label_0051;
            }
            this.mode = 9;
            r = -3;
            infBlocks.BitB = bitb;
            infBlocks.BitK = bitk;
            zipBaseStream.AvailIn = availableIn;
            zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
            zipBaseStream.NextInIndex = nextInIndex;
            infBlocks.Write = write;
            return infBlocks.InflateFlush(zipBaseStream, r);
        Label_0633:
            currentDistBlocks = (write < this.dist) ? (infBlocks.End - (this.dist - write)) : (write - this.dist);
            while (this.len != 0)
            {
                if (availableBlocks == 0)
                {
                    if ((write == infBlocks.End) && (infBlocks.Read != 0))
                    {
                        write = 0;
                        availableBlocks = (write < infBlocks.Read) ? ((infBlocks.Read - write) - 1) : (infBlocks.End - write);
                    }
                    if (availableBlocks == 0)
                    {
                        infBlocks.Write = write;
                        r = infBlocks.InflateFlush(zipBaseStream, r);
                        write = infBlocks.Write;
                        availableBlocks = (write < infBlocks.Read) ? ((infBlocks.Read - write) - 1) : (infBlocks.End - write);
                        if ((write == infBlocks.End) && (infBlocks.Read != 0))
                        {
                            write = 0;
                            availableBlocks = (write < infBlocks.Read) ? ((infBlocks.Read - write) - 1) : (infBlocks.End - write);
                        }
                        if (availableBlocks == 0)
                        {
                            infBlocks.BitB = bitb;
                            infBlocks.BitK = bitk;
                            zipBaseStream.AvailIn = availableIn;
                            zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
                            zipBaseStream.NextInIndex = nextInIndex;
                            infBlocks.Write = write;
                            return infBlocks.InflateFlush(zipBaseStream, r);
                        }
                    }
                }
                infBlocks.Window[write++] = infBlocks.Window[currentDistBlocks++];
                availableBlocks--;
                if (currentDistBlocks == infBlocks.End)
                {
                    currentDistBlocks = 0;
                }
                this.len--;
            }
            this.mode = 0;
            goto Label_0051;
        Label_0992:
            r = 1;
            infBlocks.BitB = bitb;
            infBlocks.BitK = bitk;
            zipBaseStream.AvailIn = availableIn;
            zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
            zipBaseStream.NextInIndex = nextInIndex;
            infBlocks.Write = write;
            return infBlocks.InflateFlush(zipBaseStream, r);
        }

        private static int FastInflate(int bl, int bd, int[] tl, int tilIndex, int[] td, int tidIndex, InfBlocks infBlocks, ZipBaseStream zipBaseStream)
        {
            int step;
            int nextInIndex = zipBaseStream.NextInIndex;
            int availableIn = zipBaseStream.AvailIn;
            int bitb = infBlocks.BitB;
            int bitk = infBlocks.BitK;
            int write = infBlocks.Write;
            int availableBlocks = (write < infBlocks.Read) ? ((infBlocks.Read - write) - 1) : (infBlocks.End - write);
            int bilMask = inflateMask[bl];
            int bidMask = inflateMask[bd];
        Label_0092:
            while (bitk < 20)
            {
                availableIn--;
                bitb |= (zipBaseStream.NextIn[nextInIndex++] & 0xff) << bitk;
                bitk += 8;
            }
            int num = bitb & bilMask;
            int[] numArray = tl;
            int currentTlIndex = tilIndex;
            int index = numArray[(currentTlIndex + num) * 3];
            if (index == 0)
            {
                bitb = bitb >> numArray[((currentTlIndex + num) * 3) + 1];
                bitk -= numArray[((currentTlIndex + num) * 3) + 1];
                infBlocks.Window[write++] = (byte)numArray[((currentTlIndex + num) * 3) + 2];
                availableBlocks--;
                goto Label_05CA;
            }
        Label_00F0:
            bitb = bitb >> numArray[((currentTlIndex + num) * 3) + 1];
            bitk -= numArray[((currentTlIndex + num) * 3) + 1];
            if ((index & 0x10) == 0)
            {
                if ((index & 0x40) == 0)
                {
                    num += numArray[((currentTlIndex + num) * 3) + 2];
                    num += bitb & inflateMask[index];
                    index = numArray[(currentTlIndex + num) * 3];
                    if (index != 0)
                    {
                        goto Label_00F0;
                    }
                    bitb = bitb >> numArray[((currentTlIndex + num) * 3) + 1];
                    bitk -= numArray[((currentTlIndex + num) * 3) + 1];
                    infBlocks.Window[write++] = (byte)numArray[((currentTlIndex + num) * 3) + 2];
                    availableBlocks--;
                }
                else
                {
                    if ((index & 0x20) != 0)
                    {
                        step = zipBaseStream.AvailIn - availableIn;
                        step = ((bitk >> 3) < step) ? (bitk >> 3) : step;
                        availableIn += step;
                        nextInIndex -= step;
                        bitk -= step << 3;
                        infBlocks.BitB = bitb;
                        infBlocks.BitK = bitk;
                        zipBaseStream.AvailIn = availableIn;
                        zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
                        zipBaseStream.NextInIndex = nextInIndex;
                        infBlocks.Write = write;
                        return 1;
                    }
                    step = zipBaseStream.AvailIn - availableIn;
                    step = ((bitk >> 3) < step) ? (bitk >> 3) : step;
                    availableIn += step;
                    nextInIndex -= step;
                    bitk -= step << 3;
                    infBlocks.BitB = bitb;
                    infBlocks.BitK = bitk;
                    zipBaseStream.AvailIn = availableIn;
                    zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
                    zipBaseStream.NextInIndex = nextInIndex;
                    infBlocks.Write = write;
                    return -3;
                }
                goto Label_05CA;
            }
            index &= 15;
            step = numArray[((currentTlIndex + num) * 3) + 2] + (bitb & inflateMask[index]);
            bitb = bitb >> index;
            bitk -= index;
            while (bitk < 15)
            {
                availableIn--;
                bitb |= (zipBaseStream.NextIn[nextInIndex++] & 0xff) << bitk;
                bitk += 8;
            }
            num = bitb & bidMask;
            numArray = td;
            currentTlIndex = tidIndex;
            index = numArray[(currentTlIndex + num) * 3];
        Label_018A:
            bitb = bitb >> numArray[((currentTlIndex + num) * 3) + 1];
            bitk -= numArray[((currentTlIndex + num) * 3) + 1];
            if ((index & 0x10) != 0)
            {
                int currentAvailableBytes;
                index &= 15;
                while (bitk < index)
                {
                    availableIn--;
                    bitb |= (zipBaseStream.NextIn[nextInIndex++] & 0xff) << bitk;
                    bitk += 8;
                }
                int currentNum = numArray[((currentTlIndex + num) * 3) + 2] + (bitb & inflateMask[index]);
                bitb = bitb >> index;
                bitk -= index;
                availableBlocks -= step;
                if (write >= currentNum)
                {
                    currentAvailableBytes = write - currentNum;
                    if (((write - currentAvailableBytes) > 0) && (2 > (write - currentAvailableBytes)))
                    {
                        infBlocks.Window[write++] = infBlocks.Window[currentAvailableBytes++];
                        step--;
                        infBlocks.Window[write++] = infBlocks.Window[currentAvailableBytes++];
                        step--;
                    }
                    else
                    {
                        Array.Copy(infBlocks.Window, currentAvailableBytes, infBlocks.Window, write, 2);
                        write += 2;
                        currentAvailableBytes += 2;
                        step -= 2;
                    }
                }
                else
                {
                    index = currentNum - write;
                    currentAvailableBytes = infBlocks.End - index;
                    if (step > index)
                    {
                        step -= index;
                        if (((write - currentAvailableBytes) > 0) && (index > (write - currentAvailableBytes)))
                        {
                            do
                            {
                                infBlocks.Window[write++] = infBlocks.Window[currentAvailableBytes++];
                            }
                            while (--index != 0);
                        }
                        else
                        {
                            Array.Copy(infBlocks.Window, currentAvailableBytes, infBlocks.Window, write, index);
                            write += index;
                            currentAvailableBytes += index;
                            index = 0;
                        }
                        currentAvailableBytes = 0;
                    }
                }
                if (((write - currentAvailableBytes) > 0) && (step > (write - currentAvailableBytes)))
                {
                    do
                    {
                        infBlocks.Window[write++] = infBlocks.Window[currentAvailableBytes++];
                    }
                    while (--step != 0);
                }
                else
                {
                    Array.Copy(infBlocks.Window, currentAvailableBytes, infBlocks.Window, write, step);
                    write += step;
                    currentAvailableBytes += step;
                    step = 0;
                }
            }
            else
            {
                if ((index & 0x40) == 0)
                {
                    num += numArray[((currentTlIndex + num) * 3) + 2];
                    num += bitb & inflateMask[index];
                    index = numArray[(currentTlIndex + num) * 3];
                    goto Label_018A;
                }
                step = zipBaseStream.AvailIn - availableIn;
                step = ((bitk >> 3) < step) ? (bitk >> 3) : step;
                availableIn += step;
                nextInIndex -= step;
                bitk -= step << 3;
                infBlocks.BitB = bitb;
                infBlocks.BitK = bitk;
                zipBaseStream.AvailIn = availableIn;
                zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
                zipBaseStream.NextInIndex = nextInIndex;
                infBlocks.Write = write;
                return -3;
            }
        Label_05CA:
            if ((availableBlocks >= 0x102) && (availableIn >= 10))
            {
                goto Label_0092;
            }
            step = zipBaseStream.AvailIn - availableIn;
            step = ((bitk >> 3) < step) ? (bitk >> 3) : step;
            availableIn += step;
            nextInIndex -= step;
            bitk -= step << 3;
            infBlocks.BitB = bitb;
            infBlocks.BitK = bitk;
            zipBaseStream.AvailIn = availableIn;
            zipBaseStream.TotalIn += nextInIndex - zipBaseStream.NextInIndex;
            zipBaseStream.NextInIndex = nextInIndex;
            infBlocks.Write = write;
            return 0;
        }
    }
    internal class InfBlocks
    {
        private static int[] border = new int[] 
        { 
            16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 
            14, 1, 15
        };
        private static int[] inflateMask = new int[] 
        { 
            0, 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023, 2047, 4095, 8191, 16383, 32767, 
            65535
        };
        private int[] bb = new int[1];
        private int[] blens;
        private long check;
        private object checkfn;
        private InfCodes codes;
        private int[] hufts = new int[4320];
        private int index;
        private int last;
        private int left;
        private int mode;
        private int table;
        private int[] tb = new int[1];

        internal InfBlocks(ZipBaseStream zipBaseStream, object checkfn, int windowLength)
        {
            this.Window = new byte[windowLength];
            this.End = windowLength;
            this.checkfn = checkfn;
            this.mode = 0;
            this.Reset(zipBaseStream, null);
        }

        internal int BitB { get; set; }
        internal int BitK { get; set; }
        internal int End { get; set; }
        internal int Read { get; set; }
        internal byte[] Window { get; set; }
        internal int Write { get; set; }

        internal void Free(ZipBaseStream zipBaseStream)
        {
            this.Reset(zipBaseStream, null);
            this.Window = null;
            this.hufts = null;
        }

        internal int InflateFlush(ZipBaseStream zipBaseStream, int r)
        {
            int destinationIndex = zipBaseStream.NextOutIndex;
            int read = this.Read;
            int length = ((read <= this.Write) ? this.Write : this.End) - read;
            if (length > zipBaseStream.AvailOut)
            {
                length = zipBaseStream.AvailOut;
            }
            if ((length != 0) && (r == -5))
            {
                r = 0;
            }
            zipBaseStream.AvailOut -= length;
            zipBaseStream.TotalOut += length;
            if (this.checkfn != null)
            {
                this.check = zipBaseStream.BaseValueUpdater.UpdateValue(this.check, this.Window, read, length);
                zipBaseStream.Adler = this.check;
            }
            Array.Copy(this.Window, read, zipBaseStream.NextOut, destinationIndex, length);
            destinationIndex += length;
            read += length;
            if (read == this.End)
            {
                read = 0;
                if (this.Write == this.End)
                {
                    this.Write = 0;
                }
                length = this.Write - read;
                if (length > zipBaseStream.AvailOut)
                {
                    length = zipBaseStream.AvailOut;
                }
                if ((length != 0) && (r == -5))
                {
                    r = 0;
                }
                zipBaseStream.AvailOut -= length;
                zipBaseStream.TotalOut += length;
                if (this.checkfn != null)
                {
                    this.check = zipBaseStream.BaseValueUpdater.UpdateValue(this.check, this.Window, read, length);
                    zipBaseStream.Adler = this.check;
                }
                Array.Copy(this.Window, read, zipBaseStream.NextOut, destinationIndex, length);
                destinationIndex += length;
                read += length;
            }
            zipBaseStream.NextOutIndex = destinationIndex;
            this.Read = read;
            return r;
        }

        internal int ProcessStream(ZipBaseStream zipBaseStream, int r)
        {
            int table;
            int sourceIndex = zipBaseStream.NextInIndex;
            int availableIn = zipBaseStream.AvailIn;
            int bitb = this.BitB;
            int bitk = this.BitK;
            int write = this.Write;
            int currentAvailableBytes = (write < this.Read) ? ((this.Read - write) - 1) : (this.End - write);
        Label_0047:
            switch (this.mode)
            {
                case 0:
                    while (bitk < 3)
                    {
                        if (availableIn != 0)
                        {
                            r = 0;
                        }
                        else
                        {
                            this.BitB = bitb;
                            this.BitK = bitk;
                            zipBaseStream.AvailIn = availableIn;
                            zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                            zipBaseStream.NextInIndex = sourceIndex;
                            this.Write = write;
                            return this.InflateFlush(zipBaseStream, r);
                        }
                        availableIn--;
                        bitb |= (zipBaseStream.NextIn[sourceIndex++] & 0xff) << bitk;
                        bitk += 8;
                    }
                    table = bitb & 7;
                    this.last = table & 1;
                    switch (table >> 1)
                    {
                        case 0:
                            bitb = bitb >> 3;
                            bitk -= 3;
                            table = bitk & 7;
                            bitb = bitb >> table;
                            bitk -= table;
                            this.mode = 1;
                            goto Label_0047;

                        case 1:
                            {
                                int[] numArray = new int[1];
                                int[] numArray2 = new int[1];
                                int[][] numArray3 = new int[1][];
                                int[][] numArray4 = new int[1][];
                                InfTree.InflateTreesFixed(numArray, numArray2, numArray3, numArray4);
                                this.codes = new InfCodes(numArray[0], numArray2[0], numArray3[0], numArray4[0]);
                                bitb = bitb >> 3;
                                bitk -= 3;
                                this.mode = 6;
                                goto Label_0047;
                            }
                        case 2:
                            bitb = bitb >> 3;
                            bitk -= 3;
                            this.mode = 3;
                            goto Label_0047;

                        case 3:
                            bitb = bitb >> 3;
                            bitk -= 3;
                            this.mode = 9;
                            r = -3;
                            this.BitB = bitb;
                            this.BitK = bitk;
                            zipBaseStream.AvailIn = availableIn;
                            zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                            zipBaseStream.NextInIndex = sourceIndex;
                            this.Write = write;
                            return this.InflateFlush(zipBaseStream, r);
                    }
                    goto Label_0047;

                case 1:
                    while (bitk < 0x20)
                    {
                        if (availableIn != 0)
                        {
                            r = 0;
                        }
                        else
                        {
                            this.BitB = bitb;
                            this.BitK = bitk;
                            zipBaseStream.AvailIn = availableIn;
                            zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                            zipBaseStream.NextInIndex = sourceIndex;
                            this.Write = write;
                            return this.InflateFlush(zipBaseStream, r);
                        }
                        availableIn--;
                        bitb |= (zipBaseStream.NextIn[sourceIndex++] & 0xff) << bitk;
                        bitk += 8;
                    }
                    if (((~bitb >> 0x10) & 0xffff) != (bitb & 0xffff))
                    {
                        this.mode = 9;
                        r = -3;
                        this.BitB = bitb;
                        this.BitK = bitk;
                        zipBaseStream.AvailIn = availableIn;
                        zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                        zipBaseStream.NextInIndex = sourceIndex;
                        this.Write = write;
                        return this.InflateFlush(zipBaseStream, r);
                    }
                    this.left = bitb & 0xffff;
                    bitb = bitk = 0;
                    this.mode = (this.left != 0) ? 2 : ((this.last != 0) ? 7 : 0);
                    goto Label_0047;

                case 2:
                    if (availableIn != 0)
                    {
                        if (currentAvailableBytes == 0)
                        {
                            if ((write == this.End) && (this.Read != 0))
                            {
                                write = 0;
                                currentAvailableBytes = (write < this.Read) ? ((this.Read - write) - 1) : (this.End - write);
                            }
                            if (currentAvailableBytes == 0)
                            {
                                this.Write = write;
                                r = this.InflateFlush(zipBaseStream, r);
                                write = this.Write;
                                currentAvailableBytes = (write < this.Read) ? ((this.Read - write) - 1) : (this.End - write);
                                if ((write == this.End) && (this.Read != 0))
                                {
                                    write = 0;
                                    currentAvailableBytes = (write < this.Read) ? ((this.Read - write) - 1) : (this.End - write);
                                }
                                if (currentAvailableBytes == 0)
                                {
                                    this.BitB = bitb;
                                    this.BitK = bitk;
                                    zipBaseStream.AvailIn = availableIn;
                                    zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                                    zipBaseStream.NextInIndex = sourceIndex;
                                    this.Write = write;
                                    return this.InflateFlush(zipBaseStream, r);
                                }
                            }
                        }
                        r = 0;
                        table = this.left;
                        if (table > availableIn)
                        {
                            table = availableIn;
                        }
                        if (table > currentAvailableBytes)
                        {
                            table = currentAvailableBytes;
                        }
                        Array.Copy(zipBaseStream.NextIn, sourceIndex, this.Window, write, table);
                        sourceIndex += table;
                        availableIn -= table;
                        write += table;
                        currentAvailableBytes -= table;
                        this.left -= table;
                        if (this.left == 0)
                        {
                            this.mode = (this.last != 0) ? 7 : 0;
                        }
                        goto Label_0047;
                    }
                    this.BitB = bitb;
                    this.BitK = bitk;
                    zipBaseStream.AvailIn = availableIn;
                    zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                    zipBaseStream.NextInIndex = sourceIndex;
                    this.Write = write;
                    return this.InflateFlush(zipBaseStream, r);

                case 3:
                    while (bitk < 14)
                    {
                        if (availableIn != 0)
                        {
                            r = 0;
                        }
                        else
                        {
                            this.BitB = bitb;
                            this.BitK = bitk;
                            zipBaseStream.AvailIn = availableIn;
                            zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                            zipBaseStream.NextInIndex = sourceIndex;
                            this.Write = write;
                            return this.InflateFlush(zipBaseStream, r);
                        }
                        availableIn--;
                        bitb |= (zipBaseStream.NextIn[sourceIndex++] & 0xff) << bitk;
                        bitk += 8;
                    }
                    this.table = table = bitb & 0x3fff;
                    if (((table & 0x1f) > 0x1d) || (((table >> 5) & 0x1f) > 0x1d))
                    {
                        this.mode = 9;
                        r = -3;
                        this.BitB = bitb;
                        this.BitK = bitk;
                        zipBaseStream.AvailIn = availableIn;
                        zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                        zipBaseStream.NextInIndex = sourceIndex;
                        this.Write = write;
                        return this.InflateFlush(zipBaseStream, r);
                    }
                    table = (0x102 + (table & 0x1f)) + ((table >> 5) & 0x1f);
                    this.blens = new int[table];
                    bitb = bitb >> 14;
                    bitk -= 14;
                    this.index = 0;
                    this.mode = 4;
                    break;

                case 4:
                    break;

                case 5:
                    goto Label_0794;

                case 6:
                    goto Label_0B34;

                case 7:
                    goto Label_0BF1;

                case 8:
                    goto Label_0C86;

                case 9:
                    r = -3;
                    this.BitB = bitb;
                    this.BitK = bitk;
                    zipBaseStream.AvailIn = availableIn;
                    zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                    zipBaseStream.NextInIndex = sourceIndex;
                    this.Write = write;
                    return this.InflateFlush(zipBaseStream, r);

                default:
                    r = -2;
                    this.BitB = bitb;
                    this.BitK = bitk;
                    zipBaseStream.AvailIn = availableIn;
                    zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                    zipBaseStream.NextInIndex = sourceIndex;
                    this.Write = write;
                    return this.InflateFlush(zipBaseStream, r);
            }
            while (this.index < (4 + (this.table >> 10)))
            {
                while (bitk < 3)
                {
                    if (availableIn != 0)
                    {
                        r = 0;
                    }
                    else
                    {
                        this.BitB = bitb;
                        this.BitK = bitk;
                        zipBaseStream.AvailIn = availableIn;
                        zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                        zipBaseStream.NextInIndex = sourceIndex;
                        this.Write = write;
                        return this.InflateFlush(zipBaseStream, r);
                    }
                    availableIn--;
                    bitb |= (zipBaseStream.NextIn[sourceIndex++] & 0xff) << bitk;
                    bitk += 8;
                }
                this.blens[border[this.index++]] = bitb & 7;
                bitb = bitb >> 3;
                bitk -= 3;
            }
            while (this.index < 0x13)
            {
                this.blens[border[this.index++]] = 0;
            }
            this.bb[0] = 7;
            table = InfTree.InflateTreesBits(this.blens, this.bb, this.tb, this.hufts);
            if (table != 0)
            {
                this.blens = null;
                r = table;
                if (r == -3)
                {
                    this.mode = 9;
                }
                this.BitB = bitb;
                this.BitK = bitk;
                zipBaseStream.AvailIn = availableIn;
                zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                zipBaseStream.NextInIndex = sourceIndex;
                this.Write = write;
                return this.InflateFlush(zipBaseStream, r);
            }
            this.index = 0;
            this.mode = 5;
        Label_0794:
            table = this.table;
            if (this.index < ((0x102 + (table & 0x1f)) + ((table >> 5) & 0x1f)))
            {
                table = this.bb[0];
                while (bitk < table)
                {
                    if (availableIn != 0)
                    {
                        r = 0;
                    }
                    else
                    {
                        this.BitB = bitb;
                        this.BitK = bitk;
                        zipBaseStream.AvailIn = availableIn;
                        zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                        zipBaseStream.NextInIndex = sourceIndex;
                        this.Write = write;
                        return this.InflateFlush(zipBaseStream, r);
                    }
                    availableIn--;
                    bitb |= (zipBaseStream.NextIn[sourceIndex++] & 0xff) << bitk;
                    bitk += 8;
                }
                table = this.hufts[((this.tb[0] + (bitb & inflateMask[table])) * 3) + 1];
                int currentValue = this.hufts[((this.tb[0] + (bitb & inflateMask[table])) * 3) + 2];
                if (currentValue < 0x10)
                {
                    bitb = bitb >> table;
                    bitk -= table;
                    this.blens[this.index++] = currentValue;
                }
                else
                {
                    int index = (currentValue == 0x12) ? 7 : (currentValue - 14);
                    int currentIndex = (currentValue == 0x12) ? 11 : 3;
                    while (bitk < (table + index))
                    {
                        if (availableIn != 0)
                        {
                            r = 0;
                        }
                        else
                        {
                            this.BitB = bitb;
                            this.BitK = bitk;
                            zipBaseStream.AvailIn = availableIn;
                            zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                            zipBaseStream.NextInIndex = sourceIndex;
                            this.Write = write;
                            return this.InflateFlush(zipBaseStream, r);
                        }
                        availableIn--;
                        bitb |= (zipBaseStream.NextIn[sourceIndex++] & 0xff) << bitk;
                        bitk += 8;
                    }
                    bitb = bitb >> table;
                    bitk -= table;
                    currentIndex += bitb & inflateMask[index];
                    bitb = bitb >> index;
                    bitk -= index;
                    index = this.index;
                    table = this.table;
                    if (((index + currentIndex) > ((0x102 + (table & 0x1f)) + ((table >> 5) & 0x1f))) || ((currentValue == 0x10) && (index < 1)))
                    {
                        this.blens = null;
                        this.mode = 9;
                        r = -3;
                        this.BitB = bitb;
                        this.BitK = bitk;
                        zipBaseStream.AvailIn = availableIn;
                        zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                        zipBaseStream.NextInIndex = sourceIndex;
                        this.Write = write;
                        return this.InflateFlush(zipBaseStream, r);
                    }
                    currentValue = (currentValue == 0x10) ? this.blens[index - 1] : 0;
                    do
                    {
                        this.blens[index++] = currentValue;
                    }
                    while (--currentIndex != 0);
                    this.index = index;
                }
                goto Label_0794;
            }
            this.tb[0] = -1;
            int[] bl = new int[1];
            int[] bd = new int[1];
            int[] tl = new int[1];
            int[] td = new int[1];
            bl[0] = 9;
            bd[0] = 6;
            table = this.table;
            table = InfTree.InflateTreesDynamic(0x101 + (table & 0x1f), 1 + ((table >> 5) & 0x1f), this.blens, bl, bd, tl, td, this.hufts);
            this.blens = null;
            switch (table)
            {
                case 0:
                    this.codes = new InfCodes(bl[0], bd[0], this.hufts, tl[0], this.hufts, td[0]);
                    this.mode = 6;
                    goto Label_0B34;

                case -3:
                    this.mode = 9;
                    break;
            }
            r = table;
            this.BitB = bitb;
            this.BitK = bitk;
            zipBaseStream.AvailIn = availableIn;
            zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
            zipBaseStream.NextInIndex = sourceIndex;
            this.Write = write;
            return this.InflateFlush(zipBaseStream, r);
        Label_0B34:
            this.BitB = bitb;
            this.BitK = bitk;
            zipBaseStream.AvailIn = availableIn;
            zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
            zipBaseStream.NextInIndex = sourceIndex;
            this.Write = write;
            if ((r = this.codes.ProcessStream(this, zipBaseStream, r)) != 1)
            {
                return this.InflateFlush(zipBaseStream, r);
            }
            r = 0;
            sourceIndex = zipBaseStream.NextInIndex;
            availableIn = zipBaseStream.AvailIn;
            bitb = this.BitB;
            bitk = this.BitK;
            write = this.Write;
            currentAvailableBytes = (write < this.Read) ? ((this.Read - write) - 1) : (this.End - write);
            if (this.last == 0)
            {
                this.mode = 0;
                goto Label_0047;
            }
            this.mode = 7;
        Label_0BF1:
            this.Write = write;
            r = this.InflateFlush(zipBaseStream, r);
            write = this.Write;
            currentAvailableBytes = (write < this.Read) ? ((this.Read - write) - 1) : (this.End - write);
            if (this.Read != this.Write)
            {
                this.BitB = bitb;
                this.BitK = bitk;
                zipBaseStream.AvailIn = availableIn;
                zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
                zipBaseStream.NextInIndex = sourceIndex;
                this.Write = write;
                return this.InflateFlush(zipBaseStream, r);
            }
            this.mode = 8;
        Label_0C86:
            r = 1;
            this.BitB = bitb;
            this.BitK = bitk;
            zipBaseStream.AvailIn = availableIn;
            zipBaseStream.TotalIn += sourceIndex - zipBaseStream.NextInIndex;
            zipBaseStream.NextInIndex = sourceIndex;
            this.Write = write;
            return this.InflateFlush(zipBaseStream, r);
        }

        internal void Reset(ZipBaseStream zipBaseStream, long[] c)
        {
            if (c != null)
            {
                c[0] = this.check;
            }
            if ((this.mode == 4) || (this.mode == 5))
            {
                this.blens = null;
            }
            this.mode = 0;
            this.BitK = 0;
            this.BitB = 0;
            this.Read = this.Write = 0;
            if (this.checkfn != null)
            {
                this.check = zipBaseStream.BaseValueUpdater.UpdateValue(0, null, 0, 0);
                zipBaseStream.Adler = this.check;
            }
        }
    }
    internal class InfTree
    {
        private static int[] cipDext = new int[] 
        { 
            0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 
            7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13
        };
        private static int[] cipDist = new int[] 
        { 
            1, 2, 3, 4, 5, 7, 9, 13, 0x11, 0x19, 0x21, 0x31, 0x41, 0x61, 0x81, 0xc1, 
            0x101, 0x181, 0x201, 0x301, 0x401, 0x601, 0x801, 0xc01, 0x1001, 0x1801, 0x2001, 0x3001, 0x4001, 0x6001
        };
        private static int[] cipLens = new int[] 
        { 
            3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 0x11, 0x13, 0x17, 0x1b, 0x1f, 
            0x23, 0x2b, 0x33, 0x3b, 0x43, 0x53, 0x63, 0x73, 0x83, 0xa3, 0xc3, 0xe3, 0x102, 0, 0
        };
        private static int[] cipLext = new int[] 
        { 
            0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 
            3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0, 0x70, 0x70
        };
        private static int[] fixedTd = new int[] 
        { 
            80, 5, 1, 0x57, 5, 0x101, 0x53, 5, 0x11, 0x5b, 5, 0x1001, 0x51, 5, 5, 0x59, 
            5, 0x401, 0x55, 5, 0x41, 0x5d, 5, 0x4001, 80, 5, 3, 0x58, 5, 0x201, 0x54, 5, 
            0x21, 0x5c, 5, 0x2001, 0x52, 5, 9, 90, 5, 0x801, 0x56, 5, 0x81, 0xc0, 5, 0x6001, 
            80, 5, 2, 0x57, 5, 0x181, 0x53, 5, 0x19, 0x5b, 5, 0x1801, 0x51, 5, 7, 0x59, 
            5, 0x601, 0x55, 5, 0x61, 0x5d, 5, 0x6001, 80, 5, 4, 0x58, 5, 0x301, 0x54, 5, 
            0x31, 0x5c, 5, 0x3001, 0x52, 5, 13, 90, 5, 0xc01, 0x56, 5, 0xc1, 0xc0, 5, 0x6001
        };
        private static int[] fixedTl = new int[] 
        { 
            0x60, 7, 0x100, 0, 8, 80, 0, 8, 0x10, 0x54, 8, 0x73, 0x52, 7, 0x1f, 0, 
            8, 0x70, 0, 8, 0x30, 0, 9, 0xc0, 80, 7, 10, 0, 8, 0x60, 0, 8, 
            0x20, 0, 9, 160, 0, 8, 0, 0, 8, 0x80, 0, 8, 0x40, 0, 9, 0xe0, 
            80, 7, 6, 0, 8, 0x58, 0, 8, 0x18, 0, 9, 0x90, 0x53, 7, 0x3b, 0, 
            8, 120, 0, 8, 0x38, 0, 9, 0xd0, 0x51, 7, 0x11, 0, 8, 0x68, 0, 8, 
            40, 0, 9, 0xb0, 0, 8, 8, 0, 8, 0x88, 0, 8, 0x48, 0, 9, 240, 
            80, 7, 4, 0, 8, 0x54, 0, 8, 20, 0x55, 8, 0xe3, 0x53, 7, 0x2b, 0, 
            8, 0x74, 0, 8, 0x34, 0, 9, 200, 0x51, 7, 13, 0, 8, 100, 0, 8, 
            0x24, 0, 9, 0xa8, 0, 8, 4, 0, 8, 0x84, 0, 8, 0x44, 0, 9, 0xe8, 
            80, 7, 8, 0, 8, 0x5c, 0, 8, 0x1c, 0, 9, 0x98, 0x54, 7, 0x53, 0, 
            8, 0x7c, 0, 8, 60, 0, 9, 0xd8, 0x52, 7, 0x17, 0, 8, 0x6c, 0, 8, 
            0x2c, 0, 9, 0xb8, 0, 8, 12, 0, 8, 140, 0, 8, 0x4c, 0, 9, 0xf8, 
            80, 7, 3, 0, 8, 0x52, 0, 8, 0x12, 0x55, 8, 0xa3, 0x53, 7, 0x23, 0, 
            8, 0x72, 0, 8, 50, 0, 9, 0xc4, 0x51, 7, 11, 0, 8, 0x62, 0, 8, 
            0x22, 0, 9, 0xa4, 0, 8, 2, 0, 8, 130, 0, 8, 0x42, 0, 9, 0xe4, 
            80, 7, 7, 0, 8, 90, 0, 8, 0x1a, 0, 9, 0x94, 0x54, 7, 0x43, 0, 
            8, 0x7a, 0, 8, 0x3a, 0, 9, 0xd4, 0x52, 7, 0x13, 0, 8, 0x6a, 0, 8, 
            0x2a, 0, 9, 180, 0, 8, 10, 0, 8, 0x8a, 0, 8, 0x4a, 0, 9, 0xf4, 
            80, 7, 5, 0, 8, 0x56, 0, 8, 0x16, 0xc0, 8, 0, 0x53, 7, 0x33, 0, 
            8, 0x76, 0, 8, 0x36, 0, 9, 0xcc, 0x51, 7, 15, 0, 8, 0x66, 0, 8, 
            0x26, 0, 9, 0xac, 0, 8, 6, 0, 8, 0x86, 0, 8, 70, 0, 9, 0xec, 
            80, 7, 9, 0, 8, 0x5e, 0, 8, 30, 0, 9, 0x9c, 0x54, 7, 0x63, 0, 
            8, 0x7e, 0, 8, 0x3e, 0, 9, 220, 0x52, 7, 0x1b, 0, 8, 110, 0, 8, 
            0x2e, 0, 9, 0xbc, 0, 8, 14, 0, 8, 0x8e, 0, 8, 0x4e, 0, 9, 0xfc, 
            0x60, 7, 0x100, 0, 8, 0x51, 0, 8, 0x11, 0x55, 8, 0x83, 0x52, 7, 0x1f, 0, 
            8, 0x71, 0, 8, 0x31, 0, 9, 0xc2, 80, 7, 10, 0, 8, 0x61, 0, 8, 
            0x21, 0, 9, 0xa2, 0, 8, 1, 0, 8, 0x81, 0, 8, 0x41, 0, 9, 0xe2, 
            80, 7, 6, 0, 8, 0x59, 0, 8, 0x19, 0, 9, 0x92, 0x53, 7, 0x3b, 0, 
            8, 0x79, 0, 8, 0x39, 0, 9, 210, 0x51, 7, 0x11, 0, 8, 0x69, 0, 8, 
            0x29, 0, 9, 0xb2, 0, 8, 9, 0, 8, 0x89, 0, 8, 0x49, 0, 9, 0xf2, 
            80, 7, 4, 0, 8, 0x55, 0, 8, 0x15, 80, 8, 0x102, 0x53, 7, 0x2b, 0, 
            8, 0x75, 0, 8, 0x35, 0, 9, 0xca, 0x51, 7, 13, 0, 8, 0x65, 0, 8, 
            0x25, 0, 9, 170, 0, 8, 5, 0, 8, 0x85, 0, 8, 0x45, 0, 9, 0xea, 
            80, 7, 8, 0, 8, 0x5d, 0, 8, 0x1d, 0, 9, 0x9a, 0x54, 7, 0x53, 0, 
            8, 0x7d, 0, 8, 0x3d, 0, 9, 0xda, 0x52, 7, 0x17, 0, 8, 0x6d, 0, 8, 
            0x2d, 0, 9, 0xba, 0, 8, 13, 0, 8, 0x8d, 0, 8, 0x4d, 0, 9, 250, 
            80, 7, 3, 0, 8, 0x53, 0, 8, 0x13, 0x55, 8, 0xc3, 0x53, 7, 0x23, 0, 
            8, 0x73, 0, 8, 0x33, 0, 9, 0xc6, 0x51, 7, 11, 0, 8, 0x63, 0, 8, 
            0x23, 0, 9, 0xa6, 0, 8, 3, 0, 8, 0x83, 0, 8, 0x43, 0, 9, 230, 
            80, 7, 7, 0, 8, 0x5b, 0, 8, 0x1b, 0, 9, 150, 0x54, 7, 0x43, 0, 
            8, 0x7b, 0, 8, 0x3b, 0, 9, 0xd6, 0x52, 7, 0x13, 0, 8, 0x6b, 0, 8, 
            0x2b, 0, 9, 0xb6, 0, 8, 11, 0, 8, 0x8b, 0, 8, 0x4b, 0, 9, 0xf6, 
            80, 7, 5, 0, 8, 0x57, 0, 8, 0x17, 0xc0, 8, 0, 0x53, 7, 0x33, 0, 
            8, 0x77, 0, 8, 0x37, 0, 9, 0xce, 0x51, 7, 15, 0, 8, 0x67, 0, 8, 
            0x27, 0, 9, 0xae, 0, 8, 7, 0, 8, 0x87, 0, 8, 0x47, 0, 9, 0xee, 
            80, 7, 9, 0, 8, 0x5f, 0, 8, 0x1f, 0, 9, 0x9e, 0x54, 7, 0x63, 0, 
            8, 0x7f, 0, 8, 0x3f, 0, 9, 0xde, 0x52, 7, 0x1b, 0, 8, 0x6f, 0, 8, 
            0x2f, 0, 9, 190, 0, 8, 15, 0, 8, 0x8f, 0, 8, 0x4f, 0, 9, 0xfe, 
            0x60, 7, 0x100, 0, 8, 80, 0, 8, 0x10, 0x54, 8, 0x73, 0x52, 7, 0x1f, 0, 
            8, 0x70, 0, 8, 0x30, 0, 9, 0xc1, 80, 7, 10, 0, 8, 0x60, 0, 8, 
            0x20, 0, 9, 0xa1, 0, 8, 0, 0, 8, 0x80, 0, 8, 0x40, 0, 9, 0xe1, 
            80, 7, 6, 0, 8, 0x58, 0, 8, 0x18, 0, 9, 0x91, 0x53, 7, 0x3b, 0, 
            8, 120, 0, 8, 0x38, 0, 9, 0xd1, 0x51, 7, 0x11, 0, 8, 0x68, 0, 8, 
            40, 0, 9, 0xb1, 0, 8, 8, 0, 8, 0x88, 0, 8, 0x48, 0, 9, 0xf1, 
            80, 7, 4, 0, 8, 0x54, 0, 8, 20, 0x55, 8, 0xe3, 0x53, 7, 0x2b, 0, 
            8, 0x74, 0, 8, 0x34, 0, 9, 0xc9, 0x51, 7, 13, 0, 8, 100, 0, 8, 
            0x24, 0, 9, 0xa9, 0, 8, 4, 0, 8, 0x84, 0, 8, 0x44, 0, 9, 0xe9, 
            80, 7, 8, 0, 8, 0x5c, 0, 8, 0x1c, 0, 9, 0x99, 0x54, 7, 0x53, 0, 
            8, 0x7c, 0, 8, 60, 0, 9, 0xd9, 0x52, 7, 0x17, 0, 8, 0x6c, 0, 8, 
            0x2c, 0, 9, 0xb9, 0, 8, 12, 0, 8, 140, 0, 8, 0x4c, 0, 9, 0xf9, 
            80, 7, 3, 0, 8, 0x52, 0, 8, 0x12, 0x55, 8, 0xa3, 0x53, 7, 0x23, 0, 
            8, 0x72, 0, 8, 50, 0, 9, 0xc5, 0x51, 7, 11, 0, 8, 0x62, 0, 8, 
            0x22, 0, 9, 0xa5, 0, 8, 2, 0, 8, 130, 0, 8, 0x42, 0, 9, 0xe5, 
            80, 7, 7, 0, 8, 90, 0, 8, 0x1a, 0, 9, 0x95, 0x54, 7, 0x43, 0, 
            8, 0x7a, 0, 8, 0x3a, 0, 9, 0xd5, 0x52, 7, 0x13, 0, 8, 0x6a, 0, 8, 
            0x2a, 0, 9, 0xb5, 0, 8, 10, 0, 8, 0x8a, 0, 8, 0x4a, 0, 9, 0xf5, 
            80, 7, 5, 0, 8, 0x56, 0, 8, 0x16, 0xc0, 8, 0, 0x53, 7, 0x33, 0, 
            8, 0x76, 0, 8, 0x36, 0, 9, 0xcd, 0x51, 7, 15, 0, 8, 0x66, 0, 8, 
            0x26, 0, 9, 0xad, 0, 8, 6, 0, 8, 0x86, 0, 8, 70, 0, 9, 0xed, 
            80, 7, 9, 0, 8, 0x5e, 0, 8, 30, 0, 9, 0x9d, 0x54, 7, 0x63, 0, 
            8, 0x7e, 0, 8, 0x3e, 0, 9, 0xdd, 0x52, 7, 0x1b, 0, 8, 110, 0, 8, 
            0x2e, 0, 9, 0xbd, 0, 8, 14, 0, 8, 0x8e, 0, 8, 0x4e, 0, 9, 0xfd, 
            0x60, 7, 0x100, 0, 8, 0x51, 0, 8, 0x11, 0x55, 8, 0x83, 0x52, 7, 0x1f, 0, 
            8, 0x71, 0, 8, 0x31, 0, 9, 0xc3, 80, 7, 10, 0, 8, 0x61, 0, 8, 
            0x21, 0, 9, 0xa3, 0, 8, 1, 0, 8, 0x81, 0, 8, 0x41, 0, 9, 0xe3, 
            80, 7, 6, 0, 8, 0x59, 0, 8, 0x19, 0, 9, 0x93, 0x53, 7, 0x3b, 0, 
            8, 0x79, 0, 8, 0x39, 0, 9, 0xd3, 0x51, 7, 0x11, 0, 8, 0x69, 0, 8, 
            0x29, 0, 9, 0xb3, 0, 8, 9, 0, 8, 0x89, 0, 8, 0x49, 0, 9, 0xf3, 
            80, 7, 4, 0, 8, 0x55, 0, 8, 0x15, 80, 8, 0x102, 0x53, 7, 0x2b, 0, 
            8, 0x75, 0, 8, 0x35, 0, 9, 0xcb, 0x51, 7, 13, 0, 8, 0x65, 0, 8, 
            0x25, 0, 9, 0xab, 0, 8, 5, 0, 8, 0x85, 0, 8, 0x45, 0, 9, 0xeb, 
            80, 7, 8, 0, 8, 0x5d, 0, 8, 0x1d, 0, 9, 0x9b, 0x54, 7, 0x53, 0, 
            8, 0x7d, 0, 8, 0x3d, 0, 9, 0xdb, 0x52, 7, 0x17, 0, 8, 0x6d, 0, 8, 
            0x2d, 0, 9, 0xbb, 0, 8, 13, 0, 8, 0x8d, 0, 8, 0x4d, 0, 9, 0xfb, 
            80, 7, 3, 0, 8, 0x53, 0, 8, 0x13, 0x55, 8, 0xc3, 0x53, 7, 0x23, 0, 
            8, 0x73, 0, 8, 0x33, 0, 9, 0xc7, 0x51, 7, 11, 0, 8, 0x63, 0, 8, 
            0x23, 0, 9, 0xa7, 0, 8, 3, 0, 8, 0x83, 0, 8, 0x43, 0, 9, 0xe7, 
            80, 7, 7, 0, 8, 0x5b, 0, 8, 0x1b, 0, 9, 0x97, 0x54, 7, 0x43, 0, 
            8, 0x7b, 0, 8, 0x3b, 0, 9, 0xd7, 0x52, 7, 0x13, 0, 8, 0x6b, 0, 8, 
            0x2b, 0, 9, 0xb7, 0, 8, 11, 0, 8, 0x8b, 0, 8, 0x4b, 0, 9, 0xf7, 
            80, 7, 5, 0, 8, 0x57, 0, 8, 0x17, 0xc0, 8, 0, 0x53, 7, 0x33, 0, 
            8, 0x77, 0, 8, 0x37, 0, 9, 0xcf, 0x51, 7, 15, 0, 8, 0x67, 0, 8, 
            0x27, 0, 9, 0xaf, 0, 8, 7, 0, 8, 0x87, 0, 8, 0x47, 0, 9, 0xef, 
            80, 7, 9, 0, 8, 0x5f, 0, 8, 0x1f, 0, 9, 0x9f, 0x54, 7, 0x63, 0, 
            8, 0x7f, 0, 8, 0x3f, 0, 9, 0xdf, 0x52, 7, 0x1b, 0, 8, 0x6f, 0, 8, 
            0x2f, 0, 9, 0xbf, 0, 8, 15, 0, 8, 0x8f, 0, 8, 0x4f, 0, 9, 0xff
        };

        internal static int InflateTreesBits(int[] c, int[] bb, int[] tb, int[] hp)
        {
            int[] hn = new int[1];
            int[] v = new int[0x13];
            int num = Build(c, 0, 0x13, 0x13, null, null, tb, bb, hp, hn, v);
            if (num == -3)
            {
                return num;
            }
            if ((num != -5) && (bb[0] != 0))
            {
                return num;
            }
            return -3;
        }

        internal static int InflateTreesDynamic(int nl, int nd, int[] c, int[] bl, int[] bd, int[] tl, int[] td, int[] hp)
        {
            int[] hn = new int[1];
            int[] v = new int[0x120];
            int num = Build(c, 0, nl, 0x101, cipLens, cipLext, tl, bl, hp, hn, v);
            if ((num != 0) || (bl[0] == 0))
            {
                if (num == -3)
                {
                    return num;
                }
                if (num != -4)
                {
                    num = -3;
                }
                return num;
            }
            num = Build(c, nl, nd, 0, cipDist, cipDext, td, bd, hp, hn, v);
            if ((num == 0) && ((bd[0] != 0) || (nl <= 0x101)))
            {
                return 0;
            }
            if (num == -3)
            {
                return num;
            }
            if (num == -5)
            {
                return -3;
            }
            if (num != -4)
            {
                num = -3;
            }
            return num;
        }

        internal static int InflateTreesFixed(int[] bl, int[] bd, int[][] tl, int[][] td)
        {
            bl[0] = 9;
            bd[0] = 5;
            tl[0] = fixedTl;
            td[0] = fixedTd;
            return 0;
        }

        private static int Build(int[] b, int bindex, int number, int s, int[] d, int[] e, int[] t, int[] m, int[] hp, int[] hn, int[] v)
        {
            int[] numArray = new int[0x10];
            int[] sourceArray = new int[3];
            int[] numArray3 = new int[15];
            int[] numArray4 = new int[0x10];
            int index = 0;
            int num5 = number;
            do
            {
                numArray[b[bindex + index]]++;
                index++;
                num5--;
            }
            while (num5 != 0);
            if (numArray[0] == number)
            {
                t[0] = -1;
                m[0] = 0;
                return 0;
            }
            int num8 = m[0];
            int num6 = 1;
            while (num6 <= 15)
            {
                if (numArray[num6] != 0)
                {
                    break;
                }
                num6++;
            }
            int num7 = num6;
            if (num8 < num6)
            {
                num8 = num6;
            }
            num5 = 15;
            while (num5 != 0)
            {
                if (numArray[num5] != 0)
                {
                    break;
                }
                num5--;
            }
            int num3 = num5;
            if (num8 > num5)
            {
                num8 = num5;
            }
            m[0] = num8;
            int num14 = 1 << num6;
            while (num6 < num5)
            {
                num14 -= numArray[num6];
                if (num14 < 0)
                {
                    return -3;
                }
                num6++;
                num14 = num14 << 1;
            }
            num14 -= numArray[num5];
            if (num14 < 0)
            {
                return -3;
            }
            numArray[num5] += num14;
            numArray4[1] = num6 = 0;
            index = 1;
            int num13 = 2;
            while (--num5 != 0)
            {
                numArray4[num13] = num6 += numArray[index];
                num13++;
                index++;
            }
            num5 = 0;
            index = 0;
            do
            {
                num6 = b[bindex + index];
                if (num6 != 0)
                {
                    v[numArray4[num6]++] = num5;
                }
                index++;
            }
            while (++num5 < number);
            number = numArray4[num3];
            numArray4[0] = num5 = 0;
            index = 0;
            int num4 = -1;
            int num12 = -num8;
            numArray3[0] = 0;
            int num11 = 0;
            int num15 = 0;
            while (num7 <= num3)
            {
                int num2;
                int num = numArray[num7];
                goto Label_03B7;
            Label_01B2:
                num4++;
                num12 += num8;
                num15 = num3 - num12;
                num15 = (num15 > num8) ? num8 : num15;
                if ((num2 = 1 << (num6 = num7 - num12)) > (num + 1))
                {
                    num2 -= num + 1;
                    num13 = num7;
                    if (num6 < num15)
                    {
                        while (++num6 < num15)
                        {
                            if ((num2 = num2 << 1) <= numArray[++num13])
                            {
                                break;
                            }
                            num2 -= numArray[num13];
                        }
                    }
                }
                num15 = 1 << num6;
                if ((hn[0] + num15) > 0x5a0)
                {
                    return -4;
                }
                numArray3[num4] = num11 = hn[0];
                hn[0] += num15;
                if (num4 != 0)
                {
                    numArray4[num4] = num5;
                    sourceArray[0] = (byte)num6;
                    sourceArray[1] = (byte)num8;
                    num6 = num5 >> (num12 - num8);
                    sourceArray[2] = (num11 - numArray3[num4 - 1]) - num6;
                    Array.Copy(sourceArray, 0, hp, (numArray3[num4 - 1] + num6) * 3, 3);
                }
                else
                {
                    t[0] = num11;
                }
            Label_02AD:
                if (num7 > (num12 + num8))
                {
                    goto Label_01B2;
                }
                sourceArray[1] = (byte)(num7 - num12);
                if (index >= number)
                {
                    sourceArray[0] = 0xc0;
                }
                else if (v[index] < s)
                {
                    sourceArray[0] = (v[index] < 0x100) ? ((byte)0) : ((byte)0x60);
                    sourceArray[2] = v[index++];
                }
                else
                {
                    sourceArray[0] = (byte)((e[v[index] - s] + 0x10) + 0x40);
                    sourceArray[2] = d[v[index++] - s];
                }
                num2 = 1 << (num7 - num12);
                num6 = num5 >> num12;
                while (num6 < num15)
                {
                    Array.Copy(sourceArray, 0, hp, (num11 + num6) * 3, 3);
                    num6 += num2;
                }
                num6 = 1 << (num7 - 1);
                while ((num5 & num6) != 0)
                {
                    num5 ^= num6;
                    num6 = num6 >> 1;
                }
                num5 ^= num6;
                for (int i = (1 << num12) - 1; (num5 & i) != numArray4[num4]; i = (1 << num12) - 1)
                {
                    num4--;
                    num12 -= num8;
                }
            Label_03B7:
                if (num-- != 0)
                {
                    goto Label_02AD;
                }
                num7++;
            }
            if ((num14 != 0) && (num3 != 1))
            {
                return -5;
            }
            return 0;
        }
    }
    internal class StaticTree
    {
        private static StaticTree staticBlDesc = new StaticTree(null, Tree.ExtraBlBits, 0, 0x13, 7);
        private static StaticTree staticDDesc = new StaticTree(StaticDTree, Tree.ExtraDBits, 0, 30, 15);
        private static short[] staticDTree = new short[] 
        { 
            0, 5, 0x10, 5, 8, 5, 0x18, 5, 4, 5, 20, 5, 12, 5, 0x1c, 5, 
            2, 5, 0x12, 5, 10, 5, 0x1a, 5, 6, 5, 0x16, 5, 14, 5, 30, 5, 
            1, 5, 0x11, 5, 9, 5, 0x19, 5, 5, 5, 0x15, 5, 13, 5, 0x1d, 5, 
            3, 5, 0x13, 5, 11, 5, 0x1b, 5, 7, 5, 0x17, 5
        };
        private static StaticTree staticLDesc = new StaticTree(StaticLTree, Tree.ExtraLBits, 0x101, 0x11e, 15);
        private static short[] staticLTree = new short[] 
        { 
        12, 8, 140, 8, 0x4c, 8, 0xcc, 8, 0x2c, 8, 0xac, 8, 0x6c, 8, 0xec, 8, 
        0x1c, 8, 0x9c, 8, 0x5c, 8, 220, 8, 60, 8, 0xbc, 8, 0x7c, 8, 0xfc, 8, 
        2, 8, 130, 8, 0x42, 8, 0xc2, 8, 0x22, 8, 0xa2, 8, 0x62, 8, 0xe2, 8, 
        0x12, 8, 0x92, 8, 0x52, 8, 210, 8, 50, 8, 0xb2, 8, 0x72, 8, 0xf2, 8, 
        10, 8, 0x8a, 8, 0x4a, 8, 0xca, 8, 0x2a, 8, 170, 8, 0x6a, 8, 0xea, 8, 
        0x1a, 8, 0x9a, 8, 90, 8, 0xda, 8, 0x3a, 8, 0xba, 8, 0x7a, 8, 250, 8, 
        6, 8, 0x86, 8, 70, 8, 0xc6, 8, 0x26, 8, 0xa6, 8, 0x66, 8, 230, 8, 
        0x16, 8, 150, 8, 0x56, 8, 0xd6, 8, 0x36, 8, 0xb6, 8, 0x76, 8, 0xf6, 8, 
        14, 8, 0x8e, 8, 0x4e, 8, 0xce, 8, 0x2e, 8, 0xae, 8, 110, 8, 0xee, 8, 
        30, 8, 0x9e, 8, 0x5e, 8, 0xde, 8, 0x3e, 8, 190, 8, 0x7e, 8, 0xfe, 8, 
        1, 8, 0x81, 8, 0x41, 8, 0xc1, 8, 0x21, 8, 0xa1, 8, 0x61, 8, 0xe1, 8, 
        0x11, 8, 0x91, 8, 0x51, 8, 0xd1, 8, 0x31, 8, 0xb1, 8, 0x71, 8, 0xf1, 8, 
        9, 8, 0x89, 8, 0x49, 8, 0xc9, 8, 0x29, 8, 0xa9, 8, 0x69, 8, 0xe9, 8, 
        0x19, 8, 0x99, 8, 0x59, 8, 0xd9, 8, 0x39, 8, 0xb9, 8, 0x79, 8, 0xf9, 8, 
        5, 8, 0x85, 8, 0x45, 8, 0xc5, 8, 0x25, 8, 0xa5, 8, 0x65, 8, 0xe5, 8, 
        0x15, 8, 0x95, 8, 0x55, 8, 0xd5, 8, 0x35, 8, 0xb5, 8, 0x75, 8, 0xf5, 8, 
        13, 8, 0x8d, 8, 0x4d, 8, 0xcd, 8, 0x2d, 8, 0xad, 8, 0x6d, 8, 0xed, 8, 
        0x1d, 8, 0x9d, 8, 0x5d, 8, 0xdd, 8, 0x3d, 8, 0xbd, 8, 0x7d, 8, 0xfd, 8, 
        0x13, 9, 0x113, 9, 0x93, 9, 0x193, 9, 0x53, 9, 0x153, 9, 0xd3, 9, 0x1d3, 9, 
        0x33, 9, 0x133, 9, 0xb3, 9, 0x1b3, 9, 0x73, 9, 0x173, 9, 0xf3, 9, 0x1f3, 9, 
        11, 9, 0x10b, 9, 0x8b, 9, 0x18b, 9, 0x4b, 9, 0x14b, 9, 0xcb, 9, 0x1cb, 9, 
        0x2b, 9, 0x12b, 9, 0xab, 9, 0x1ab, 9, 0x6b, 9, 0x16b, 9, 0xeb, 9, 0x1eb, 9, 
        0x1b, 9, 0x11b, 9, 0x9b, 9, 0x19b, 9, 0x5b, 9, 0x15b, 9, 0xdb, 9, 0x1db, 9, 
        0x3b, 9, 0x13b, 9, 0xbb, 9, 0x1bb, 9, 0x7b, 9, 0x17b, 9, 0xfb, 9, 0x1fb, 9, 
        7, 9, 0x107, 9, 0x87, 9, 0x187, 9, 0x47, 9, 0x147, 9, 0xc7, 9, 0x1c7, 9, 
        0x27, 9, 0x127, 9, 0xa7, 9, 0x1a7, 9, 0x67, 9, 0x167, 9, 0xe7, 9, 0x1e7, 9, 
        0x17, 9, 0x117, 9, 0x97, 9, 0x197, 9, 0x57, 9, 0x157, 9, 0xd7, 9, 0x1d7, 9, 
        0x37, 9, 0x137, 9, 0xb7, 9, 0x1b7, 9, 0x77, 9, 0x177, 9, 0xf7, 9, 0x1f7, 9, 
        15, 9, 0x10f, 9, 0x8f, 9, 0x18f, 9, 0x4f, 9, 0x14f, 9, 0xcf, 9, 0x1cf, 9, 
        0x2f, 9, 0x12f, 9, 0xaf, 9, 0x1af, 9, 0x6f, 9, 0x16f, 9, 0xef, 9, 0x1ef, 9, 
        0x1f, 9, 0x11f, 9, 0x9f, 9, 0x19f, 9, 0x5f, 9, 0x15f, 9, 0xdf, 9, 0x1df, 9, 
        0x3f, 9, 0x13f, 9, 0xbf, 9, 0x1bf, 9, 0x7f, 9, 0x17f, 9, 0xff, 9, 0x1ff, 9, 
        0, 7, 0x40, 7, 0x20, 7, 0x60, 7, 0x10, 7, 80, 7, 0x30, 7, 0x70, 7, 
        8, 7, 0x48, 7, 40, 7, 0x68, 7, 0x18, 7, 0x58, 7, 0x38, 7, 120, 7, 
        4, 7, 0x44, 7, 0x24, 7, 100, 7, 20, 7, 0x54, 7, 0x34, 7, 0x74, 7, 
        3, 8, 0x83, 8, 0x43, 8, 0xc3, 8, 0x23, 8, 0xa3, 8, 0x63, 8, 0xe3, 8
     };

        private StaticTree(short[] staticTree, int[] extraBits, int extraBase, int elements, int maxLength)
        {
            this.StaticTreeHelper = staticTree;
            this.ExtraBits = extraBits;
            this.ExtraBase = extraBase;
            this.Elements = elements;
            this.MaxLength = maxLength;
        }

        internal static StaticTree StaticLDesc
        {
            get
            {
                return staticLDesc;
            }
        }
        internal static short[] StaticLTree
        {
            get
            {
                return staticLTree;
            }
        }
        internal static StaticTree StaticBlDesc
        {
            get
            {
                return staticBlDesc;
            }
        }
        internal static StaticTree StaticDDesc
        {
            get
            {
                return staticDDesc;
            }
        }
        internal static short[] StaticDTree
        {
            get
            {
                return staticDTree;
            }
        }
        internal int Elements { get; set; }
        internal int ExtraBase { get; set; }
        internal int[] ExtraBits { get; set; }
        internal short[] StaticTreeHelper { get; set; }
        internal int MaxLength { get; set; }
    }
    internal class Tree
    {
        private static int[] baseDist = new int[] 
        { 
            0, 1, 2, 3, 4, 6, 8, 12, 0x10, 0x18, 0x20, 0x30, 0x40, 0x60, 0x80, 0xc0, 
            0x100, 0x180, 0x200, 0x300, 0x400, 0x600, 0x800, 0xc00, 0x1000, 0x1800, 0x2000, 0x3000, 0x4000, 0x6000
        };

        private static int[] baseLength = new int[] 
        { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 12, 14, 0x10, 20, 0x18, 0x1c, 
            0x20, 40, 0x30, 0x38, 0x40, 80, 0x60, 0x70, 0x80, 160, 0xc0, 0xe0, 0
        };

        private static byte[] bilOrder = new byte[] 
        { 
            0x10, 0x11, 0x12, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 
            14, 1, 15
        };

        private static int[] extraBlBits = new int[] 
        { 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            2, 3, 7
        };

        private static int[] extraDBits = new int[] 
        { 
            0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 
            7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13
        };

        private static int[] extraLBits = new int[] 
        { 
            0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 
            3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0
        };

        private static byte[] lengthCode = new byte[] 
        { 
            0, 1, 2, 3, 4, 5, 6, 7, 8, 8, 9, 9, 10, 10, 11, 11, 
            12, 12, 12, 12, 13, 13, 13, 13, 14, 14, 14, 14, 15, 15, 15, 15, 
            0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 
            0x12, 0x12, 0x12, 0x12, 0x12, 0x12, 0x12, 0x12, 0x13, 0x13, 0x13, 0x13, 0x13, 0x13, 0x13, 0x13, 
            20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 
            0x15, 0x15, 0x15, 0x15, 0x15, 0x15, 0x15, 0x15, 0x15, 0x15, 0x15, 0x15, 0x15, 0x15, 0x15, 0x15, 
            0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 
            0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 
            0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 
            0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 
            0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 
            0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 
            0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 
            0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 
            0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 
            0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1c
        };

        private static byte[] distCode = new byte[] 
        { 
            0, 1, 2, 3, 4, 4, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 
            8, 8, 8, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 9, 9, 9, 
            10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 
            11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 
            13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 
            13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 
            14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 
            14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 
            14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 
            14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 
            15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 
            15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 
            15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 
            15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 
            0, 0, 0x10, 0x11, 0x12, 0x12, 0x13, 0x13, 20, 20, 20, 20, 0x15, 0x15, 0x15, 0x15, 
            0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 0x17, 
            0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 0x18, 
            0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 0x19, 
            0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 
            0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 0x1a, 
            0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 
            0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 0x1b, 
            0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 
            0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 
            0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 
            0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 0x1c, 
            0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 
            0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 
            0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 
            0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d, 0x1d
        };

        internal static byte[] LengthCode
        {
            get
            {
                return lengthCode;
            }
        }
        internal static int[] BaseDist
        {
            get
            {
                return baseDist;
            }
        }
        internal static int[] BaseLength
        {
            get
            {
                return baseLength;
            }
        }
        internal static int[] ExtraBlBits
        {
            get
            {
                return extraBlBits;
            }
        }
        internal static byte[] BilOrder
        {
            get
            {
                return bilOrder;
            }
        }
        internal static int[] ExtraDBits
        {
            get
            {
                return extraDBits;
            }
        }
        internal static int[] ExtraLBits
        {
            get
            {
                return extraLBits;
            }
        }
        internal short[] DynTree { get; set; }
        internal int MaxCode { get; set; }
        internal StaticTree StatDesc { get; set; }

        internal static int Code(int dist)
        {
            if (dist >= 0x100)
            {
                return distCode[0x100 + (dist >> 7)];
            }
            return distCode[dist];
        }

        internal void BuildTree(DeflateManager s)
        {
            int num2;
            int num5;
            short[] tree = this.DynTree;
            short[] numArray2 = this.StatDesc.StaticTreeHelper;
            int elems = this.StatDesc.Elements;
            int num4 = -1;
            s.HeapLength = 0;
            s.HeapMax = 0x23d;
            for (num2 = 0; num2 < elems; num2++)
            {
                if (tree[num2 * 2] != 0)
                {
                    s.Heap[++s.HeapLength] = num4 = num2;
                    s.Depth[num2] = 0;
                }
                else
                {
                    tree[(num2 * 2) + 1] = 0;
                }
            }
            while (s.HeapLength < 2)
            {
                num5 = s.Heap[++s.HeapLength] = (num4 < 2) ? ++num4 : 0;
                tree[num5 * 2] = 1;
                s.Depth[num5] = 0;
                s.OptLength--;
                if (numArray2 != null)
                {
                    s.StaticLength -= numArray2[(num5 * 2) + 1];
                }
            }
            this.MaxCode = num4;
            num2 = s.HeapLength / 2;
            while (num2 >= 1)
            {
                s.DownHeap(tree, num2);
                num2--;
            }
            num5 = elems;
            do
            {
                num2 = s.Heap[1];
                s.Heap[1] = s.Heap[s.HeapLength--];
                s.DownHeap(tree, 1);
                int index = s.Heap[1];
                s.Heap[--s.HeapMax] = num2;
                s.Heap[--s.HeapMax] = index;
                tree[num5 * 2] = (short)(tree[num2 * 2] + tree[index * 2]);
                s.Depth[num5] = (byte)(System.Math.Max(s.Depth[num2], s.Depth[index]) + 1);
                tree[(num2 * 2) + 1] = tree[(index * 2) + 1] = (short)num5;
                s.Heap[1] = num5++;
                s.DownHeap(tree, 1);
            }
            while (s.HeapLength >= 2);
            s.Heap[--s.HeapMax] = s.Heap[1];
            this.GenerateBytesLength(s);
            GenerateCodes(tree, num4, s.BilCount);
        }

        private static void GenerateCodes(short[] tree, int max_code, short[] bil_count)
        {
            short[] numArray = new short[0x10];
            short num = 0;
            for (int i = 1; i <= 15; i++)
            {
                numArray[i] = num = (short)((num + bil_count[i - 1]) << 1);
            }
            for (int j = 0; j <= max_code; j++)
            {
                int index = tree[(j * 2) + 1];
                if (index != 0)
                {
                    short num5;
                    numArray[index] = (short)((num5 = numArray[index]) + 1);
                    tree[j * 2] = (short)Reverse(num5, index);
                }
            }
        }

        private static int Reverse(int code, int len)
        {
            int num = 0;
            do
            {
                num |= code & 1;
                code = code >> 1;
                num = num << 1;
            }
            while (--len > 0);
            return num >> 1;
        }

        private void GenerateBytesLength(DeflateManager s)
        {
            int num4;
            short[] numArray = this.DynTree;
            short[] numArray2 = this.StatDesc.StaticTreeHelper;
            int[] numArray3 = this.StatDesc.ExtraBits;
            int num = this.StatDesc.ExtraBase;
            int index = this.StatDesc.MaxLength;
            int num9 = 0;
            int num6 = 0;
            while (num6 <= 15)
            {
                s.BilCount[num6] = 0;
                num6++;
            }
            numArray[(s.Heap[s.HeapMax] * 2) + 1] = 0;
            int num3 = s.HeapMax + 1;
            while (num3 < 0x23d)
            {
                num4 = s.Heap[num3];
                num6 = numArray[(numArray[(num4 * 2) + 1] * 2) + 1] + 1;
                if (num6 > index)
                {
                    num6 = index;
                    num9++;
                }
                numArray[(num4 * 2) + 1] = (short)num6;
                if (num4 <= this.MaxCode)
                {
                    s.BilCount[num6] = (short)(s.BilCount[num6] + 1);
                    int num7 = 0;
                    if (num4 >= num)
                    {
                        num7 = numArray3[num4 - num];
                    }
                    short num8 = numArray[num4 * 2];
                    s.OptLength += num8 * (num6 + num7);
                    if (numArray2 != null)
                    {
                        s.StaticLength += num8 * (numArray2[(num4 * 2) + 1] + num7);
                    }
                }
                num3++;
            }
            if (num9 != 0)
            {
                do
                {
                    num6 = index - 1;
                    while (s.BilCount[num6] == 0)
                    {
                        num6--;
                    }
                    s.BilCount[num6] = (short)(s.BilCount[num6] - 1);
                    s.BilCount[num6 + 1] = (short)(s.BilCount[num6 + 1] + 2);
                    s.BilCount[index] = (short)(s.BilCount[index] - 1);
                    num9 -= 2;
                }
                while (num9 > 0);
                for (num6 = index; num6 != 0; num6--)
                {
                    num4 = s.BilCount[num6];
                    while (num4 != 0)
                    {
                        int num5 = s.Heap[--num3];
                        if (num5 <= this.MaxCode)
                        {
                            if (numArray[(num5 * 2) + 1] != num6)
                            {
                                s.OptLength += (num6 - numArray[(num5 * 2) + 1]) * numArray[num5 * 2];
                                numArray[(num5 * 2) + 1] = (short)num6;
                            }
                            num4--;
                        }
                    }
                }
            }
        }
    }
    internal class DeflateManager
    {
        private static Config[] configTable = new Config[] { new Config(0, 0, 0, 0, 0), new Config(4, 4, 8, 4, 1), new Config(4, 5, 0x10, 8, 1), new Config(4, 6, 0x20, 0x20, 1), new Config(4, 4, 0x10, 0x10, 2), new Config(8, 0x10, 0x20, 0x20, 2), new Config(8, 0x10, 0x80, 0x80, 2), new Config(8, 0x20, 0x80, 0x100, 2), new Config(0x20, 0x80, 0x102, 0x400, 2), new Config(0x20, 0x102, 0x102, 0x1000, 2) };
        private short[] bilCount = new short[0x10];
        private byte[] depth = new byte[0x23d];
        private int[] heap = new int[0x23d];
        private short bitBuf;
        private int bitValid;
        private Tree bilDesc = new Tree();
        private int blockStart;
        private short[] bilTree = new short[0x4e];
        private byte dataType;
        private int dipBuf;
        private Tree dipDesc = new Tree();
        private short[] dynDtree = new short[0x7a];
        private short[] dynLTree = new short[0x47a];
        private int goodMatch;
        private int hashBits;
        private int hashMask;
        private int hashShift;
        private int hashSize;
        private short[] head;
        private int insH;
        private int lastEobLength;
        private int lastFlush;
        private int lastLit;
        private int lipBuffer;
        private Tree lipDesc = new Tree();
        private int level;
        private int litBufSize;
        private int lookahead;
        private int matchAvailable;
        private int matches;
        private int matchLength;
        private int matchStart;
        private int maxChainLength;
        private int maxLazyMatch;
        private int niceMatch;
        private int pendingBufferSize;
        private short[] prev;
        private int prevLength;
        private int prevMatch;
        private int status;
        private int strategy;
        private ZipBaseStream stream;
        private int strStart;
        private int winBits;
        private byte[] window;
        private int windowSize;
        private int winMask;
        private int winSize;

        internal int[] Heap
        {
            get
            {
                return this.heap;
            }
            set
            {
                this.heap = value;
            }
        }
        internal byte[] Depth
        {
            get
            {
                return this.depth;
            }
            set
            {
                this.depth = value;
            }
        }
        internal short[] BilCount
        {
            get
            {
                return this.bilCount;
            }
            set
            {
                this.bilCount = value;
            }
        }
        internal int HeapLength { get; set; }
        internal int HeapMax { get; set; }
        internal int NonHeader { get; set; }
        internal int OptLength { get; set; }
        internal int Pending { get; set; }
        internal byte[] PendingBuffer { get; set; }
        internal int PendingOut { get; set; }
        internal int StaticLength { get; set; }

        internal int Deflate(ZipBaseStream strm, int flush)
        {
            if ((flush > 4) || (flush < 0))
            {
                return -2;
            }
            if (((strm.NextOut == null) || ((strm.NextIn == null) && (strm.AvailIn != 0))) || ((this.status == 0x29a) && (flush != 4)))
            {
                return -2;
            }
            if (strm.AvailOut == 0)
            {
                return -5;
            }
            this.stream = strm;
            int num = this.lastFlush;
            this.lastFlush = flush;
            if (this.status == 0x2a)
            {
                int b = (8 + ((this.winBits - 8) << 4)) << 8;
                int num3 = ((this.level - 1) & 0xff) >> 1;
                if (num3 > 3)
                {
                    num3 = 3;
                }
                b |= num3 << 6;
                if (this.strStart != 0)
                {
                    b |= 0x20;
                }
                b += 0x1f - (b % 0x1f);
                this.status = 0x71;
                this.PutShortMSB(b);
                if (this.strStart != 0)
                {
                    this.PutShortMSB((int)(strm.Adler >> 0x10));
                    this.PutShortMSB((int)(strm.Adler & 0xffffL));
                }
                strm.Adler = strm.BaseValueUpdater.UpdateValue(0, null, 0, 0);
            }
            if (this.Pending != 0)
            {
                strm.FlushPending();
                if (strm.AvailOut == 0)
                {
                    this.lastFlush = -1;
                    return 0;
                }
            }
            else if (((strm.AvailIn == 0) && (flush <= num)) && (flush != 4))
            {
                return 1;
            }
            if ((this.status == 0x29a) && (strm.AvailIn != 0))
            {
                return -5;
            }
            if (((strm.AvailIn != 0) || (this.lookahead != 0)) || ((flush != 0) && (this.status != 0x29a)))
            {
                int num4 = -1;
                switch (configTable[this.level].Func)
                {
                    case 0:
                        num4 = this.StoredDeflate(flush);
                        break;

                    case 1:
                        num4 = this.FastDeflate(flush);
                        break;

                    case 2:
                        num4 = this.SlowDeflate(flush);
                        break;
                }
                switch (num4)
                {
                    case 2:
                    case 3:
                        this.status = 0x29a;
                        break;
                }
                if ((num4 == 0) || (num4 == 2))
                {
                    if (strm.AvailOut == 0)
                    {
                        this.lastFlush = -1;
                    }
                    return 0;
                }
                if (num4 == 1)
                {
                    if (flush == 1)
                    {
                        this.Align();
                    }
                    else
                    {
                        this.StoredBlock(0, 0, false);
                        if (flush == 3)
                        {
                            for (int i = 0; i < this.hashSize; i++)
                            {
                                this.head[i] = 0;
                            }
                        }
                    }
                    strm.FlushPending();
                    if (strm.AvailOut == 0)
                    {
                        this.lastFlush = -1;
                        return 0;
                    }
                }
            }
            if (flush == 4)
            {
                if (this.NonHeader != 0)
                {
                    return 1;
                }
                this.PutShortMSB((int)(strm.Adler >> 0x10));
                this.PutShortMSB((int)(strm.Adler & 0xffffL));
                strm.FlushPending();
                this.NonHeader = -1;
                if (this.Pending == 0)
                {
                    return 1;
                }
            }
            return 0;
        }

        internal int DeflateEnd()
        {
            if (((this.status != 0x2a) && (this.status != 0x71)) && (this.status != 0x29a))
            {
                return -2;
            }
            this.PendingBuffer = null;
            this.head = null;
            this.prev = null;
            this.window = null;
            if (this.status != 0x71)
            {
                return 0;
            }
            return -3;
        }

        internal int DeflateInitiliaze(ZipBaseStream strm, int level, int method, int windowBits, int memLevel, int strategy)
        {
            int num = 0;
            if (level < 0)
            {
                level = 6;
            }
            if (windowBits < 0)
            {
                num = 1;
                windowBits = -windowBits;
            }
            if ((((memLevel < 1) || (memLevel > 9)) || ((method != 8) || (windowBits < 8))) || (((windowBits > 15) || (level < 0)) || (((level > 9) || (strategy < 0)) || (strategy > 2))))
            {
                return -2;
            }
            this.InitParams(strm, level, windowBits, memLevel, strategy, num);
            return this.ResetDeflate(strm);
        }

        internal void DownHeap(short[] tree, int k)
        {
            int heapNumber = this.heap[k];
            for (int i = k << 1; i <= this.HeapLength; i = i << 1)
            {
                if ((i < this.HeapLength) && IsTreeSmaller(tree, this.heap[i + 1], this.heap[i], this.Depth))
                {
                    i++;
                }
                if (IsTreeSmaller(tree, heapNumber, this.heap[i], this.Depth))
                {
                    break;
                }
                this.heap[k] = this.heap[i];
                k = i;
            }
            this.heap[k] = heapNumber;
        }

        private static bool IsTreeSmaller(short[] tree, int number, int m, byte[] depth)
        {
            return (tree[number * 2] < tree[m * 2]) || ((tree[number * 2] == tree[m * 2]) && (depth[number] <= depth[m]));
        }

        private void Align()
        {
            this.SendBits(2, 3);
            this.SendCode(0x100, StaticTree.StaticLTree);
            this.Flush();
            if ((((1 + this.lastEobLength) + 10) - this.bitValid) < 9)
            {
                this.SendBits(2, 3);
                this.SendCode(0x100, StaticTree.StaticLTree);
                this.Flush();
            }
            this.lastEobLength = 7;
        }

        private int BuildTree()
        {
            this.ScanTree(this.dynLTree, this.lipDesc.MaxCode);
            this.ScanTree(this.dynDtree, this.dipDesc.MaxCode);
            this.bilDesc.BuildTree(this);
            int index = 0x12;
            while (index >= 3)
            {
                if (this.bilTree[(Tree.BilOrder[index] * 2) + 1] != 0)
                {
                    break;
                }
                index--;
            }
            this.OptLength += (((3 * (index + 1)) + 5) + 5) + 4;
            return index;
        }

        private void CompressBlock(short[] ltree, short[] dtree)
        {
            int number = 0;
            if (this.lastLit != 0)
            {
                do
                {
                    int dist = ((this.PendingBuffer[this.dipBuf + (number * 2)] << 8) & 0xff00) | (this.PendingBuffer[(this.dipBuf + (number * 2)) + 1] & 0xff);
                    int c = this.PendingBuffer[this.lipBuffer + number] & 0xff;
                    number++;
                    if (dist == 0)
                    {
                        this.SendCode(c, ltree);
                    }
                    else
                    {
                        int index = Tree.LengthCode[c];
                        this.SendCode((index + 0x100) + 1, ltree);
                        int length = Tree.ExtraLBits[index];
                        if (length != 0)
                        {
                            c -= Tree.BaseLength[index];
                            this.SendBits(c, length);
                        }
                        dist--;
                        index = Tree.Code(dist);
                        this.SendCode(index, dtree);
                        length = Tree.ExtraDBits[index];
                        if (length != 0)
                        {
                            dist -= Tree.BaseDist[index];
                            this.SendBits(dist, length);
                        }
                    }
                }
                while (number < this.lastLit);
            }
            this.SendCode(0x100, ltree);
            this.lastEobLength = ltree[0x201];
        }

        private void CopyBlock(int buf, int len, bool header)
        {
            this.WindUp();
            this.lastEobLength = 8;
            if (header)
            {
                this.PutShort((short)len);
                this.PutShort((short)~len);
            }
            this.PutByte(this.window, buf, len);
        }

        private int FastDeflate(int flush)
        {
            int number = 0;
            do
            {
                bool flag;
                do
                {
                    if (this.lookahead < 0x106)
                    {
                        this.FillWindow();
                        if ((this.lookahead < 0x106) && (flush == 0))
                        {
                            return 0;
                        }
                        if (this.lookahead == 0)
                        {
                            this.FlushBlockOnly(flush == 4);
                            if (this.stream.AvailOut == 0)
                            {
                                if (flush == 4)
                                {
                                    return 2;
                                }
                                return 0;
                            }
                            if (flush != 4)
                            {
                                return 1;
                            }
                            return 3;
                        }
                    }
                    if (this.lookahead >= 3)
                    {
                        this.insH = ((this.insH << this.hashShift) ^ (this.window[this.strStart + 2] & 0xff)) & this.hashMask;
                        number = this.head[this.insH] & 0xffff;
                        this.prev[this.strStart & this.winMask] = this.head[this.insH];
                        this.head[this.insH] = (short)this.strStart;
                    }
                    if (((number != 0) && ((this.strStart - number) <= (this.winSize - 0x106))) && (this.strategy != 2))
                    {
                        this.matchLength = this.LongestMatch(number);
                    }
                    if (this.matchLength >= 3)
                    {
                        flag = this.Tally(this.strStart - this.matchStart, this.matchLength - 3);
                        this.lookahead -= this.matchLength;
                        if ((this.matchLength <= this.maxLazyMatch) && (this.lookahead >= 3))
                        {
                            this.matchLength--;
                            do
                            {
                                this.strStart++;
                                this.insH = ((this.insH << this.hashShift) ^ (this.window[this.strStart + 2] & 0xff)) & this.hashMask;
                                number = this.head[this.insH] & 0xffff;
                                this.prev[this.strStart & this.winMask] = this.head[this.insH];
                                this.head[this.insH] = (short)this.strStart;
                            }
                            while (--this.matchLength != 0);
                            this.strStart++;
                        }
                        else
                        {
                            this.strStart += this.matchLength;
                            this.matchLength = 0;
                            this.insH = this.window[this.strStart] & 0xff;
                            this.insH = ((this.insH << this.hashShift) ^ (this.window[this.strStart + 1] & 0xff)) & this.hashMask;
                        }
                    }
                    else
                    {
                        flag = this.Tally(0, this.window[this.strStart] & 0xff);
                        this.lookahead--;
                        this.strStart++;
                    }
                }
                while (!flag);
                this.FlushBlockOnly(false);
            }
            while (this.stream.AvailOut != 0);
            return 0;
        }

        private void FillWindow()
        {
            int hashSize;
            int winSize;
            bool finished = false;
            do
            {
                winSize = (this.windowSize - this.lookahead) - this.strStart;
                if (((winSize == 0) && (this.strStart == 0)) && (this.lookahead == 0))
                {
                    winSize = this.winSize;
                }
                else if (winSize == -1)
                {
                    winSize--;
                }
                else if (this.strStart >= ((this.winSize + this.winSize) - 262))
                {
                    int currentWSize;
                    Array.Copy(this.window, this.winSize, this.window, 0, this.winSize);
                    this.matchStart -= this.winSize;
                    this.strStart -= this.winSize;
                    this.blockStart -= this.winSize;
                    hashSize = this.hashSize;
                    int index = hashSize;
                    do
                    {
                        currentWSize = this.head[--index] & 0xffff;
                        this.head[index] = (currentWSize >= this.winSize) ? ((short)(currentWSize - this.winSize)) : ((short)0);
                    }
                    while (--hashSize != 0);
                    hashSize = this.winSize;
                    index = hashSize;
                    do
                    {
                        currentWSize = this.prev[--index] & 0xffff;
                        this.prev[index] = (currentWSize >= this.winSize) ? ((short)(currentWSize - this.winSize)) : ((short)0);
                    }
                    while (--hashSize != 0);
                    winSize += this.winSize;
                }
                if (this.stream.AvailIn != 0)
                {
                    hashSize = this.stream.ReadBuffer(this.window, this.strStart + this.lookahead, winSize);
                    this.lookahead += hashSize;
                    if (this.lookahead >= 3)
                    {
                        this.insH = this.window[this.strStart] & 0xff;
                        this.insH = ((this.insH << this.hashShift) ^ (this.window[this.strStart + 1] & 0xff)) & this.hashMask;
                    }
                    if ((this.lookahead < 0x106) && (this.stream.AvailIn != 0))
                    {
                        finished = true;
                    }
                    else
                    {
                        finished = false;
                    }
                }
            }
            while (finished);
        }

        private void Flush()
        {
            if (this.bitValid == 0x10)
            {
                this.PutShort(this.bitBuf);
                this.bitBuf = 0;
                this.bitValid = 0;
            }
            else if (this.bitValid >= 8)
            {
                this.PutByte((byte)this.bitBuf);
                this.bitBuf = (short)(this.bitBuf >> 8);
                this.bitValid -= 8;
            }
        }

        private void FlushBlock(int buf, int stored_len, bool eof)
        {
            int number;
            int number2;
            int number3 = 0;
            if (this.level > 0)
            {
                if (this.dataType == 2)
                {
                    this.SetDataType();
                }
                this.lipDesc.BuildTree(this);
                this.dipDesc.BuildTree(this);
                number3 = this.BuildTree();
                number = ((this.OptLength + 3) + 7) >> 3;
                number2 = ((this.StaticLength + 3) + 7) >> 3;
                if (number2 <= number)
                {
                    number = number2;
                }
            }
            else
            {
                number = number2 = stored_len + 5;
            }
            if (((stored_len + 4) <= number) && (buf != -1))
            {
                this.StoredBlock(buf, stored_len, eof);
            }
            else if (number2 == number)
            {
                this.SendBits(2 + (eof ? 1 : 0), 3);
                this.CompressBlock(StaticTree.StaticLTree, StaticTree.StaticDTree);
            }
            else
            {
                this.SendBits(4 + (eof ? 1 : 0), 3);
                this.SendAllTrees(this.lipDesc.MaxCode + 1, this.dipDesc.MaxCode + 1, number3 + 1);
                this.CompressBlock(this.dynLTree, this.dynDtree);
            }
            this.InitBlock();
            if (eof)
            {
                this.WindUp();
            }
        }

        private void FlushBlockOnly(bool eof)
        {
            this.FlushBlock((this.blockStart >= 0) ? this.blockStart : -1, this.strStart - this.blockStart, eof);
            this.blockStart = this.strStart;
            this.stream.FlushPending();
        }

        private void InitBlock()
        {
            for (int i = 0; i < 0x11e; i++)
            {
                this.dynLTree[i * 2] = 0;
            }
            for (int j = 0; j < 30; j++)
            {
                this.dynDtree[j * 2] = 0;
            }
            for (int k = 0; k < 0x13; k++)
            {
                this.bilTree[k * 2] = 0;
            }
            this.dynLTree[0x200] = 1;
            this.OptLength = this.StaticLength = 0;
            this.lastLit = this.matches = 0;
        }

        private void InitParams(ZipBaseStream strm, int level, int windowBits, int memLevel, int strategy, int num)
        {
            strm.DeflateState = this;
            this.NonHeader = num;
            this.winBits = windowBits;
            this.winSize = 1 << this.winBits;
            this.winMask = this.winSize - 1;
            this.hashBits = memLevel + 7;
            this.hashSize = 1 << this.hashBits;
            this.hashMask = this.hashSize - 1;
            this.hashShift = ((this.hashBits + 3) - 1) / 3;
            this.window = new byte[this.winSize * 2];
            this.prev = new short[this.winSize];
            this.head = new short[this.hashSize];
            this.litBufSize = 1 << (memLevel + 6);
            this.PendingBuffer = new byte[this.litBufSize * 4];
            this.pendingBufferSize = this.litBufSize * 4;
            this.dipBuf = this.litBufSize / 2;
            this.lipBuffer = 3 * this.litBufSize;
            this.level = level;
            this.strategy = strategy;
        }

        private void LmInit()
        {
            this.windowSize = 2 * this.winSize;
            this.head[this.hashSize - 1] = 0;
            for (int i = 0; i < (this.hashSize - 1); i++)
            {
                this.head[i] = 0;
            }
            this.maxLazyMatch = configTable[this.level].MaxLazy;
            this.goodMatch = configTable[this.level].GoodLength;
            this.niceMatch = configTable[this.level].NiceLength;
            this.maxChainLength = configTable[this.level].MaxChain;
            this.strStart = 0;
            this.blockStart = 0;
            this.lookahead = 0;
            this.matchLength = this.prevLength = 2;
            this.matchAvailable = 0;
            this.insH = 0;
        }

        private int LongestMatch(int cur_match)
        {
            int num = this.maxChainLength;
            int strstart = this.strStart;
            int previousLength = this.prevLength;
            int currentAvailableSize = (this.strStart > (this.winSize - 262)) ? (this.strStart - (this.winSize - 262)) : 0;
            int lookahead = this.niceMatch;
            int winMask = this.winMask;
            int currentStrStart = this.strStart + 0x102;
            byte previousElement = this.window[(strstart + previousLength) - 1];
            byte currentElement = this.window[strstart + previousLength];
            if (this.prevLength >= this.goodMatch)
            {
                num = num >> 2;
            }
            if (lookahead > this.lookahead)
            {
                lookahead = this.lookahead;
            }
            do
            {
                int index = cur_match;
                if (((this.window[index + previousLength] == currentElement) && (this.window[(index + previousLength) - 1] == previousElement)) && ((this.window[index] == this.window[strstart]) && (this.window[++index] == this.window[strstart + 1])))
                {
                    strstart += 2;
                    index++;
                    while ((((this.window[++strstart] == this.window[++index]) && (this.window[++strstart] == this.window[++index])) && ((this.window[++strstart] == this.window[++index]) && (this.window[++strstart] == this.window[++index]))) && ((((this.window[++strstart] == this.window[++index]) && (this.window[++strstart] == this.window[++index])) && ((this.window[++strstart] == this.window[++index]) && (this.window[++strstart] == this.window[++index]))) && ((strstart < currentStrStart) && (index < currentStrStart))))
                    {
                    }
                    int availableStrLength = 258 - (currentStrStart - strstart);
                    strstart = currentStrStart - 0x102;
                    if (availableStrLength > previousLength)
                    {
                        this.matchStart = cur_match;
                        previousLength = availableStrLength;
                        if (availableStrLength >= lookahead)
                        {
                            break;
                        }
                        previousElement = this.window[(strstart + previousLength) - 1];
                        currentElement = this.window[strstart + previousLength];
                    }
                }
            }
            while (((cur_match = this.prev[cur_match & winMask] & 0xffff) > currentAvailableSize) && (--num != 0));
            if (previousLength <= this.lookahead)
            {
                return previousLength;
            }
            return this.lookahead;
        }

        private int ProcessSlowDeflate(int flush, ref int num)
        {
        Label_0002:
            do
            {
                bool flag;
                do
                {
                    if (this.lookahead < 0x106)
                    {
                        this.FillWindow();
                        if ((this.lookahead < 0x106) && (flush == 0))
                        {
                            return 0;
                        }
                        if (this.lookahead == 0)
                        {
                            if (this.matchAvailable != 0)
                            {
                                flag = this.Tally(0, this.window[this.strStart - 1] & 0xff);
                                this.matchAvailable = 0;
                            }
                            this.FlushBlockOnly(flush == 4);
                            if (this.stream.AvailOut == 0)
                            {
                                if (flush == 4)
                                {
                                    return 2;
                                }
                                return 0;
                            }
                            if (flush != 4)
                            {
                                return 1;
                            }
                            return 3;
                        }
                    }
                    if (this.lookahead >= 3)
                    {
                        this.insH = ((this.insH << this.hashShift) ^ (this.window[this.strStart + 2] & 0xff)) & this.hashMask;
                        num = this.head[this.insH] & 0xffff;
                        this.prev[this.strStart & this.winMask] = this.head[this.insH];
                        this.head[this.insH] = (short)this.strStart;
                    }
                    this.prevLength = this.matchLength;
                    this.prevMatch = this.matchStart;
                    this.matchLength = 2;
                    if (((num != 0) && (this.prevLength < this.maxLazyMatch)) && ((this.strStart - num) <= (this.winSize - 0x106)))
                    {
                        if (this.strategy != 2)
                        {
                            this.matchLength = this.LongestMatch(num);
                        }
                        if ((this.matchLength <= 5) && ((this.strategy == 1) || ((this.matchLength == 3) && ((this.strStart - this.matchStart) > 0x1000))))
                        {
                            this.matchLength = 2;
                        }
                    }
                    if ((this.prevLength < 3) || (this.matchLength > this.prevLength))
                    {
                        if (this.matchAvailable != 0)
                        {
                            if (this.Tally(0, this.window[this.strStart - 1] & 0xff))
                            {
                                this.FlushBlockOnly(false);
                            }
                            this.strStart++;
                            this.lookahead--;
                            if (this.stream.AvailOut == 0)
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            this.matchAvailable = 1;
                            this.strStart++;
                            this.lookahead--;
                        }
                        goto Label_0002;
                    }
                    int currentStringPosition = (this.strStart + this.lookahead) - 3;
                    flag = this.Tally((this.strStart - 1) - this.prevMatch, this.prevLength - 3);
                    this.lookahead -= this.prevLength - 1;
                    this.prevLength -= 2;
                    do
                    {
                        if (++this.strStart <= currentStringPosition)
                        {
                            this.insH = ((this.insH << this.hashShift) ^ (this.window[this.strStart + 2] & 0xff)) & this.hashMask;
                            num = this.head[this.insH] & 0xffff;
                            this.prev[this.strStart & this.winMask] = this.head[this.insH];
                            this.head[this.insH] = (short)this.strStart;
                        }
                    }
                    while (--this.prevLength != 0);
                    this.matchAvailable = 0;
                    this.matchLength = 2;
                    this.strStart++;
                }
                while (!flag);
                this.FlushBlockOnly(false);
            }
            while (this.stream.AvailOut != 0);
            return 0;
        }

        private void PutByte(byte[] p, int start, int len)
        {
            Array.Copy(p, start, this.PendingBuffer, this.Pending, len);
            this.Pending += len;
        }

        private void PutByte(byte c)
        {
            this.PendingBuffer[this.Pending++] = c;
        }

        private void PutShort(int w)
        {
            this.PutByte((byte)w);
            this.PutByte((byte)(w >> 8));
        }

        private void PutShortMSB(int b)
        {
            this.PutByte((byte)(b >> 8));
            this.PutByte((byte)b);
        }

        private int ResetDeflate(ZipBaseStream strm)
        {
            strm.TotalIn = strm.TotalOut = 0;
            strm.DataType = 2;
            this.Pending = 0;
            this.PendingOut = 0;
            if (this.NonHeader < 0)
            {
                this.NonHeader = 0;
            }
            this.status = (this.NonHeader != 0) ? 113 : 42;
            strm.Adler = strm.BaseValueUpdater.UpdateValue(0, null, 0, 0);
            this.lastFlush = 0;
            this.TrInit();
            this.LmInit();
            return 0;
        }

        private void ScanTree(short[] tree, int max_code)
        {
            int num = -1;
            int secondTreeElement = tree[1];
            int num2 = 0;
            int num3 = 7;
            int num4 = 4;
            if (secondTreeElement == 0)
            {
                num3 = 138;
                num4 = 3;
            }
            tree[((max_code + 1) * 2) + 1] = -1;
            for (int i = 0; i <= max_code; i++)
            {
                int num5 = secondTreeElement;
                secondTreeElement = tree[((i + 1) * 2) + 1];
                if ((++num2 >= num3) || (num5 != secondTreeElement))
                {
                    if (num2 < num4)
                    {
                        this.bilTree[num5 * 2] = (short)(this.bilTree[num5 * 2] + ((short)num2));
                    }
                    else if (num5 != 0)
                    {
                        if (num5 != num)
                        {
                            this.bilTree[num5 * 2] = (short)(this.bilTree[num5 * 2] + 1);
                        }
                        this.bilTree[0x20] = (short)(this.bilTree[0x20] + 1);
                    }
                    else if (num2 <= 10)
                    {
                        this.bilTree[0x22] = (short)(this.bilTree[0x22] + 1);
                    }
                    else
                    {
                        this.bilTree[0x24] = (short)(this.bilTree[0x24] + 1);
                    }
                    num2 = 0;
                    num = num5;
                    if (secondTreeElement == 0)
                    {
                        num3 = 0x8a;
                        num4 = 3;
                    }
                    else if (num5 == secondTreeElement)
                    {
                        num3 = 6;
                        num4 = 3;
                    }
                    else
                    {
                        num3 = 7;
                        num4 = 4;
                    }
                }
            }
        }

        private void SendAllTrees(int lcodes, int dcodes, int blcodes)
        {
            this.SendBits(lcodes - 257, 5);
            this.SendBits(dcodes - 1, 5);
            this.SendBits(blcodes - 4, 4);
            for (int i = 0; i < blcodes; i++)
            {
                this.SendBits(this.bilTree[(Tree.BilOrder[i] * 2) + 1], 3);
            }
            this.SendTree(this.dynLTree, lcodes - 1);
            this.SendTree(this.dynDtree, dcodes - 1);
        }

        private void SendBits(int value, int length)
        {
            int num = length;
            if (this.bitValid > (16 - num))
            {
                int num2 = value;
                this.bitBuf = (short)(((ushort)this.bitBuf) | ((ushort)((num2 << this.bitValid) & 65535)));
                this.PutShort(this.bitBuf);
                this.bitBuf = (short)(num2 >> (0x10 - this.bitValid));
                this.bitValid += num - 0x10;
            }
            else
            {
                this.bitBuf = (short)(((ushort)this.bitBuf) | ((ushort)((value << this.bitValid) & 65535)));
                this.bitValid += num;
            }
        }

        private void SendCode(int c, short[] tree)
        {
            this.SendBits(tree[c * 2] & 65535, tree[(c * 2) + 1] & 65535);
        }

        private void SendTree(short[] tree, int max_code)
        {
            int num = -1;
            int secondTreeElement = tree[1];
            int num2 = 0;
            int num3 = 7;
            int num4 = 4;
            if (secondTreeElement == 0)
            {
                num3 = 0x8a;
                num4 = 3;
            }
            for (int i = 0; i <= max_code; i++)
            {
                int c = secondTreeElement;
                secondTreeElement = tree[((i + 1) * 2) + 1];
                if ((++num2 >= num3) || (c != secondTreeElement))
                {
                    if (num2 < num4)
                    {
                        do
                        {
                            this.SendCode(c, this.bilTree);
                        }
                        while (--num2 != 0);
                    }
                    else if (c != 0)
                    {
                        if (c != num)
                        {
                            this.SendCode(c, this.bilTree);
                            num2--;
                        }
                        this.SendCode(0x10, this.bilTree);
                        this.SendBits(num2 - 3, 2);
                    }
                    else if (num2 <= 10)
                    {
                        this.SendCode(0x11, this.bilTree);
                        this.SendBits(num2 - 3, 3);
                    }
                    else
                    {
                        this.SendCode(0x12, this.bilTree);
                        this.SendBits(num2 - 11, 7);
                    }
                    num2 = 0;
                    num = c;
                    if (secondTreeElement == 0)
                    {
                        num3 = 138;
                        num4 = 3;
                    }
                    else if (c == secondTreeElement)
                    {
                        num3 = 6;
                        num4 = 3;
                    }
                    else
                    {
                        num3 = 7;
                        num4 = 4;
                    }
                }
            }
        }

        private void SetDataType()
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            while (num < 7)
            {
                num3 += this.dynLTree[num * 2];
                num++;
            }
            while (num < 128)
            {
                num2 += this.dynLTree[num * 2];
                num++;
            }
            while (num < 256)
            {
                num3 += this.dynLTree[num * 2];
                num++;
            }
            this.dataType = (num3 > (num2 >> 2)) ? ((byte)0) : ((byte)1);
        }

        private int SlowDeflate(int flush)
        {
            int num = 0;
            return this.ProcessSlowDeflate(flush, ref num);
        }

        private void StoredBlock(int buf, int stored_len, bool eof)
        {
            this.SendBits(eof ? 1 : 0, 3);
            this.CopyBlock(buf, stored_len, true);
        }

        private int StoredDeflate(int flush)
        {
            int num = 0xffff;
            if (num > (this.pendingBufferSize - 5))
            {
                num = this.pendingBufferSize - 5;
            }
            do
            {
                do
                {
                    if (this.lookahead <= 1)
                    {
                        this.FillWindow();
                        if ((this.lookahead == 0) && (flush == 0))
                        {
                            return 0;
                        }
                        if (this.lookahead == 0)
                        {
                            this.FlushBlockOnly(flush == 4);
                            if (this.stream.AvailOut == 0)
                            {
                                if (flush != 4)
                                {
                                    return 0;
                                }
                                return 2;
                            }
                            if (flush != 4)
                            {
                                return 1;
                            }
                            return 3;
                        }
                    }
                    this.strStart += this.lookahead;
                    this.lookahead = 0;
                    int num2 = this.blockStart + num;
                    if ((this.strStart == 0) || (this.strStart >= num2))
                    {
                        this.lookahead = this.strStart - num2;
                        this.strStart = num2;
                        this.FlushBlockOnly(false);
                        if (this.stream.AvailOut == 0)
                        {
                            return 0;
                        }
                    }
                }
                while ((this.strStart - this.blockStart) < (this.winSize - 0x106));
                this.FlushBlockOnly(false);
            }
            while (this.stream.AvailOut != 0);
            return 0;
        }

        private bool Tally(int dist, int lc)
        {
            this.PendingBuffer[this.dipBuf + (this.lastLit * 2)] = (byte)(dist >> 8);
            this.PendingBuffer[(this.dipBuf + (this.lastLit * 2)) + 1] = (byte)dist;
            this.PendingBuffer[this.lipBuffer + this.lastLit] = (byte)lc;
            this.lastLit++;
            if (dist == 0)
            {
                this.dynLTree[lc * 2] = (short)(this.dynLTree[lc * 2] + 1);
            }
            else
            {
                this.matches++;
                dist--;
                this.dynLTree[((Tree.LengthCode[lc] + 0x100) + 1) * 2] = (short)(this.dynLTree[((Tree.LengthCode[lc] + 0x100) + 1) * 2] + 1);
                this.dynDtree[Tree.Code(dist) * 2] = (short)(this.dynDtree[Tree.Code(dist) * 2] + 1);
            }
            if (((this.lastLit & 0x1fff) == 0) && (this.level > 2))
            {
                int num = this.lastLit * 8;
                int num2 = this.strStart - this.blockStart;
                for (int i = 0; i < 30; i++)
                {
                    num += this.dynDtree[i * 2] * (5 + Tree.ExtraDBits[i]);
                }
                num = num >> 3;
                if ((this.matches < (this.lastLit / 2)) && (num < (num2 / 2)))
                {
                    return true;
                }
            }
            return this.lastLit == (this.litBufSize - 1);
        }

        private void TrInit()
        {
            this.lipDesc.DynTree = this.dynLTree;
            this.lipDesc.StatDesc = StaticTree.StaticLDesc;
            this.dipDesc.DynTree = this.dynDtree;
            this.dipDesc.StatDesc = StaticTree.StaticDDesc;
            this.bilDesc.DynTree = this.bilTree;
            this.bilDesc.StatDesc = StaticTree.StaticBlDesc;
            this.bitBuf = 0;
            this.bitValid = 0;
            this.lastEobLength = 8;
            this.InitBlock();
        }

        private void WindUp()
        {
            if (this.bitValid > 8)
            {
                this.PutShort(this.bitBuf);
            }
            else if (this.bitValid > 0)
            {
                this.PutByte((byte)this.bitBuf);
            }
            this.bitBuf = 0;
            this.bitValid = 0;
        }

        private class Config
        {
            internal Config(int goodLength, int maxLazy, int niceLength, int maxChain, int func)
            {
                this.GoodLength = goodLength;
                this.MaxLazy = maxLazy;
                this.NiceLength = niceLength;
                this.MaxChain = maxChain;
                this.Func = func;
            }

            internal int Func { get; set; }
            internal int GoodLength { get; set; }
            internal int MaxChain { get; set; }
            internal int MaxLazy { get; set; }
            internal int NiceLength { get; set; }
        }
    }
    internal class InflateManager
    {
        private InfBlocks blocks;
        private int method;
        private int mode;
        private long need;
        private int nowrap;
        private long[] was = new long[1];
        private int windowbits;

        internal static int Inflate(ZipBaseStream zipBaseStream, int flag)
        {
            if (((zipBaseStream == null) || (zipBaseStream.InflateState == null)) || (zipBaseStream.NextIn == null))
            {
                return -2;
            }
            flag = (flag == 4) ? -5 : 0;
            int internalFlag = -5;
        Label_0024:
            switch (zipBaseStream.InflateState.mode)
            {
                case 0:
                    if (zipBaseStream.AvailIn != 0)
                    {
                        internalFlag = flag;
                        zipBaseStream.AvailIn--;
                        zipBaseStream.TotalIn += 1;
                        zipBaseStream.InflateState.method = zipBaseStream.NextIn[zipBaseStream.NextInIndex++];
                        if ((zipBaseStream.InflateState.method & 15) != 8)
                        {
                            zipBaseStream.InflateState.mode = 13;
                            goto Label_0024;
                        }
                        if (((zipBaseStream.InflateState.method >> 4) + 8) > zipBaseStream.InflateState.windowbits)
                        {
                            zipBaseStream.InflateState.mode = 13;
                            goto Label_0024;
                        }
                        zipBaseStream.InflateState.mode = 1;
                        break;
                    }
                    return internalFlag;

                case 1:
                    break;

                case 2:
                    goto Label_01F4;

                case 3:
                    goto Label_0258;

                case 4:
                    goto Label_02C3;

                case 5:
                    goto Label_032D;

                case 6:
                    zipBaseStream.InflateState.mode = 13;
                    return -2;

                case 7:
                    internalFlag = zipBaseStream.InflateState.blocks.ProcessStream(zipBaseStream, internalFlag);
                    if (internalFlag != -3)
                    {
                        if (internalFlag == 0)
                        {
                            internalFlag = flag;
                        }
                        if (internalFlag != 1)
                        {
                            return internalFlag;
                        }
                        internalFlag = flag;
                        zipBaseStream.InflateState.blocks.Reset(zipBaseStream, zipBaseStream.InflateState.was);
                        if (zipBaseStream.InflateState.nowrap != 0)
                        {
                            zipBaseStream.InflateState.mode = 12;
                            goto Label_0024;
                        }
                        zipBaseStream.InflateState.mode = 8;
                        goto Label_0459;
                    }
                    zipBaseStream.InflateState.mode = 13;
                    goto Label_0024;

                case 8:
                    goto Label_0459;

                case 9:
                    goto Label_04BE;

                case 10:
                    goto Label_052A;

                case 11:
                    goto Label_0595;

                case 12:
                    goto Label_0643;

                case 13:
                    return -3;

                default:
                    return -2;
            }
            if (zipBaseStream.AvailIn == 0)
            {
                return internalFlag;
            }
            internalFlag = flag;
            zipBaseStream.AvailIn--;
            zipBaseStream.TotalIn += 1;
            int num2 = zipBaseStream.NextIn[zipBaseStream.NextInIndex++] & 0xff;
            if ((((zipBaseStream.InflateState.method << 8) + num2) % 0x1f) != 0)
            {
                zipBaseStream.InflateState.mode = 13;
                goto Label_0024;
            }
            if ((num2 & 0x20) == 0)
            {
                zipBaseStream.InflateState.mode = 7;
                goto Label_0024;
            }
            zipBaseStream.InflateState.mode = 2;
        Label_01F4:
            if (zipBaseStream.AvailIn == 0)
            {
                return internalFlag;
            }
            internalFlag = flag;
            zipBaseStream.AvailIn--;
            zipBaseStream.TotalIn += 1;
            zipBaseStream.InflateState.need = (zipBaseStream.NextIn[zipBaseStream.NextInIndex++] & 0xff) << 0x18;
            zipBaseStream.InflateState.mode = 3;
        Label_0258:
            if (zipBaseStream.AvailIn == 0)
            {
                return internalFlag;
            }
            internalFlag = flag;
            zipBaseStream.AvailIn--;
            zipBaseStream.TotalIn += 1;
            zipBaseStream.InflateState.need += (zipBaseStream.NextIn[zipBaseStream.NextInIndex++] & 0xff) << 0x10;
            zipBaseStream.InflateState.mode = 4;
        Label_02C3:
            if (zipBaseStream.AvailIn == 0)
            {
                return internalFlag;
            }
            internalFlag = flag;
            zipBaseStream.AvailIn--;
            zipBaseStream.TotalIn += 1;
            zipBaseStream.InflateState.need += (zipBaseStream.NextIn[zipBaseStream.NextInIndex++] & 0xff) << 8;
            zipBaseStream.InflateState.mode = 5;
        Label_032D:
            if (zipBaseStream.AvailIn == 0)
            {
                return internalFlag;
            }
            internalFlag = flag;
            zipBaseStream.AvailIn--;
            zipBaseStream.TotalIn += 1;
            zipBaseStream.InflateState.need += zipBaseStream.NextIn[zipBaseStream.NextInIndex++] & 0xff;
            zipBaseStream.Adler = zipBaseStream.InflateState.need;
            zipBaseStream.InflateState.mode = 6;
            return 2;
        Label_0459:
            if (zipBaseStream.AvailIn == 0)
            {
                return internalFlag;
            }
            internalFlag = flag;
            zipBaseStream.AvailIn--;
            zipBaseStream.TotalIn += 1;
            zipBaseStream.InflateState.need = (zipBaseStream.NextIn[zipBaseStream.NextInIndex++] & 0xff) << 0x18;
            zipBaseStream.InflateState.mode = 9;
        Label_04BE:
            if (zipBaseStream.AvailIn == 0)
            {
                return internalFlag;
            }
            internalFlag = flag;
            zipBaseStream.AvailIn--;
            zipBaseStream.TotalIn += 1;
            zipBaseStream.InflateState.need += (zipBaseStream.NextIn[zipBaseStream.NextInIndex++] & 0xff) << 0x10;
            zipBaseStream.InflateState.mode = 10;
        Label_052A:
            if (zipBaseStream.AvailIn == 0)
            {
                return internalFlag;
            }
            internalFlag = flag;
            zipBaseStream.AvailIn--;
            zipBaseStream.TotalIn += 1;
            zipBaseStream.InflateState.need += (zipBaseStream.NextIn[zipBaseStream.NextInIndex++] & 0xff) << 8;
            zipBaseStream.InflateState.mode = 11;
        Label_0595:
            if (zipBaseStream.AvailIn == 0)
            {
                return internalFlag;
            }
            internalFlag = flag;
            zipBaseStream.AvailIn--;
            zipBaseStream.TotalIn += 1;
            zipBaseStream.InflateState.need += zipBaseStream.NextIn[zipBaseStream.NextInIndex++] & 0xff;
            if (((int)zipBaseStream.InflateState.was[0]) != ((int)zipBaseStream.InflateState.need))
            {
                zipBaseStream.InflateState.mode = 13;
                goto Label_0024;
            }
            zipBaseStream.InflateState.mode = 12;
        Label_0643:
            return 1;
        }

        internal int InitInflate(ZipBaseStream zipBaseStream, int windowLength)
        {
            this.blocks = null;
            this.nowrap = 0;
            if (windowLength < 0)
            {
                windowLength = -windowLength;
                this.nowrap = 1;
            }
            if ((windowLength < 8) || (windowLength > 15))
            {
                this.InflateEnd(zipBaseStream);
                return -2;
            }
            this.windowbits = windowLength;
            zipBaseStream.InflateState.blocks = new InfBlocks(zipBaseStream, (zipBaseStream.InflateState.nowrap != 0) ? null : this, 1 << windowLength);
            ResetInflate(zipBaseStream);
            return 0;
        }

        private static int ResetInflate(ZipBaseStream zipBaseStream)
        {
            if ((zipBaseStream == null) || (zipBaseStream.InflateState == null))
            {
                return -2;
            }
            zipBaseStream.TotalOut = 0;
            zipBaseStream.TotalIn = 0;
            zipBaseStream.InflateState.mode = (zipBaseStream.InflateState.nowrap != 0) ? 7 : 0;
            zipBaseStream.InflateState.blocks.Reset(zipBaseStream, null);
            return 0;
        }

        private int InflateEnd(ZipBaseStream zipBaseStream)
        {
            if (this.blocks != null)
            {
                this.blocks.Free(zipBaseStream);
            }
            this.blocks = null;
            return 0;
        }
    }
    internal class ZipBaseStream
    {
        private bool crc32;

        internal ZipBaseStream(bool crc32)
        {
            this.crc32 = crc32;
            if (crc32)
            {
                this.BaseValueUpdater = new CRC32();
            }
            else
            {
                this.BaseValueUpdater = new Adler32();
            }
        }

        internal long Adler { get; set; }
        internal int AvailIn { get; set; }
        internal int AvailOut { get; set; }
        internal IValueUpdater BaseValueUpdater { get; set; }
        internal int DataType { get; set; }
        internal DeflateManager DeflateState { get; set; }
        internal InflateManager InflateState { get; set; }
        internal byte[] NextIn { get; set; }
        internal int NextInIndex { get; set; }
        internal byte[] NextOut { get; set; }
        internal int NextOutIndex { get; set; }
        internal long TotalIn { get; set; }
        internal long TotalOut { get; set; }

        internal int Deflate(int flush)
        {
            if (this.DeflateState == null)
            {
                return -2;
            }
            return this.DeflateState.Deflate(this, flush);
        }

        internal int EndDeflate()
        {
            if (this.DeflateState == null)
            {
                return -2;
            }
            int num = this.DeflateState.DeflateEnd();
            this.DeflateState = null;
            return num;
        }

        internal void FlushPending()
        {
            int pending = this.DeflateState.Pending;
            if (pending > this.AvailOut)
            {
                pending = this.AvailOut;
            }
            if (pending != 0)
            {
                this.Flush(pending);
            }
        }

        internal int Inflate(int flush)
        {
            if (this.InflateState == null)
            {
                return -2;
            }
            return InflateManager.Inflate(this, flush);
        }

        internal int InflateInit(int bits)
        {
            this.InflateState = new InflateManager();
            return this.InflateState.InitInflate(this, bits);
        }

        internal int InitDeflate(int level)
        {
            this.DeflateState = new DeflateManager();
            return this.DeflateState.DeflateInitiliaze(this, level, 8, 15, 8, 0);
        }

        internal int InitDeflate(int level, int bits)
        {
            this.DeflateState = new DeflateManager();
            return this.DeflateState.DeflateInitiliaze(this, level, 8, bits, 8, 0);
        }

        internal int ReadBuffer(byte[] buf, int start, int size)
        {
            int len = this.AvailIn;
            if (len > size)
            {
                len = size;
            }
            if (len == 0)
            {
                return 0;
            }
            this.AvailIn -= len;
            if ((this.DeflateState.NonHeader == 0) || this.crc32)
            {
                this.Adler = this.BaseValueUpdater.UpdateValue(this.Adler, this.NextIn, this.NextInIndex, len);
            }
            Array.Copy(this.NextIn, this.NextInIndex, buf, start, len);
            this.NextInIndex += len;
            this.TotalIn += len;
            return len;
        }

        private void Flush(int pending)
        {
            Array.Copy(this.DeflateState.PendingBuffer, this.DeflateState.PendingOut, this.NextOut, this.NextOutIndex, pending);
            this.NextOutIndex += pending;
            this.DeflateState.PendingOut += pending;
            this.TotalOut += pending;
            this.AvailOut -= pending;
            this.DeflateState.Pending -= pending;
            if (this.DeflateState.Pending == 0)
            {
                this.DeflateState.PendingOut = 0;
            }
        }
    }
    #endregion

    #endregion

    #region -- Public Classes --

    /// <summary>
    /// Represents a stream that can write into a compressed stream.
    /// </summary>
    public class ZipOutputStream : Stream
    {
        private Stream baseStream;
        private byte[] buffer;
        private bool cancel;
        private DateTime dateTime;
        private string fileName;
        private bool finished;
        private long flushed;
        private bool isZipEntryOutputStream;
        private ZipPackage owner;
        private bool ownsBaseStream;
        private byte[] recordedByte;
        private string recordName;
        private string tempFileName;
        private ZipBaseStream zipBaseStream;

        /// <summary>
        /// Initializes a new instance of the ZipOutputStream class.
        /// </summary>
        /// <param name="baseStream">
        /// The stream that will be compressed.
        /// </param>
        public ZipOutputStream(Stream baseStream)
            : this(baseStream, ZipCompression.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ZipOutputStream class.
        /// </summary>
        /// <param name="baseStream">
        /// The stream that will be compressed.
        /// </param>
        /// /// <param name="method">
        /// The compression method.
        /// </param>
        public ZipOutputStream(Stream baseStream, ZipCompression method)
        {
            this.recordedByte = new byte[1];
            this.InitializeStream(baseStream, method, true, false);
        }

        internal ZipOutputStream(bool isZipEntryOutputStream)
        {
            this.isZipEntryOutputStream = isZipEntryOutputStream;
        }

        /// <summary>
        /// The stream that is compressed.
        /// </summary>
        public Stream BaseStream
        {
            get
            {
                return this.baseStream;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports reading.
        /// </summary>
        /// <returns>true if the stream supports reading; otherwise, false.</returns>
        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        /// <summary> 
        /// Gets a value indicating whether the current stream supports seeking.
        /// </summary>
        /// <returns>true if the stream supports seeking; otherwise, false.</returns>
        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports writing.
        /// </summary>
        /// <returns>true if the stream supports writing; otherwise, false.</returns>
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the checksum of the compressed stream.
        /// </summary>
        public int Checksum
        {
            get
            {
                return (int)this.zipBaseStream.Adler;
            }
        }

        /// <summary>
        /// Gets the compressed size of the stream.
        /// </summary>
        public int CompressedSize
        {
            get
            {
                return (int)this.zipBaseStream.TotalOut;
            }
        }

        /// <summary>
        /// Gets the length in bytes of the stream.
        /// </summary>
        /// <returns>A long value representing the length of the stream in bytes.</returns>
        public override long Length
        {
            get
            {
                return this.baseStream.Length;
            }
        }

        /// <summary>
        /// Gets the position within the current stream. Set is not supported.
        /// </summary>
        /// <returns>The current position within the stream.</returns>
        public override long Position
        {
            get
            {
                return this.baseStream.Position;
            }
            set
            {
                throw new NotSupportedException("Seek not supported.");
            }
        }

        /// <summary>
        /// Gets the uncompressed size of the stream.
        /// </summary>
        public int UncompressedSize
        {
            get
            {
                return (int)this.zipBaseStream.TotalIn;
            }
        }

        /// <summary>
        /// Stops the compression of the stream.
        /// </summary>
        public void Cancel()
        {
            this.cancel = true;
        }

        /// <summary
        /// >Closes the current stream and releases any resources (such as sockets
        /// and file handles) associated with the current stream.
        /// </summary>
        public override void Close()
        {
            this.FinishWriting();
            if (this.ownsBaseStream)
            {
                this.baseStream.Close();
            }

            if (this.isZipEntryOutputStream)
            {
                try
                {
                    if (!this.cancel)
                    {
                        this.BaseStream.Position = 0;
                        this.AddCompressed();
                    }
                }
                finally
                {
                    this.BaseStream.Close();
                    if (this.BaseStream is FileStream)
                    {
                        File.Delete(this.tempFileName);
                    }
                }
            }
        }

        /// <summary>
        /// Clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        public override void Flush()
        {
            this.FlushStream();
            this.baseStream.Flush();
        }

        /// <summary>
        /// Reading is not supported.
        /// </summary>
        public override int Read(byte[] buf, int offset, int count)
        {
            throw new NotSupportedException("Read not supported.");
        }

        /// <summary>
        /// Seeking is not supported.
        /// </summary>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Seek not supported.");
        }

        /// <summary>
        /// Setting length is not supported.
        /// </summary>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("SetLength not supported.");
        }

        /// <summary>
        /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number
        /// of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count"/>
        /// bytes from <paramref name="buffer"/> to the current stream. </param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/>
        /// at which to begin copying bytes to the current stream. </param>
        /// <param name="count">The number of bytes to be written to the current stream.
        /// </param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.WriteInternal(buffer, offset, count);
        }

        /// <summary>
        /// Writes a byte to the current position in the stream and advances the
        /// position within the stream by one byte.
        /// </summary>
        /// <param name="value">The byte to write to the stream. </param>
        public override void WriteByte(byte value)
        {
            this.recordedByte[0] = value;
            this.WriteInternal(this.recordedByte, 0, 1);
        }

        internal static Stream CreateZipEntryOutputStream(ZipCompression method, ZipPackage zipFile, string fileName, string recordName, DateTime dateTime, bool memory)
        {
            ZipOutputStream outputStream = new ZipOutputStream(true);
            Stream stream;
            if (memory)
            {
                outputStream.tempFileName = null;
                stream = new MemoryStream();
            }
            else
            {
                outputStream.tempFileName = zipFile.TempFileName;
                if ((outputStream.tempFileName == null) || (outputStream.tempFileName.Length == 0))
                {
                    outputStream.tempFileName = Path.GetTempFileName();
                }
                stream = new FileStream(outputStream.tempFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            }

            outputStream.owner = zipFile;
            outputStream.recordName = recordName;
            outputStream.fileName = fileName;
            outputStream.cancel = false;
            outputStream.dateTime = dateTime;
            outputStream.InitializeStream(stream, method, false, true);
            outputStream.ownsBaseStream = false;

            return outputStream;
        }

        private void AddCompressed()
        {
            ZipPackageEntry item = new ZipPackageEntry(this.owner);
            this.recordName = item.SetFlags(this.recordName);
            item.SetName(this.fileName, this.dateTime);
            int index = this.owner.IndexOf(item.FileNameInZip);
            if (index >= 0)
            {
                this.owner.RemoveIndex(index);
            }
            var records = this.owner.ZipPackageEntries as List<ZipPackageEntry>;
            if (records.Count >= 65535)
            {
                throw new ArgumentException("Zip cannot contain more than 65,535 records!");
            }

            this.WriteCompressedData(item);
        }

        private void FinishWriting()
        {
            if (!this.finished)
            {
                int availableBufferLength;
                int deflateNum = this.zipBaseStream.Deflate(4);
                while (deflateNum != 1)
                {
                    if (deflateNum != 0)
                    {
                        throw new Exception("Error deflating!");
                    }
                    availableBufferLength = this.buffer.Length - this.zipBaseStream.AvailOut;
                    if (availableBufferLength > 0)
                    {
                        this.baseStream.Write(this.buffer, 0, availableBufferLength);
                        this.zipBaseStream.AvailOut = this.buffer.Length;
                        this.zipBaseStream.NextOut = this.buffer;
                        this.zipBaseStream.NextOutIndex = 0;
                    }

                    deflateNum = this.zipBaseStream.Deflate(4);
                }
                if (this.zipBaseStream.EndDeflate() != 0)
                {
                    throw new Exception("Error deflating!");
                }
                availableBufferLength = this.buffer.Length - this.zipBaseStream.AvailOut;
                if (availableBufferLength > 0)
                {
                    this.baseStream.Write(this.buffer, 0, availableBufferLength);
                }
                this.finished = true;
            }
        }

        private void FlushStream()
        {
            if (this.finished)
                return;

            int deflateNumber = this.zipBaseStream.Deflate(2);
            while (deflateNumber != 1)
            {
                if (deflateNumber != 0)
                {
                    throw new Exception("Error deflating!");
                }
                int count = this.buffer.Length - this.zipBaseStream.AvailOut;
                if (count > 0)
                {
                    this.baseStream.Write(this.buffer, 0, count);
                    this.zipBaseStream.AvailOut = this.buffer.Length;
                    this.zipBaseStream.NextOut = this.buffer;
                    this.zipBaseStream.NextOutIndex = 0;
                }
                deflateNumber = this.zipBaseStream.Deflate(2);
            }
        }

        private void InitializeStream(Stream baseStream, ZipCompression method, bool header, bool crc32)
        {
            if ((baseStream == null) || !baseStream.CanWrite)
            {
                throw new ArgumentException("Non-writeable stream!");
            }

            this.InitializeStreamAttributes(baseStream, crc32);

            if ((header ? this.zipBaseStream.InitDeflate((int)method) : this.zipBaseStream.InitDeflate((int)method, -15)) != 0)
            {
                throw new Exception("Initialization failure.");
            }
        }

        private void InitializeStreamAttributes(Stream baseStream, bool crc32)
        {
            this.buffer = new byte[16384];
            this.baseStream = baseStream;
            this.ownsBaseStream = true;
            this.zipBaseStream = new ZipBaseStream(crc32);
            this.zipBaseStream.NextOut = this.buffer;
            this.zipBaseStream.AvailOut = this.buffer.Length;
            this.zipBaseStream.NextOutIndex = 0;
        }

        private void WriteCompressedData(ZipPackageEntry item)
        {
            Stream fileStream = null;
            try
            {
                fileStream = this.owner.OpenFile(this.owner.FileName, false, true);
                this.owner.Headers.Add(item);
                item.Offset = this.owner.BytesBeforeZip + this.owner.Offset;
                fileStream.SetLength((long)item.Offset);
                fileStream.Position = item.Offset;
                item.WriteLocalHeader(fileStream);
                Stream baseStream = this.BaseStream;
                baseStream.Position = 0;
                this.StreamCopy(fileStream, baseStream, (uint)baseStream.Length);
                item.CompressedSizeInternal = this.CompressedSize;
                item.UncompressedSizeInternal = this.UncompressedSize;
                item.Crc32 = this.Checksum;
                fileStream.Position = item.Offset;
                item.WriteLocalHeader(fileStream);
                fileStream.Position = fileStream.Length;
                this.owner.Offset = (uint)fileStream.Position;
                this.owner.WriteCentralDir(fileStream);
            }
            finally
            {
                this.owner.Close(fileStream);
            }
        }

        private void StreamCopy(Stream dstStream, Stream srcStream, uint len)
        {
            byte[] buffer = new byte[32768];
            int count = 0;
            int readBytes = 0;
            for (uint i = len; i > 0; i -= (uint)readBytes)
            {
                count = (int)System.Math.Min((long)buffer.Length, (long)i);
                readBytes = srcStream.Read(buffer, 0, count);
                if (readBytes == 0)
                    break;
                dstStream.Write(buffer, 0, readBytes);
            }

            dstStream.Flush();
        }

        private void WriteInternal(byte[] buf, int offset, int count)
        {
            if (count == 0)
                return;

            if (this.finished)
            {
                throw new Exception("Writing not allowed!");
            }

            this.zipBaseStream.NextIn = buf;
            this.zipBaseStream.NextInIndex = offset;
            this.zipBaseStream.AvailIn = count;
            do
            {
                int flush = 0;
                if ((this.zipBaseStream.TotalIn - this.flushed) > 512000)
                {
                    flush = 2;
                    this.flushed = this.zipBaseStream.TotalIn;
                }
                if (this.zipBaseStream.Deflate(flush) != 0)
                {
                    throw new Exception("Error deflating!");
                }
                int availableLength = this.buffer.Length - this.zipBaseStream.AvailOut;
                if (availableLength > 0)
                {
                    this.baseStream.Write(this.buffer, 0, availableLength);
                    this.zipBaseStream.AvailOut = this.buffer.Length;
                    this.zipBaseStream.NextOut = this.buffer;
                    this.zipBaseStream.NextOutIndex = 0;
                }
            }
            while (this.zipBaseStream.AvailIn > 0);
        }
    }

    /// <summary>
    /// Represents a stream that can read from a compressed stream.
    /// </summary>
    public class ZipInputStream : Stream
    {
        private Stream baseStream;
        private byte[] buffer;
        private long length;
        private bool nonInput;
        private byte[] rb = new byte[1];
        private long start;
        private bool stored;
        private ZipBaseStream zipBaseStream;

        /// <summary>
        /// Initializes a new instance of the ZipInputStream class.
        /// </summary>
        /// <param name="baseStream">
        /// The stream that will be decompressed.
        /// </param>
        public ZipInputStream(Stream baseStream)
        {
            this.InitializeStream(baseStream, true, false);
        }

        internal ZipInputStream(Stream baseStream, bool zip, int sizeCompressed, int method)
        {
            bool header = !zip;
            bool flag = zip;
            this.InitializeStream(baseStream, header, flag);
            this.length = sizeCompressed;
            this.stored = method == 0;

            if ((method != 0) && (method != 8))
            {
                throw new ArgumentException("Method not supported.");
            }
        }

        /// <summary>
        /// The stream that is decompressed.
        /// </summary>
        public Stream BaseStream
        {
            get
            {
                return this.baseStream;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports reading.
        /// </summary>
        /// <returns>true if the stream supports reading; otherwise, false.</returns>
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        /// <summary> 
        /// Gets a value indicating whether the current stream supports seeking.
        /// </summary>
        /// <returns>true if the stream supports seeking; otherwise, false.</returns>
        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports writing.
        /// </summary>
        /// <returns>true if the stream supports writing; otherwise, false.</returns>
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the compressed size of the stream.
        /// </summary>
        public int CompressedSize
        {
            get
            {
                return (int)this.zipBaseStream.TotalIn;
            }
        }

        /// <summary>
        /// Gets the length in bytes of the stream.
        /// </summary>
        /// <returns>A long value representing the length of the stream in bytes.</returns>
        public override long Length
        {
            get
            {
                if (this.length >= 0)
                {
                    return this.length;
                }
                return 0;
            }
        }

        /// <summary>
        /// Gets the position within the current stream. Set is not supported.
        /// </summary>
        /// <returns>The current position within the stream.</returns>
        public override long Position
        {
            get
            {
                return this.baseStream.Position - this.start;
            }
            set
            {
                throw new NotSupportedException("Seek not supported.");
            }
        }

        /// <summary>
        /// Gets the uncompressed size of the stream.
        /// </summary>
        public int UncompressedSize
        {
            get
            {
                return (int)this.zipBaseStream.TotalOut;
            }
        }

        /// <summary>
        /// Clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        public override void Flush()
        {
            this.baseStream.Flush();
        }

        /// <summary>
        /// Reads a sequence of bytes from the
        /// current stream and advances the position within the stream by the number of bytes
        /// read.
        /// </summary>
        /// <returns>The total number of bytes read into the buffer. This can be less than
        /// the number of bytes requested if that many bytes are not currently available,
        /// or zero (0) if the end of the stream has been reached.</returns>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer
        /// contains the specified byte array with the values between <paramref name="offset"/>
        /// and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the
        /// bytes read from the current source. </param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/>
        /// at which to begin storing the data read from the current stream. </param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.
        /// </param>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.ReadBuffer(buffer, offset, count);
        }

        /// <summary>
        /// Reads a byte from the stream and advances the position within the stream
        /// by one byte, or returns -1 if at the end of the stream.</summary>
        /// <returns>The unsigned byte cast to an Int32, or -1 if at the end of the stream.
        /// </returns>
        public override int ReadByte()
        {
            if (this.ReadBuffer(this.rb, 0, 1) != 0)
            {
                return this.rb[0];
            }

            return -1;
        }

        /// <summary>
        /// Seeking is not supported.
        /// </summary>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Seek not supported.");
        }

        /// <summary>
        /// Sets the length of the current stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes. </param>
        public override void SetLength(long value)
        {
            this.length = value;
        }

        /// <summary>
        /// Writing is not supported.
        /// </summary>
        public override void Write(byte[] buf, int offset, int count)
        {
            throw new NotSupportedException("Write not supported.");
        }

        private void InitializeStream(Stream baseStream, bool header, bool crc32)
        {
            if ((baseStream == null) || !baseStream.CanRead)
            {
                throw new ArgumentException("ZipInputStream needs a readable stream.");
            }

            this.InitializeStreamAttributes(baseStream, crc32);

            if (this.zipBaseStream.InflateInit(header ? 15 : -15) != 0)
            {
                throw new Exception("Initialization failure.");
            }
        }

        private void InitializeStreamAttributes(Stream baseStream, bool crc32)
        {
            this.nonInput = false;
            this.buffer = new byte[32768];
            this.baseStream = baseStream;
            this.start = 0;
            this.length = -1;
            this.stored = false;

            try
            {
                this.start = baseStream.Position;
            }
            catch
            {
            }

            this.zipBaseStream = new ZipBaseStream(crc32);
            this.zipBaseStream.NextInIndex = 0;
            this.zipBaseStream.AvailIn = 0;
            this.zipBaseStream.NextIn = this.buffer;
        }

        private int ProcessInternal(int len, out int availableLength)
        {
            if ((this.zipBaseStream.AvailIn == 0) && !this.nonInput)
            {
                int length = this.buffer.Length;
                if (this.length >= 0)
                {
                    length = (int)System.Math.Min((long)length, this.length - this.Position);
                }
                if (length > 0)
                {
                    this.zipBaseStream.NextInIndex = 0;
                    this.zipBaseStream.AvailIn = this.baseStream.Read(this.buffer, 0, length);
                    this.nonInput = this.zipBaseStream.AvailIn < length;
                }
                else
                {
                    this.nonInput = true;
                }
            }

            if (!this.stored)
            {
                int inflatedNumber = this.zipBaseStream.Inflate(0);
                switch (inflatedNumber)
                {
                    case -5:
                        if (this.nonInput)
                        {
                            inflatedNumber = 0;
                        }
                        break;

                    case 1:
                        inflatedNumber = 0;
                        break;

                    case 0:
                        availableLength = len - this.zipBaseStream.AvailOut;
                        if ((availableLength <= 0) && !this.nonInput)
                        {
                            this.ProcessInternal(len, out availableLength);
                        }
                        return availableLength;
                }
            }

            int minAvailable = System.Math.Min(this.zipBaseStream.AvailIn, this.zipBaseStream.AvailOut);
            for (int i = 0; i < minAvailable; i++)
            {
                this.zipBaseStream.NextOut[this.zipBaseStream.NextOutIndex + i] = this.zipBaseStream.NextIn[this.zipBaseStream.NextInIndex + i];
            }

            this.zipBaseStream.NextOutIndex += minAvailable;
            this.zipBaseStream.NextInIndex += minAvailable;
            this.zipBaseStream.AvailIn -= minAvailable;
            this.zipBaseStream.AvailOut -= minAvailable;
            availableLength = len - this.zipBaseStream.AvailOut;
            if ((availableLength <= 0) && !this.nonInput)
            {
                this.ProcessInternal(len, out availableLength);
            }

            return availableLength;
        }

        private int ReadBuffer(byte[] buffer, int offset, int len)
        {
            int num = 0;
            while (len > 0)
            {
                int readBytes = this.ReadInternal(buffer, offset, len);
                if (readBytes == 0)
                {
                    return num;
                }

                num += readBytes;
                offset += readBytes;
                len -= readBytes;
            }

            return num;
        }

        private int ReadInternal(byte[] buffer, int offset, int length)
        {
            int availableLength;
            if (length == 0)
            {
                return 0;
            }

            this.zipBaseStream.NextOut = buffer;
            this.zipBaseStream.NextOutIndex = offset;
            this.zipBaseStream.AvailOut = length;

            return this.ProcessInternal(length, out availableLength);
        }
    }

    /// <summary>
    /// Represents the ZipPackageEntry class.
    /// </summary>
    public class ZipPackageEntry
    {
        private static byte[] localSignature = new byte[] { 80, 75, 3, 4 };
        private static byte[] signature = new byte[] { 80, 75, 1, 2 };
        private string comment;
        private short commentSize;
        private short diskStart;
        private int externalAttribute;
        private byte[] extraField;
        private string fileNameInZip;
        private short flags;
        private short internalAttr;
        private short modDate;
        private short modTime;
        private ZipPackage entryOwner;
        private short versionMadeBy;
        private short versionNeeded;

        internal ZipPackageEntry(ZipPackage entryOwner)
        {
            this.entryOwner = entryOwner;
        }

        /// <summary>
        /// Gets the file attributes for the entry.
        /// </summary>
        public FileAttributes Attributes
        {
            get
            {
                return (FileAttributes)this.externalAttribute;
            }
        }

        /// <summary>
        /// Gets the compressed size for the entry.
        /// </summary>
        public int CompressedSize
        {
            get
            {
                return this.CompressedSizeInternal;
            }
        }

        /// <summary>
        /// Gets the file name in the ZipPackage for the entry.
        /// </summary>
        public string FileNameInZip
        {
            get
            {
                return this.fileNameInZip;
            }
        }

        /// <summary>
        /// Gets the uncompressed size for the entry.
        /// </summary>
        public int UncompressedSize
        {
            get
            {
                return this.UncompressedSizeInternal;
            }
        }

        internal int CompressedSizeInternal { get; set; }
        internal int Crc32 { get; set; }
        internal short ExtraFieldSize { get; set; }
        internal short FileNameSize { get; set; }
        internal short Method { get; set; }
        internal uint Offset { get; set; }
        internal int UncompressedSizeInternal { get; set; }

        /// <summary>
        /// Opens an input stream that represents the entry.
        /// </summary>
        public Stream OpenInputStream()
        {
            return this.entryOwner.OpenInputStream(this);
        }

        internal bool ReadHeader(Stream fileStream)
        {
            Encoding encoding = Encoding.UTF8;
            BinaryReader reader = new BinaryReader(fileStream);
            byte[] buf = reader.ReadBytes(signature.Length);

            if ((buf.Length != signature.Length) || !ZipPackage.Match(buf, signature, 0))
            {
                return false;
            }

            this.versionMadeBy = reader.ReadInt16();
            this.ReadCommonHeaderAttributes(reader);
            this.Crc32 = reader.ReadInt32();
            this.CompressedSizeInternal = reader.ReadInt32();
            this.UncompressedSizeInternal = reader.ReadInt32();
            this.FileNameSize = reader.ReadInt16();
            this.ExtraFieldSize = reader.ReadInt16();
            this.commentSize = reader.ReadInt16();
            this.diskStart = reader.ReadInt16();
            this.internalAttr = reader.ReadInt16();
            this.externalAttribute = reader.ReadInt32();
            this.Offset = (uint)reader.ReadInt32();
            byte[] bytes = reader.ReadBytes(this.FileNameSize);

            if (bytes.Length != this.FileNameSize)
            {
                throw new Exception("Corrupted zip file!");
            }

            this.fileNameInZip = encoding.GetString(bytes, 0, bytes.Length);
            this.extraField = null;

            if (this.ExtraFieldSize > 0)
            {
                this.extraField = reader.ReadBytes(this.ExtraFieldSize);
            }

            this.comment = string.Empty;
            if (this.commentSize > 0)
            {
                bytes = reader.ReadBytes(this.commentSize);
                if (bytes.Length != this.commentSize)
                {
                    throw new Exception("Corrupted zip file!");
                }

                this.comment = encoding.GetString(bytes, 0, bytes.Length);
            }

            return true;
        }

        internal bool ReadLocalHeader(Stream fileStream)
        {
            fileStream.Position = this.entryOwner.BytesBeforeZip + this.Offset;
            BinaryReader reader = new BinaryReader(fileStream);
            byte[] buffer = reader.ReadBytes(localSignature.Length);

            if ((buffer.Length != localSignature.Length) || !ZipPackage.Match(buffer, localSignature, 0))
            {
                return false;
            }

            this.ReadCommonHeaderAttributes(reader);
            int crc32 = reader.ReadInt32();
            int compressedSize = reader.ReadInt32();
            int uncompressedSize = reader.ReadInt32();
            this.FileNameSize = reader.ReadInt16();
            this.ExtraFieldSize = reader.ReadInt16();

            if ((!((this.flags & 8) != 0) && (crc32 != 0)) && ((compressedSize > 0) && (uncompressedSize > 0)))
            {
                this.Crc32 = crc32;
                this.CompressedSizeInternal = compressedSize;
                this.UncompressedSizeInternal = uncompressedSize;
            }

            return true;
        }

        internal string SetFlags(string fileNameInZip)
        {
            fileNameInZip = fileNameInZip.Replace(Path.DirectorySeparatorChar, '/');
            fileNameInZip = fileNameInZip.Replace(Path.AltDirectorySeparatorChar, '/');
            this.fileNameInZip = fileNameInZip;
            this.FileNameSize = (short)this.fileNameInZip.Length;
            this.comment = string.Empty;
            this.commentSize = 0;
            this.Method = 8;
            this.versionMadeBy = 20;
            this.versionNeeded = 20;
            this.flags = 0;

            return fileNameInZip;
        }

        internal void SetName(string fileName, DateTime dateTime)
        {
            if ((fileName != null) && (File.Exists(fileName) || Directory.Exists(fileName)))
            {
                this.SetTime(File.GetLastWriteTime(fileName));
                FileInfo info = new FileInfo(fileName);
                this.externalAttribute = (int)info.Attributes;
            }
            else
            {
                this.SetTime(dateTime);
                this.externalAttribute = 32;
            }
        }

        internal int WriteHeader(Stream fileStream)
        {
            int initialSize = 46;
            Encoding encoding = Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(this.fileNameInZip);
            byte[] buffer = encoding.GetBytes(this.comment);
            this.FileNameSize = (short)bytes.Length;
            this.commentSize = (short)buffer.Length;
            BinaryWriter writer = new BinaryWriter(fileStream);
            writer.Write(signature);
            writer.Write(this.versionMadeBy);
            this.WriteCommonHeaderAttributes(writer);
            writer.Write(this.commentSize);
            writer.Write(this.diskStart);
            writer.Write(this.internalAttr);
            writer.Write(this.externalAttribute);
            writer.Write(this.Offset);
            writer.Write(bytes);

            if (this.ExtraFieldSize > 0)
            {
                writer.Write(this.extraField);
            }

            writer.Write(buffer);

            int wroteSize = ((initialSize + this.FileNameSize) + this.ExtraFieldSize) + this.commentSize;
            return wroteSize;
        }

        internal int WriteLocalHeader(Stream fileStream)
        {
            int initialSize = 30;
            byte[] bytes = Encoding.UTF8.GetBytes(this.fileNameInZip);
            this.FileNameSize = (short)bytes.Length;
            BinaryWriter writer = new BinaryWriter(fileStream);
            writer.Write(localSignature);
            this.WriteCommonHeaderAttributes(writer);
            writer.Write(bytes);

            if (this.ExtraFieldSize > 0)
            {
                writer.Write(this.extraField);
            }

            int wroteSize = (initialSize + this.FileNameSize) + this.ExtraFieldSize;
            return wroteSize;
        }

        private void ReadCommonHeaderAttributes(BinaryReader reader)
        {
            this.versionNeeded = reader.ReadInt16();
            this.flags = reader.ReadInt16();
            this.Method = reader.ReadInt16();
            this.modTime = reader.ReadInt16();
            this.modDate = reader.ReadInt16();
        }

        private void SetTime(DateTime time)
        {
            int year = System.Math.Max(time.Year - 1980, 0);
            this.modDate = (short)((time.Day + (time.Month << 5)) + (year << 9));
            this.modTime = (short)(((time.Second >> 1) + (time.Minute << 5)) + (time.Hour << 11));
        }

        private void WriteCommonHeaderAttributes(BinaryWriter writer)
        {
            writer.Write(this.versionNeeded);
            writer.Write(this.flags);
            writer.Write(this.Method);
            writer.Write(this.modTime);
            writer.Write(this.modDate);
            writer.Write(this.Crc32);
            writer.Write(this.CompressedSizeInternal);
            writer.Write(this.UncompressedSizeInternal);
            writer.Write(this.FileNameSize);
            writer.Write(this.ExtraFieldSize);
        }
    }

    /// <summary>
    /// Represents the ZipPackage class.
    /// </summary>
    public class ZipPackage : IDisposable
    {
        private static byte[] signature = new byte[] { 80, 75, 5, 6 };
        private ushort allEntriesCount;
        private byte[] buffer;
        private ushort centralDirDisk;
        private string comment;
        private short commentSize;
        private uint dirSize;
        private ushort diskNumber;
        private ushort diskEntriesCount;
        private string fileName;
        private Stream internalStream;
        private FileAccess access;

        private ZipPackage()
        {
            this.buffer = new byte[32768];
            this.fileName = string.Empty;
            this.comment = string.Empty;
            this.Headers = new List<ZipPackageEntry>();
            this.TempFileName = null;
            this.internalStream = null;
        }

        /// <summary>
        /// Gets the file name for the ZipPackage.
        /// </summary>
        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        /// <summary>
        /// Gets the list with all zip entries.
        /// </summary>
        public IList<ZipPackageEntry> ZipPackageEntries
        {
            get
            {
                return this.Headers;
            }
        }

        internal uint BytesBeforeZip { get; set; }
        internal IList<ZipPackageEntry> Headers { get; set; }
        internal uint Offset { get; set; }
        internal string TempFileName { get; set; }

        /// <summary>
        /// This method is used to create a ZipPackage from a stream.
        /// </summary>
        public static ZipPackage Create(Stream stream)
        {
            ZipPackage zip = new ZipPackage();
            zip.CreateStream(stream);
            zip.access = FileAccess.Write;
            return zip;
        }

        /// <summary>
        /// This method is used to create a ZipPackage with the passed file name.
        /// </summary>
        public static ZipPackage CreateFile(string fileName)
        {
            ZipPackage zip = new ZipPackage();
            zip.CreateFileHelper(fileName);
            zip.access = FileAccess.Write;
            return zip;
        }

        /// <summary>
        /// This method is used to open a ZipPackage with the passed file name.
        /// </summary>
        public static ZipPackage OpenFile(string fileName, FileAccess access)
        {
            if (access != FileAccess.Read)
            {
                throw new InvalidOperationException("File should be opened with read access.");
            }

            ZipPackage zip = new ZipPackage();
            zip.OpenFileHelper(fileName);
            zip.access = access;
            return zip;
        }

        /// <summary>
        /// This method is used to open a ZipPakcage from a stream.
        /// </summary>
        public static ZipPackage Open(Stream stream, FileAccess access)
        {
            if (!stream.CanSeek && access != FileAccess.Read)
            {
                throw new InvalidOperationException("Stream cannot seek.");
            }

            ZipPackage zip = new ZipPackage();
            zip.OpenStream(stream);
            zip.access = access;

            return zip;
        }

        /// <summary>
        /// Checks whether the stream that represents a zip file is actually a zip file.
        /// </summary>
        public static bool IsZipFile(Stream stream)
        {
            bool flag;
            if ((stream == null) || (stream.Length < 22))
            {
                return false;
            }

            long position = stream.Position;
            try
            {
                new ZipPackage().OpenStream(stream);
                flag = true;
            }
            catch
            {
                flag = false;
            }
            finally
            {
                stream.Position = position;
            }

            return flag;
        }

        /// <summary>
        /// Checks whether the file with the passed file name is actually a zip file.
        /// </summary>
        public static bool IsZipFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return false;
            }

            try
            {
                new ZipPackage().OpenFileHelper(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a file with the passed file name in the ZipPackage.
        /// </summary>
        public void Add(string fileName)
        {
            string fileNameInZip = this.GetFileName(fileName);
            this.Add(fileName, fileNameInZip);
        }

        /// <summary>
        /// Adds the files from the passed array of file names in the ZipPackage.
        /// </summary>
        public void Add(string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                string fileNameInZip = this.GetFileName(fileName);
                this.Add(fileName, fileNameInZip);
            }
        }

        /// <summary>
        /// Adds a stream in the ZipPackage and associates it with the passed file name in zip.
        /// </summary>
        public void AddStream(string fileNameInZip, Stream stream)
        {
            this.AddStream(ZipCompression.Default, fileNameInZip, stream, DateTime.Now);
        }

        /// <summary>
        /// Adds a file with the passed file name in the ZipPackage and associates it with the passed file name in zip.
        /// </summary>
        public void Add(string fileName, string fileNameInZip)
        {
            this.Add(fileName, fileNameInZip, DateTime.Now);
        }

        /// <summary>
        /// Adds a file with the passed file name in the ZipPackage, associates it with the passed file name in zip and sets a date time for the entry.
        /// </summary>
        public void Add(string fileName, string fileNameInZip, DateTime dateTime)
        {
            if (string.Compare(fileName, this.FileName, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0)
            {
                throw new ArgumentException("Can't add a zip file to itself.");
            }
            bool flag = File.Exists(fileName);
            bool flag2 = !flag && Directory.Exists(fileName);
            if (!flag && !flag2)
            {
                throw new FileNotFoundException(string.Format("File not found: '{0}'.", fileName));
            }

            this.AddEntry(fileName, fileNameInZip, dateTime);
        }

        /// <summary>
        /// Adds a stream in the ZipPackage, compresses it with the passed compress method, associates it with the passed file name in zip and sets a date time for the entry. 
        /// </summary>
        public void AddStream(ZipCompression method, string fileNameInZip, Stream stream, DateTime dateTime)
        {
            this.AddEntry(method, stream, fileNameInZip, dateTime);
        }

        /// <summary>
        /// Closes the ZipPackage. If the value is set to false it just resets the internal values, otherwise closes the file.
        /// </summary>
        public void Close(bool shouldCloseStream)
        {
            if (this.access != FileAccess.Read)
            {
                this.diskNumber = 0;
                this.centralDirDisk = 0;
                this.diskEntriesCount = 0;
                this.allEntriesCount = 0;
                this.dirSize = 0;
                this.Offset = 0;
                this.BytesBeforeZip = 0;
                this.commentSize = 0;
                this.fileName = string.Empty;
                this.comment = string.Empty;
                this.Headers.Clear();
            }

            if (this.internalStream != null)
            {
                this.internalStream.Flush();
                if (shouldCloseStream)
                {
                    this.CloseBundle();
                }
            }
        }

        /// <summary>
        /// Gets the index of the entry in the list of entries of the ZipPackage.
        /// </summary>
        public int IndexOf(string fileNameInZip)
        {
            var entries = this.Headers as List<ZipPackageEntry>;
            for (int i = 0; i < entries.Count; i++)
            {
                string fileName = entries[i].FileNameInZip;
                if (string.Compare(fileNameInZip, fileName, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Removes the passed entry from the ZipPackage.
        /// </summary>
        public void RemoveEntry(ZipPackageEntry zipPackageEntry)
        {
            this.RemoveEntryHelper(zipPackageEntry);
        }

        void IDisposable.Dispose()
        {
            this.CloseBundle();
        }

        internal static bool Match(byte[] buffer, byte[] signatureBytes, int offset)
        {
            for (int i = 0; i < signatureBytes.Length; i++)
            {
                if (buffer[offset + i] != signatureBytes[i])
                {
                    return false;
                }
            }

            return true;
        }

        internal void Close(Stream stream)
        {
            if (this.internalStream == null)
            {
                stream.Close();
            }
        }

        internal Stream OpenFile(string fileName, bool readOnly, bool readEntries)
        {
            if (this.internalStream != null)
            {
                return this.internalStream;
            }

            Stream fileStream = null;
            if (!readOnly)
            {
                try
                {
                    fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                }
                catch
                {
                }
            }

            if (fileStream == null)
            {
                fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            }

            try
            {
                this.OpenStream(fileStream, readEntries);
            }
            catch
            {
                fileStream.Close();
                throw;
            }

            this.fileName = fileName;
            return fileStream;
        }

        internal Stream OpenInputStream(ZipPackageEntry zipPackageEntry)
        {
            ZipInputStream inputStream = null;
            Stream fileStream = null;
            try
            {
                fileStream = this.OpenFile(this.FileName, true, false);
                zipPackageEntry.ReadLocalHeader(fileStream);
                fileStream.Position = (((this.BytesBeforeZip + zipPackageEntry.Offset) + 30) + zipPackageEntry.FileNameSize) + zipPackageEntry.ExtraFieldSize;
                inputStream = new ZipInputStream(fileStream, true, zipPackageEntry.CompressedSize, zipPackageEntry.Method);
            }
            catch
            {
                this.Close(fileStream);
                if (inputStream != null)
                {
                    inputStream.Close();
                }
                throw new Exception("Cannot extract entry!");
            }

            return inputStream;
        }

        internal void RemoveIndex(int index)
        {
            if (((this.FileName == null) || (this.FileName.Length == 0)) && (this.internalStream == null))
            {
                throw new ArgumentException("Zip file is not open.");
            }

            if ((index < 0) || (index >= this.Headers.Count))
            {
                throw new IndexOutOfRangeException("Invalid index or entry name.");
            }

            this.SafelyRemove(index);
        }

        internal void RemoveEntryHelper(ZipPackageEntry zipPackageEntry)
        {
            var entries = this.Headers as List<ZipPackageEntry>;
            this.RemoveIndex(entries.IndexOf(zipPackageEntry));
        }

        internal void WriteCentralDir(Stream fileStream)
        {
            this.allEntriesCount = (ushort)this.Headers.Count;
            this.diskEntriesCount = this.allEntriesCount;
            this.Offset = ((uint)fileStream.Position) - this.BytesBeforeZip;
            this.dirSize = 0;

            if (this.allEntriesCount != 0)
            {
                for (int i = 0; i < this.Headers.Count; i++)
                {
                    this.dirSize += (uint)this.FindEntryByIndex(i).WriteHeader(fileStream);
                }
            }

            this.WriteCentralDirAttributes(fileStream);
        }

        private static long Locate(Stream fileStream)
        {
            long currentOffset = 65557;
            long length = fileStream.Length;
            if (currentOffset > length)
            {
                currentOffset = length;
            }

            long offset = 0;
            long currentCount = 0;
            byte[] buffer = new byte[32768];
            while (offset < currentOffset)
            {
                offset = currentCount + 32768;
                if (offset > currentOffset)
                {
                    offset = currentOffset;
                }
                int count = (int)(offset - currentCount);
                fileStream.Seek(-offset, SeekOrigin.End);
                if (fileStream.Read(buffer, 0, count) != count)
                {
                    throw new IOException("Error reading data from Zip file (file may be corrupted).");
                }
                for (int i = count - 4; i >= 0; i--)
                {
                    if (Match(buffer, signature, i))
                    {
                        return length - offset + i;
                    }
                }
                currentCount += count - 3;
            }

            throw new Exception("Dir not found!");
        }

        private string GetFileName(string fileName)
        {
            string str = Path.GetFileName(fileName);
            return str;
        }

        private void CloseBundle()
        {
            if (this.internalStream != null)
            {
                this.internalStream.Close();
                this.internalStream = null;
            }
        }

        private void CreateFileHelper(string fileName)
        {
            this.Close(false);
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                this.WriteCentralDir(stream);
            }
            this.fileName = fileName;
        }

        private void CreateStream(Stream stream)
        {
            this.Close(true);
            this.WriteCentralDir(stream);
            this.internalStream = stream;
        }

        private void OpenFileHelper(string fileName)
        {
            if (!File.Exists(fileName))
            {
                this.CreateFileHelper(fileName);
            }
            else
            {
                Stream stream = null;
                try
                {
                    stream = this.OpenFile(fileName, true, true);
                }
                finally
                {
                    this.Close(stream);
                }
            }
        }

        private void OpenStream(Stream stream)
        {
            this.Close(true);
            this.OpenStream(stream, true);
            this.internalStream = stream;
        }

        private void AddEntry(string fileName, string fileNameInZip, DateTime dateTime)
        {
            if (File.Exists(fileName))
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    this.AddEntry(ZipCompression.Default, stream, fileNameInZip, dateTime);
                    return;
                }
            }

            if (!Directory.Exists(fileName))
            {
                throw new FileNotFoundException(string.Format("File not found: '{0}'.", fileName));
            }

            this.OpenOutputStream(ZipCompression.Default, fileName, fileNameInZip, dateTime, true).Close();
        }

        private void AddEntry(ZipCompression method, Stream recordStream, string fileNameInZip, DateTime dateTime)
        {
            if (this.Headers.Count >= 65535)
            {
                throw new Exception("Entries cannot be more than 64 K.");
            }

            if (recordStream.Length > 4294967295)
            {
                throw new Exception("Entry cannot exceed 4G!");
            }

            if (((this.FileName == null) || (this.FileName.Length == 0)) && (this.internalStream == null))
            {
                throw new ArgumentException("Zip file is not open.");
            }

            string fileName = null;
            FileStream stream = recordStream as FileStream;
            if (stream != null)
            {
                try
                {
                    fileName = stream.Name;
                }
                catch (SecurityException)
                {
                }
                catch (MethodAccessException)
                {
                }
            }

            Stream outputStream = this.OpenOutputStream(method, fileName, fileNameInZip, dateTime, true);
            long length = recordStream.Length - recordStream.Position;
            this.CopyRecordStream((uint)length, outputStream, recordStream);

            outputStream.Close();
        }

        private void CopyRecordStream(uint length, Stream outputStream, Stream recordStream)
        {
            int count = 0;
            int readBytes = 0;
            for (uint i = length; i > 0; i -= (uint)readBytes)
            {
                count = (int)System.Math.Min((long)this.buffer.Length, (long)i);
                readBytes = recordStream.Read(this.buffer, 0, count);
                if (readBytes == 0)
                    break;
                outputStream.Write(this.buffer, 0, readBytes);
            }

            outputStream.Flush();
        }

        private ZipPackageEntry FindEntryByIndex(int entryIndex)
        {
            return this.Headers[entryIndex];
        }

        private Stream OpenOutputStream(ZipCompression method, string fileName, string fileNameInZip, DateTime dateTime, bool memory)
        {
            return ZipOutputStream.CreateZipEntryOutputStream(method, this, fileName, fileNameInZip, dateTime, memory);
        }

        private Stream OpenStream(Stream fileStream, bool readEntries)
        {
            long offset = this.ReadFileStreamAttributes(fileStream);
            if (((this.diskNumber != 0) || (this.centralDirDisk != 0)) || (this.diskEntriesCount != this.allEntriesCount))
            {
                throw new NotSupportedException("Disk spanning not supported.");
            }

            if (this.dirSize < (this.allEntriesCount * 46))
            {
                throw new Exception("Corrupted zip file!");
            }

            this.BytesBeforeZip = (((uint)offset) - this.dirSize) - this.Offset;
            if (this.BytesBeforeZip != 0)
            {
                throw new Exception("Unrecognized zip file format!");
            }

            if (readEntries)
            {
                this.Headers.Clear();
                fileStream.Seek((long)(this.Offset + this.BytesBeforeZip), SeekOrigin.Begin);
                for (int i = 0; i < this.allEntriesCount; i++)
                {
                    ZipPackageEntry item = new ZipPackageEntry(this);
                    if (!item.ReadHeader(fileStream))
                    {
                        throw new Exception("Corrupted zip file!");
                    }
                    this.Headers.Add(item);
                }
            }

            return fileStream;
        }

        private long ReadFileStreamAttributes(Stream fileStream)
        {
            long offset = Locate(fileStream);
            fileStream.Seek(offset, SeekOrigin.Begin);
            BinaryReader reader = new BinaryReader(fileStream);
            byte[] buf = reader.ReadBytes(signature.Length);
            if ((buf.Length != signature.Length) || !Match(buf, signature, 0))
            {
                throw new Exception("Not a zip file!");
            }

            this.diskNumber = reader.ReadUInt16();
            this.centralDirDisk = reader.ReadUInt16();
            this.diskEntriesCount = reader.ReadUInt16();
            this.allEntriesCount = reader.ReadUInt16();
            this.dirSize = reader.ReadUInt32();
            this.Offset = reader.ReadUInt32();
            this.commentSize = reader.ReadInt16();
            Encoding encoding = Encoding.UTF8;
            this.comment = string.Empty;
            if (this.commentSize > 0)
            {
                byte[] bytes = reader.ReadBytes(this.commentSize);
                if (bytes.Length != this.commentSize)
                {
                    throw new Exception("Corrupted zip file!");
                }
                this.comment = encoding.GetString(bytes, 0, bytes.Length);
            }

            return offset;
        }

        private int RemovePackedFile(Stream fileStream, int index)
        {
            List<ZipPackageEntry> entries = this.Headers as List<ZipPackageEntry>;
            if (index == (entries.Count - 1))
            {
                long num = entries[index].Offset + this.BytesBeforeZip;
                fileStream.SetLength(num);
                return 0;
            }

            long currentOffset = entries[index].Offset + this.BytesBeforeZip;
            long nextOffset = entries[index + 1].Offset + this.BytesBeforeZip;
            long length = fileStream.Length - nextOffset;
            long currentLength = length;
            long seekLength = 0;
            int count = 0;
            byte[] buffer = this.buffer;

            if (length > buffer.Length)
            {
                length = buffer.Length;
            }

            do
            {
                fileStream.Seek(nextOffset + seekLength, SeekOrigin.Begin);
                count = fileStream.Read(buffer, 0, (int)length);
                if (count > 0)
                {
                    fileStream.Seek(currentOffset + seekLength, SeekOrigin.Begin);
                    fileStream.Write(buffer, 0, count);
                }
                seekLength += count;
            }
            while (count == length);

            if (currentLength != seekLength)
            {
                throw new IOException("Error writing data to Zip file (file may be corrupted).");
            }

            uint offsetStep = (uint)(nextOffset - currentOffset);
            fileStream.SetLength(fileStream.Length - offsetStep);
            return (int)offsetStep;
        }

        private void SafelyRemove(int index)
        {
            Stream fileStream = null;
            try
            {
                fileStream = this.OpenFile(this.FileName, false, true);
                fileStream.SetLength((long)(this.BytesBeforeZip + this.Offset));
                int num = this.RemovePackedFile(fileStream, index);
                this.Headers.RemoveAt(index);
                List<ZipPackageEntry> entries = this.Headers as List<ZipPackageEntry>;
                for (int i = index; i < entries.Count; i++)
                {
                    ZipPackageEntry currentEntry = entries[i];
                    currentEntry.Offset -= (uint)num;
                }

                this.WriteCentralDir(fileStream);
            }
            finally
            {
                this.Close(fileStream);
            }
        }

        private void WriteCentralDirAttributes(Stream fileStream)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(this.comment);
            BinaryWriter writer = new BinaryWriter(fileStream);
            writer.Write(signature);
            writer.Write(this.diskNumber);
            writer.Write(this.centralDirDisk);
            writer.Write(this.diskEntriesCount);
            writer.Write(this.allEntriesCount);
            writer.Write(this.dirSize);
            writer.Write(this.Offset);
            writer.Write((short)bytes.Length);
            writer.Write(bytes);
        }
    }
    #endregion
}
