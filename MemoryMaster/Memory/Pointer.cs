using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MemoryMaster.Memory
{
    public class Pointer
    {
        public Process process;

        public int Address = 0;

        public Pointer(Process process, int address)
        {
            this.process = process;
            this.Address = new Memory(process, address).GetValue();
        }

        public Pointer GetPointer(int offset)
        {
            return new Pointer(process, Address + offset);
        }

        public Memory GetMemory(int offset)
        {
            return new Memory(process, Address + offset);
        }
    }
}
