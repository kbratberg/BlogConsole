using System;
using System.Collections.Generic;
namespace soft_arch
{
    public class HrPerson
    {
       private List<Employee> employees = new List<Employee>();

    public HrPerson() {

    }

    public void hireEmployee(String firstName, String lastName, String ssn) {
        Employee e = new Employee();
        e.FirstName = firstName;
        e.LastName = lastName;
        e.ssn = ssn;
        employees.Add(e);
        orientEmployee(e);
    }

    // HRManager delegates work to employee
    private void orientEmployee(Employee emp) {
        emp.doFirstTimeOrientation("B101");
    }

    public void outputReport(String ssn) {

        // find employee in list
        foreach (Employee emp in employees) {
            if (emp.ssn == ssn) {
                // if found run report
                if (emp.metWithHr && emp.metDeptStaff
                        && emp.reviewedDeptPolicies && emp.movedIn) {
                    emp.printReport();
                }
                break;
            }
        } 
    }
    }
}
