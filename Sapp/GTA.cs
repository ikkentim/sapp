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
            if (gta !=null && gta.process.HasExited)
                gta = null;

            if (gta == null)
            {
                Process[] processes = Process.GetProcessesByName("gta_sa");
                if (processes.Length == 1)
                    gta = new MemoryMaster.Memory.ProcessReader(processes[0]);
            }

            if (gta != null && samp == null)
                samp = gta.GetModulePointer("samp.dll");

        }
    }
}
