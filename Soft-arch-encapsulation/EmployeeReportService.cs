using System;

using System.Collections.Generic;

namespace Soft_arch_encapsulation
{
    public class EmployeeReportService
    {
        private String report = "";

    public void addData(String data) {
        report += data;
    }

    public void clearReport() {
        report = "";
    }

    public void outputReport() {
        Console.WriteLine(report);
    }

    }
}
