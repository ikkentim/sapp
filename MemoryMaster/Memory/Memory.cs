using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MemoryMaster.Memory
{
    public class Memory
    {
        public Process process;
        public int Address = 0;

        public Memory(Process process, int address)
        {
            this.process = process;
            this.Address = address;
        }

        public int GetValue()
        {
            int bytesRead;
            byte[] value = Reader.Read(process, Address, 4, out bytesRead);
            int am = BitConverter.ToInt32(value, 0);
            return am;
        }
        public float GetFloat()
        {
            int bytesRead;
            byte[] value = Reader.Read(process, Address, 4, out bytesRead);
            float am = BitConverter.ToSingle(value, 0);
            return am;
        }
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
