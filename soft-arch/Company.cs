using System;

namespace soft_arch
{
    public class Company
    {
         private HrPerson hr;

    public Company() {
        hr = new HrPerson();
    }

    public void hireEmployee(string firstName, string lastName, string ssn) {
        hr.hireEmployee(firstName, lastName, ssn);
        hr.outputReport(ssn);
    }

    }
}
