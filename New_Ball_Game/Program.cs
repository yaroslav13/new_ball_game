using System;

namespace New_Ball_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(AppStrings.PressEnter) ;
            Console.WriteLine(AppStrings.PressQ);
            do {
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Escape) break;
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        Run();
                        break;
                    case ConsoleKey.Q:
                        Clear();
                        break;
                }
            } while (key.Key != ConsoleKey.Q);
            Console.ReadLine();

            static void Run()
            {
                _ = new Game();
            }

            static void Clear()
            {
                Console.Clear();
            }
        }
    }
}
