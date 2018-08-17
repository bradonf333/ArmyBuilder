using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Input
{
    public class ConsoleReader : IReader
    {
        public void ReadChar()
        {
            Console.ReadKey();
        }

        public void ReadLine()
        {
            Console.ReadLine();
        }
    }
}
