using System;
using System.Collections.Generic;

namespace Soft_arch_encapsulation
{
    class Program
    {
        static void Main(string[] args)
        {
             Company company = new Company();

        // Startup delegates work to Company which then delegates work to HRManager
        company.hireEmployee("John", "Doe", "444-44-4444");
        }
    }
}
