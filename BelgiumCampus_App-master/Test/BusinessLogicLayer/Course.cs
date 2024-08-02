using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.BusinessLogicLayer
{
    internal class Course
    {
        string ModuleName, ModuleDesc, Links;
        int CourseCode;

        public Course(string moduleName, string moduleDesc, string links, int courseCode)
        {
            ModuleName1 = moduleName;
            ModuleDesc1 = moduleDesc;
            Links1 = links;
            CourseCode1 = courseCode;
        }

        public string ModuleName1 { get => ModuleName; set => ModuleName = value; }
        public string ModuleDesc1 { get => ModuleDesc; set => ModuleDesc = value; }
        public string Links1 { get => Links; set => Links = value; }
        public int CourseCode1 { get => CourseCode; set => CourseCode = value; }
    }
}
