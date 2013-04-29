using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MemoryMaster.Memory
{
    internal static class Reader
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hProcess);


        public static byte[] Read(Process process, int address, int numOfBytes, out int bytesRead)
        {
            IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, process.Id);
            byte[] buffer = new byte[numOfBytes];
            ReadProcessMemory(hProc, new IntPtr(address), buffer, numOfBytes, out bytesRead);
            return buffer;
        }
    }
}
