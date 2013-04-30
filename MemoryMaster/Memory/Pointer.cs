using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MemoryMaster.Memory
{
    /// <summary>
    /// An objective view on a Memory Pointer
    /// </summary>
    public class Pointer
    {
        /// <summary>
        /// The process who's pool this Pointer is in
        /// </summary>
        public Process process;

        /// <summary>
        /// The address of this pointer
        /// </summary>
        public int Address = 0;

        /// <summary>
        /// Wether this pointer is pointing at an Address
        /// </summary>
        public bool Pointing;

        /// <summary>
        /// Internally used constructor
        /// </summary>
        /// <param name="process">The process to watch</param>
        /// <param name="address">The address of the pointer</param>
        public Pointer(Process process, int address)
        {
            this.process = process;
            this.Address = new Memory(process, address).GetValue();

            Pointing = this.Address != 0;
        }

        /// <summary>
        /// Get Pointer at this Pointer's finger + offset
        /// </summary>
        /// <param name="offset">offset of pointer</param>
        /// <returns>The Pointer object</returns>
        public Pointer GetPointer(int offset)
        {
            return new Pointer(process, Address + offset);
        }

        /// <summary>
        /// Get Memory at this Pointer's finger + offset
        /// </summary>
        /// <param name="offset">offset of pointer</param>
        /// <returns>The Memory object</returns>
        public Memory GetMemory(int offset)
        {
            return new Memory(process, Address + offset);
        }
    }
}
