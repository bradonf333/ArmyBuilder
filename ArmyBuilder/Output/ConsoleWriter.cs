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

        public void Alert()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;
        }

        public void Information()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
        }

        public void Default()
        {
            Console.ResetColor();
        }

        public void CustomBG(Color color)
        {
            switch (color)
            {
                case Color.Blue:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case Color.Cyan:
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;
                case Color.DarkCyan:
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;
                case Color.Black:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case Color.Green:
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case Color.Red:
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case Color.Yellow:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                case Color.White:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }

        public void CustomFG(Color color)
        {
            switch (color)
            {
                case Color.Grey:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case Color.DarkGrey:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case Color.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Color.Cyan:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case Color.DarkCyan:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case Color.Black:
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Color.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Color.White:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
            }
        }
    }
}
