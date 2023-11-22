using GraphLib.Enums;

namespace GraphLib.Utils
{
    public class ConsoleHelper
    {
        public static void WriteTaskMessage(string message, TaskEnum task)
        {
            var color = Console.ForegroundColor;
            var originalColor = Console.ForegroundColor;

            switch(task)
            {
                case TaskEnum.Task1: color = ConsoleColor.DarkCyan; break;
                case TaskEnum.Task2: color = ConsoleColor.DarkBlue; break;
                case TaskEnum.Task3: color = ConsoleColor.DarkMagenta; break;
                case TaskEnum.Task4: color = ConsoleColor.DarkYellow; break;
            }

            Console.Write(DateTime.Now.ToString("HH:mm:ss") + "\n");
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = originalColor;
            Console.WriteLine("\n");
        }
        public static void WriteInfo(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.Write(DateTime.Now.ToString("HH:mm:ss") + "\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("INFO: " + message + "\n");
            Console.ForegroundColor = originalColor;
        }
        public static void WriteSeparator()
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("==================================================" + "\n");
            Console.ForegroundColor = originalColor;
        }
        public static void WriteError(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.Write(DateTime.Now.ToString("HH:mm:ss") + "\n");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ERROR: " + message + "\n");
            Console.ForegroundColor = originalColor;
        }
    }
}
