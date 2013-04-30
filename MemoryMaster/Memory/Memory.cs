using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MemoryMaster.Memory
{
    /// <summary>
    /// An objective view on a memory address
    /// </summary>
    public class Memory
    {
        /// <summary>
        /// The process who's pool this Pointer is in
        /// </summary>
        public Process process;

        /// <summary>
        /// The address of this memory
        /// </summary>
        public int Address = 0;

        /// <summary>
        /// Internally used constructor
        /// </summary>
        /// <param name="process">The process to watch</param>
        /// <param name="address">The address of the pointer</param>
        public Memory(Process process, int address)
        {
            this.process = process;
            this.Address = address;
        }

        /// <summary>
        /// Get Value as integer (4 bytes)
        /// </summary>
        /// <returns>Integer</returns>
        public int GetValue()
        {
            int bytesRead;
            byte[] value = Reader.Read(process, Address, 4, out bytesRead);
            int am = BitConverter.ToInt32(value, 0);
            return am;
        }

        /// <summary>
        /// Get value as float (4 bytes)
        /// </summary>
        /// <returns>Float</returns>
        public float GetFloat()
        {
            int bytesRead;
            byte[] value = Reader.Read(process, Address, 4, out bytesRead);
            float am = BitConverter.ToSingle(value, 0);
            return am;
        }

        /// <summary>
        /// Get value as single byte
        /// </summary>
        /// <returns>Byte</returns>
        public byte GetByte()
        {
            int bytesRead;
            byte[] value = Reader.Read(process, Address, 4, out bytesRead);
            //byte[] value2 = new byte[4];
            //value2[0] = value[0];
            //int am = BitConverter.ToInt32(value2, 0);
            //return am;
            return value[0];
        }

        /// <summary>
        /// Get value as string
        /// </summary>
        /// <param name="length">Length of the string</param>
        /// <returns>String</returns>
        public string GetString(int length)
        {
            int bytesRead;
            byte[] value = Reader.Read(process, Address, length, out bytesRead);

            // get final length of string
            int i = value.Length - 1;
            while (value[i] == 0 && i != 0)
                --i;

            if (i == 0)
                return "";

            //Populate array with ending empty byte
            byte[] bar = new byte[i + 1];
            Array.Copy(value, bar, i + 1);

            return System.Text.Encoding.ASCII.GetString(bar);
        }

        /// <summary>
        /// Get value as short
        /// </summary>
        /// <returns>Short</returns>
        public short GetShort()
        {
            int bytesRead;
            byte[] value = Reader.Read(process, Address, 4, out bytesRead);
            byte[] value2 = new byte[4];
            value2[0] = value[0];
            value2[1] = value[1];

            return (short)BitConverter.ToInt32(value2, 0);
        }
    }
}
