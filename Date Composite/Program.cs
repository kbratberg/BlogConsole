using System;

namespace Date_Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime today = DateTime.Now;
            Console.WriteLine("1. {0}", today);
            // display month
            Console.WriteLine("2. {0:MMMM}", today);
            //display written month and date
            Console.WriteLine("3. {0:MMMM} {0:dd}", today);
            // display written month, date, and 4 digit year
            Console.WriteLine("4. {0:MMMM} {0:dd}, {0:yyyy}", today);
            
            Console.WriteLine("{0:yyyy}.{0:MM}.{0:dd}", today);
            Console.WriteLine("Day {0:dd} of {0:MMMM}, {0:yyyy}", today);
            Console.WriteLine("Year:{0:yyyy},Month:{0:MM},Day:{0:dd}", today);
            Console.WriteLine("{0:dddd}", today);
            Console.WriteLine("{0:HH}:{0:mm} {0:tt}", today);
            Console.WriteLine("h:{0:HH},m:{0:mm},s:{0:ss}", today);
            Console.WriteLine("{0:yyyy}.{0:MM}.{0:dd}.{0:HH}.{0:mm}.{0:ss}", today);

            DateTime date = DateTime.Now;
            Console.WriteLine($"{date:yyyy}.{date:MM}.{date:dd}", today);
            Console.WriteLine($"Day {date:dd} of {date:MMMM}, {date:yyyy}");
            Console.WriteLine($"Year:{date:yyyy},Month:{date:MM},Day:{date:dd}");
            Console.WriteLine($"{date:dddd}");
            Console.WriteLine($"{date:HH}:{date:mm} {date:tt}");
            Console.WriteLine($"h:{date:HH},m:{date:mm},s:{date:ss}");
            Console.WriteLine($"{date:yyyy}.{date:MM}.{date:dd}.{date:HH}.{date:mm}.{date:ss}");



        }
    }
}
