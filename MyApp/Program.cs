using System;
using System.Linq;
using MyLib;

namespace MyApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintArgs(args);

            var instance = Arrange();
            Act(instance);

            Console.WriteLine("Done. Press any key to quit.");
            Console.ReadKey();
        }

        internal static void PrintArgs(params string[] args)
        {
            if (args.Any())
                Console.WriteLine("Arguments: {0}", String.Join(" ", args));
        }

        internal static Class1 Arrange()
        {
            Console.WriteLine("Instantiating a class...");
            var instance = new Class1();
            return instance;
        }

        internal static void Act(Class1 instance)
        {
            Console.WriteLine("Acting on instance...");
            instance.SomeCoveredMethod();
            instance.SomeUncoveredMethod();
        }
    }
}
