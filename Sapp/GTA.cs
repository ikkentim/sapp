using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Sapp
{
    static class GTA
    {
        public static MemoryMaster.Memory.ProcessReader gta = null;
        public static MemoryMaster.Memory.Pointer samp = null;

        public static void Find()
        {
            if (gta != null && gta.process.HasExited)
            {
                Debug.WriteLine("GTA has exited, resetting process...");
                gta = null;
                samp = null;
            }
            if (gta == null || samp == null)
            {
                Process[] processes = Process.GetProcessesByName("gta_sa");
                if (processes.Length == 1)
                {
                    Debug.WriteLine("Found GTA process...");
                    gta = new MemoryMaster.Memory.ProcessReader(processes[0]);

                    if (samp == null)
                    {
                        Debug.WriteLine("Waiting for SAMP...");
                        samp = gta.GetModulePointer("samp.dll");
                        if (samp != null)
                            Debug.WriteLine("Found SAMP module...");
                    }
                }
            }



        }
    }
}
