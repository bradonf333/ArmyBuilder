using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyBuilder.Writers
{
    public class ConsoleWriter : IWriter
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ClearMessage()
        {
            Console.Clear();
        }
    }
}
