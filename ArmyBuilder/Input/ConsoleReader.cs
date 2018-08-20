using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Input
{
    public class ConsoleReader : IReader
    {
        public char ReadChar()
        {
            var input = Console.ReadKey();
            return input.KeyChar;
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
