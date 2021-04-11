using System;

namespace soft_arch
{
    public class Employee
    {
        private string _firstName; 
                public String FirstName{get{ return _firstName;} set{if (value == null){
                    throw new ArgumentException("First Name is " + REQ_MESG);
                }
                _firstName = value;}}
            
        private string _lastName;
        public String LastName{get{ return _lastName;} set{if (value == null){
                    throw new ArgumentException("Last Name is " + REQ_MESG);
                }
                _lastName = value;}}
        private string _ssn;
        public String ssn{get{ return _ssn;} set{if (value == null || value.Length < ssnMin || value.Length > ssnMax ){
                    throw new ArgumentException("ssn is" + REQ_MESG + " and either " + ssnMin + " or " + ssnMax +" characters.");
                }
                _ssn = value;}}
        public const int ssnMin = 9;
        public const int ssnMax = 11;

        public const string REQ_MESG = " is manditory";
        public const string NEWLINE = "\n";

        public Boolean metWithHr {get; set;}
        public Boolean metDeptStaff{get; set;}
        public Boolean reviewedDeptPolicies{get; set;}
        public Boolean movedIn{get; set;}
        
        private string _cubeId;
        public string CubeId{get{return _cubeId;}set{if(value == null){
            throw new ArgumentException("Cube Id is " + REQ_MESG);
            }
            _cubeId = value;}}
        private DateTime _orientationDate;
        private EmployeeReportService reportService = new EmployeeReportService();
        
        public DateTime OrientationDate{
            get{return _orientationDate;}
            set{_orientationDate = new DateTime();}
    }
    public void doFirstTimeOrientation(String cubeId) {
        
        meetWithHRForBenefitAndSalaryInfo();
        meetDepartmentStaff();
        reviewDeptPolicies();
        moveIntoCubicle(cubeId);
    }
        private void meetWithHRForBenefitAndSalaryInfo(){
            metWithHr = true;
            reportService.addData(_firstName + " " + _lastName + " met with HR on " + OrientationDate.ToString("D") + NEWLINE);
        }
        
        private void meetDepartmentStaff() {
        metDeptStaff = true;
        reportService.addData(_firstName + " " + _lastName + " met with dept staff on "
                + OrientationDate.ToString("D") + NEWLINE);
    }

    public void reviewDeptPolicies() {
        reviewedDeptPolicies = true;
        reportService.addData(_firstName + " " + _lastName + " reviewed dept policies on "
                + OrientationDate.ToString("D") + NEWLINE);
    }
    public void moveIntoCubicle(String cubeId) {
        CubeId = cubeId;

        movedIn = true;
        reportService.addData(_firstName + " " + _lastName + " moved into cubicle "
                + cubeId + " on " + OrientationDate.ToString("D") + NEWLINE);
    }

    public void printReport() {
        reportService.outputReport();
    }
    }
}
