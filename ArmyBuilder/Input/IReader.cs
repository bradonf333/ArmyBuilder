using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Input
{
    public interface IReader
    {
        /// <summary>
        /// Reads a single Character.
        /// </summary>
        void ReadChar();

        /// <summary>
        /// Reads an entire line.
        /// </summary>
        void ReadLine();
    }
}
