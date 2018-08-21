using ArmyBuilder.Writers;
using System;

namespace ArmyBuilder.Output
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
