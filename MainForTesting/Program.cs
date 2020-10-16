using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuBuilder;

// Demonstration of Menu's usage.

namespace MainForTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();

            menu.Options.Add(new Option("Option 1", Opt1));
            menu.Options.Add(new Option("Option 2", Opt2));
            menu.Options.Add(new Option("Option 3", Opt2));

            menu.Loop = true;
            menu.QuitOption = true;

            menu.Serve();
        }

        private static void Opt1()
        {
            Console.WriteLine("Option 1...");
        }

        private static void Opt2()
        {
            Console.WriteLine("Option 2...");
        }

        private static void Opt3()
        {
            Console.WriteLine("Option 3...");
        }
    }
}
