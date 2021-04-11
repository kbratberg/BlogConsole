using System;

using System.Collections.Generic;

namespace Soft_arch_encapsulation
{
    public class Company
    {
         private HrPerson hr;

    public Company() {
        hr = new HrPerson();
    }

    public void hireEmployee(String firstName, String lastName, String ssn) {
        hr.hireEmployee(firstName, lastName, ssn);
        hr.outputReport(ssn);
    }
    }
}
