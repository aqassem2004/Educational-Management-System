using System;
using static Educational_Management_System.EducationalSystem;

namespace Educational_Management_System
{
    class StudentInterface
    {
        public string StudentId { get; set; }
        public StudentInterface(string StudentId)
        {
            this.StudentId = StudentId;
            RunStudentInterface();
        }
        public void RunStudentInterface()
        {
            Console.WriteLine(StudentsAccessor[StudentId].InterfaceMassege());
            while (true)
            {
                Console.WriteLine("Choose the command\n" +
                   "\t1- Register in course\n" +
                   "\t2- List My Courses\n" +
                   "\t3- View a course\n" +
                   "\t4- Grades Report\n" +
                   "\t5- Log out"
                   );
                short command = HelperFunctions.CheckCommand(1, 5);
                switch (command)
                {
                    case 1:
                        StudentsAccessor[StudentId].RegisterInCourse();
                        break;
                    case 2:
                        StudentsAccessor[StudentId].ListMyCourses();
                        break;
                    case 3:
                        StudentsAccessor[StudentId].ViewCourse();
                        break;
                    case 4:
                        StudentsAccessor[StudentId].GradesReport();
                        break;
                    default:
                        break;
                }
                if (command == 5)
                    break;
            }

        }
    }
}
