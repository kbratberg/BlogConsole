using System;

namespace YourName
{
    class Program
    {
        static void Main(string[] args)
        {
             // display a literal value
            Console.WriteLine("What is your name?");
            // input a value and assign it to a string variable
            string name = Console.ReadLine();
            // display the string variable
            Console.WriteLine("Hello, " + name);
            Console.WriteLine("Hello, {0}", name);
        }
    }
}
