using System;
using System.Collections.Generic;
using System.Linq;
using static Educational_Management_System.EducationalSystem;

namespace Educational_Management_System
{
    class Student : User
    {
        public List<string> CoursesRegisterId = new List<string>();
        public List<string> Solutionsid = new List<string>();
        public void RegisterInCourse()
        {
            int counter = 1;
            foreach(var Course in CoursesAccessor)
            {
                if(!CoursesRegisterId.Contains(Course.Key))
                {
                    Console.WriteLine($"{counter++}- Course name: {Course.Value.CourseName}" +
                        $" Code: {Course.Value.CourseCode} by doctor: {Course.Value.Owner}");
                }
            }
            Console.WriteLine("Choose a course to register in by entering the course code or entering 0 to return");
            string code = HelperFunctions.ValidateInput("Enter course code: ", "There is no course for this code!", (string input) => (!(input is string) && input != "0") || !CoursesAccessor.ContainsKey(input));
            if(code!="0")
            {
                CoursesRegisterId.Add(code);
                CoursesAccessor[code].RegisteredStudents.Add(UserName);
                Console.WriteLine("Done!\n");
            }
        }
        public void ListMyCourses()
        {
            int counter = 1;
            if (CoursesRegisterId.Count > 0)
            {
                foreach (var code in CoursesRegisterId)
                {
                    Console.WriteLine($"{counter++}- Course name: {CoursesAccessor[code].CourseName}  Code: {code}" +
                        $"By Doc: {CoursesAccessor[code].Owner}");
                }
            }
            else
                Console.WriteLine("Your list is empty.");
           
        }
        public void ViewCourse()
        {
            ListMyCourses();
            if(CoursesRegisterId.Count > 0)
            {
                int choise = HelperFunctions.ChoiceFromList(CoursesAccessor.First().Value.GetType().Name, CoursesRegisterId.Count);
                if (choise != 0)
                {
                    CoursesAccessor[CoursesRegisterId[choise - 1]].ViewCourseForStudent(ref Solutionsid);
                }
                else return;
                while (true)
                {
                    Console.WriteLine("Choose the command\n" +
                       "\t1- UnRegister from Course\n" +
                       "\t2- Submit solution\n" +
                       "\t3- Back"
                       );
                    short command = HelperFunctions.CheckCommand(1, 3);
                    switch (command)
                    {
                        case 1:
                            UnRegisterFromCourse(CoursesRegisterId[choise - 1]);
                            break;
                        case 2:
                            SubmitSolution(CoursesRegisterId[choise - 1]);
                            break;
                        default:
                            break;
                    }
                    if (command == 3 || command == 1)
                        break;
                }
            }
        }
        void UnRegisterFromCourse(string UnRegCourse)
        {
            foreach(var code in Solutionsid)
            {
                SolutionsAccessor.Remove(code);
            }
            CoursesRegisterId.Remove(UnRegCourse);
            Console.WriteLine("UnRegister Successfully");
        }
        void SubmitSolution(string CourseCode)
        {
            CoursesAccessor[CourseCode].ListUnSubmittedAssignment(ref Solutionsid, UserName);
        }
        public void GradesReport()
        {
            if (CoursesRegisterId.Count > 0)
            {
                Console.WriteLine("Final grade based on evaluated solutions only.");
                foreach (var course in CoursesRegisterId)
                {
                    Console.Write($"Course {CoursesAccessor[course].CourseName} Code {CoursesAccessor[course].CourseCode} ");
                    int totalsub = 0, totalgrade = 0, totalMaxGrade = 0;
                    foreach (var sol in Solutionsid)
                    {
                        if (CoursesAccessor[course].AssignmentsCode.Contains(SolutionsAccessor[sol].AssignmentId) && SolutionsAccessor[sol].grade != -1)
                        {
                            totalsub++; totalgrade += SolutionsAccessor[sol].grade;
                            totalMaxGrade += AssignmentsAccessor[SolutionsAccessor[sol].AssignmentId].MaxGrade;
                        }
                    }
                    foreach (var assignment in CoursesAccessor[course].AssignmentsCode)
                        totalMaxGrade += AssignmentsAccessor[assignment].MaxGrade;
                    Console.Write($"total evaluation submitted Assignment is {totalsub} - Grade {totalgrade} / {totalMaxGrade}\n");
                }
            }
            else
                Console.WriteLine("You have not registered for any course yet.");
        }
    }
}
