using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MemoryMaster
{
    internal class Reader
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hProcess);


        public static byte[] ReadMemory(Process process, int address, int numOfBytes, out int bytesRead)
        {
            IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, process.Id);
            byte[] buffer = new byte[numOfBytes];
            ReadProcessMemory(hProc, new IntPtr(address), buffer, numOfBytes, out bytesRead);
            return buffer;
        }
        public static int getValue(Process proc, int address)
        {
            int bytesRead;
            byte[] value = ReadMemory(proc, address, 4, out bytesRead);
            int am = BitConverter.ToInt32(value, 0);
            return am;
        }
        public static float getValueFloat(Process proc, int address)
        {
            int bytesRead;
            byte[] value = ReadMemory(proc, address, 4, out bytesRead);
            float am = BitConverter.ToSingle(value, 0);
            return am;
        }
        public static int getValueByte(Process proc, int address)
        {
            int bytesRead;
            byte[] value = ReadMemory(proc, address, 4, out bytesRead);
            //byte[] value2 = new byte[4];
            //value2[0] = value[0];
            //int am = BitConverter.ToInt32(value2, 0);
            //return am;
            return value[0];
        }

        public static string getValString(Process proc, int address, int len)
        {
            int bytesRead;
            byte[] value = ReadMemory(proc, address, len, out bytesRead);

            // populate foo
            int i = value.Length - 1;
            while (value[i] == 0 && i != 0)
                --i;

            if (i == 0)
                return "";

            byte[] bar = new byte[i + 1];
            Array.Copy(value, bar, i + 1);

            string line = System.Text.Encoding.ASCII.GetString(bar);
            return line;

        }

        public static int getValW(Process proc, int address)
        {
            int bytesRead;
            byte[] value = ReadMemory(proc, address, 4, out bytesRead);
            byte[] value2 = new byte[4];
            value2[0] = value[0];
            value2[1] = value[1];
            int am = BitConverter.ToInt32(value2, 0);
            return am;
        }
    }
}
