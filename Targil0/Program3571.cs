using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welecome3571();
            Welecome9647();
            Console.ReadKey();
        }

        static partial void Welecome9647();

        private static void Welecome3571()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
