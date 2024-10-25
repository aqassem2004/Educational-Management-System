using System;
using System.Collections.Generic;
using System.Linq;
using static Educational_Management_System.EducationalSystem;
namespace Educational_Management_System
{
    class Doctor : User
    {
        List<string> CoursesCode = new List<string>();
        public void ListCourse()
        {
            
            if (CoursesCode.Count > 0)
            {
                
                int counter = 1;
                foreach(var Code in CoursesCode)
                {
                    Console.WriteLine($"{counter++}- Course name: {CoursesAccessor[Code].CourseName} with Code: {Code}");
                    if (CoursesAccessor[Code].RegisteredStudents.Count > 0)
                    {
                        Console.Write("Registered Students ID: ");
                        foreach (var student in CoursesAccessor[Code].RegisteredStudents)
                        {
                            Console.Write($"{student} ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No student has registered for the course yet.");
                    }
                    Console.WriteLine();
                }
            }
            else
                Console.WriteLine("You have not added any courses yet.");
            


        }
        public void CreateCourse()
        {
            Course NewCourse = new Course();
            NewCourse.CourseCode = "CS" + HelperFunctions.GenerateRandomID((string id) => CoursesAccessor.ContainsKey("CS" + id));
            Console.WriteLine($"Course code is: {NewCourse.CourseCode}");
            NewCourse.CourseName = HelperFunctions.ValidateInput("Course name: ", "", (string input) => !(input is string));
            NewCourse.Owner = this.FirstName + this.LastName;
            CoursesAccessor.Add(NewCourse.CourseCode,NewCourse);
            CoursesCode.Add(NewCourse.CourseCode);
            Console.WriteLine("The course has been create successfully.\n" +
                "Now you can add assignments to it.\n");

        }
        public void ViewCourse()
        {
            ListCourse();
            if (CoursesCode.Count > 0)
            {
                int choise = HelperFunctions.ChoiceFromList(CoursesAccessor.First().Value.GetType().Name, CoursesAccessor.Count);
                if (choise != 0)
                    ViewCourse(CoursesAccessor[CoursesCode[choise - 1]]);
            }
        }
        void ViewCourse(Course course)
        {
            while (true)
            {
                Console.WriteLine("Choose the command\n" +
                   "\t1- List Assignments\n" +
                   "\t2- Create Assignment\n" +
                   "\t3- View Assignment\n" +
                   "\t4- Back"
                   );
                short command = HelperFunctions.CheckCommand(1, 4);
                switch (command)
                {
                    case 1:
                        course.ListAssignments();
                        break;
                    case 2:
                        course.CreateAssignment();
                        break;
                    case 3:
                        {
                            course.ViewAssignment();
                        }
                        break;
                    default:
                        break;
                }
                if (command == 4)
                    break;
            }
        }
    }
}
