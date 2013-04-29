using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MemoryMaster.Memory
{
    public class ProcessReader : Pointer
    {
        public ProcessReader(Process process) : base(process, 0)
        {
        }

        public Pointer GetModulePointer(string moduleName)
        {
            foreach (ProcessModule module in process.Modules)
                if (string.Compare(module.ModuleName, moduleName, true) == 0)
                    return new Pointer(process, (int)module.BaseAddress);
            return null;
        }
    }
}
