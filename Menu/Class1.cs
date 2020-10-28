using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBuilder
{
    public class Option
    {
        public string Title { set; get; }
        public Action Callback { set; get; }

        public Option(string title, Action callback)
        {
            Title = title;
            Callback = callback;
        }
    }

    public class Menu
    {
        public List<Option> Options { set; get; }
        public bool Loop;
        public bool QuitOption;

        public Menu()
        {
            Options = new List<Option>();
            Loop = false;
            QuitOption = false;
        }

        private class NoOptionsException : Exception { }

        public void Serve()
        {
            if (Options.Count == 0)
            {
                throw new NoOptionsException();
            }

            bool quit = false;
            int totalOptions = Options.Count + (QuitOption ? 1 : 0);
            int selectedOption = 0;
            do
            {
                Console.Clear();
                for (int i = 0; i < Options.Count; i++)
                {
                    PrintOption(Options[i].Title, i == selectedOption);
                }

                if (QuitOption)
                {
                    PrintOption(Loop ? "Salir" : "Cancelar", selectedOption == Options.Count);
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption + 1) % totalOptions;
                        break;

                    case ConsoleKey.UpArrow:
                        selectedOption--;
                        if (selectedOption < 0) {
                            selectedOption = totalOptions - 1;
                        }
                        break;

                    case ConsoleKey.Spacebar:
                    case ConsoleKey.Enter:
                        Console.Clear();

                        if (selectedOption == Options.Count)
                        {
                            quit = true;
                        }
                        else
                        {
                            Options[selectedOption].Callback();

                            ConsoleColor oldBg = Console.BackgroundColor;
                            ConsoleColor oldFg = Console.ForegroundColor;

                            Console.WriteLine();
                            InvertConsoleColors();
                            Console.Write("Pulsa ENTER para volver. ");
                            InvertConsoleColors();

                            while (Console.ReadKey().Key != ConsoleKey.Enter);

                            if (!Loop)
                            {
                                quit = true;
                            }
                        }
                        break;
                }
            } while (!quit);
        }

        private static void PrintOption(string title, bool isSelected)
        {
            if (isSelected)
            {
                InvertConsoleColors();
            }

            Console.WriteLine("[{0}] {1}", isSelected ? "+" : " ", title);

            if (isSelected)
            {
                InvertConsoleColors();
            }
        }

        private static void InvertConsoleColors()
        {
            ConsoleColor oldBg = Console.BackgroundColor;
            Console.BackgroundColor = Console.ForegroundColor;
            Console.ForegroundColor = oldBg;
        }
    }
}
