using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.BusinessLogicLayer
{
    internal class Students
    {
        string FirstName, Surname, Gender, Phone;
        int  CourseCode;
        DateTime DOB;

        public Students(string firstName, string surname, string gender, string phone,  int courseCode, DateTime dOB)
        {
            FirstName1 = firstName;
            Surname1 = surname;
            Gender1 = gender;
            Phone1 = phone;
            CourseCode1 = courseCode;
            DOB1 = dOB;
        }

        public string FirstName1 { get => FirstName; set => FirstName = value; }
        public string Surname1 { get => Surname; set => Surname = value; }
        public string Gender1 { get => Gender; set => Gender = value; }
        public string Phone1 { get => Phone; set => Phone = value; }
        public int CourseCode1 { get => CourseCode; set => CourseCode = value; }
        public DateTime DOB1 { get => DOB; set => DOB = value; }
    }
}
