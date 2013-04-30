using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MemoryMaster.Memory
{
    /// <summary>
    /// Memory Reader for a specific Processes
    /// </summary>
    public class ProcessReader : Pointer
    {
        /// <summary>
        /// Constructor for Reading Memory for a specific Process
        /// </summary>
        /// <param name="process">Process to read from</param>
        public ProcessReader(Process process) : base(process, 0)
        {
        }

        /// <summary>
        /// Get the memory Pointer of a specific module of this process
        /// </summary>
        /// <param name="moduleName">The name of this module</param>
        /// <returns>Pointer</returns>
        public Pointer GetModulePointer(string moduleName)
        {
            foreach (ProcessModule module in process.Modules)
                if (string.Compare(module.ModuleName, moduleName, true) == 0)
                    return new Pointer(process, (int)module.BaseAddress);
            return null;
        }
    }
}
