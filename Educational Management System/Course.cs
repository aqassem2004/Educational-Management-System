using System;
using System.Collections.Generic;
using System.Linq;
using static Educational_Management_System.EducationalSystem;

namespace Educational_Management_System
{
    class Course
    {
        
        public string CourseCode { set; get; }
        public string CourseName { get; set; }
        public string Owner { get; set; }
        public List<string> RegisteredStudents { get; set; }
        public List<string> AssignmentsCode = new List<string>();
        public Course()
        {
            RegisteredStudents = new List<string>();
        }
        public void ListAssignments()
        {
            if (AssignmentsCode.Count > 0)
            {
                int counter = 1;
                foreach (var Code in AssignmentsCode)
                {
                    Console.WriteLine($"{counter++}- Assignment ID: {AssignmentsAccessor[Code].AssignmentId}");
                }
            }
            else
                Console.WriteLine("You have not added any Assignments yet.");
        }
        public void CreateAssignment()
        {
            Assignment assignment = new Assignment();
            assignment.AssignmentId = HelperFunctions.GenerateRandomID((string input) => AssignmentsAccessor.ContainsKey(input));
            Console.WriteLine($"Assignment Id: {assignment.AssignmentId}");
            Console.Write("Enter assignment content: ");
            assignment.Content = Console.ReadLine();
            assignment.MaxGrade = Convert.ToInt32(HelperFunctions.ValidateInput("Enter max grade between 100 - 60: ", "Invalid Entry", (string input) => !int.TryParse(input, out _) || Convert.ToInt32(input)>100 || Convert.ToInt32(input) <60 ));
            AssignmentsAccessor.Add(assignment.AssignmentId, assignment);
            AssignmentsCode.Add(assignment.AssignmentId);
            Console.WriteLine("The assignment has been added successfully.");
        }
        public void ViewAssignment()
        {
            ListAssignments();
            if (AssignmentsCode.Count > 0)
            {
                int choise = HelperFunctions.ChoiceFromList(AssignmentsAccessor.First().Value.GetType().Name, AssignmentsCode.Count);
                if (choise != 0)
                    ViewAssignment(AssignmentsAccessor[AssignmentsCode[choise - 1]]);
            }
        }
         void ViewAssignment(Assignment assignment)
         {
            while (true)
            {
                Console.WriteLine("Choose the command\n" +
                   "\t1- Show Info\n" +
                   "\t2- Show Grades Report\n" +
                   "\t3- List Solutions\n" +
                   "\t4- View Solution\n" +
                   "\t5- Back"
                   );
                short command = HelperFunctions.CheckCommand(1, 5);
                switch (command)
                {
                    case 1:
                        assignment.ShowInfo();
                        break;
                    case 2:
                        assignment.ShowGradesReport();
                        break;
                    case 3:
                        assignment.ListSolutions();
                        break;
                    case 4:
                        assignment.ViewSolution();
                        break;
                    default:
                        break;
                }
                if (command == 5)
                    break;
            }
         }
        public void ViewCourseForStudent(ref List<string> Solutionsid)
        {
            Console.WriteLine($"Course: {CourseName}  Code: {CourseCode}" +
                       $"- By Doc: {Owner}");
            Console.WriteLine($"Course has {AssignmentsCode.Count} Assignment");

            if(AssignmentsCode.Count>0)
            {
                int counter = 1;
                foreach(var code in AssignmentsCode)
                {
                    Console.Write($"Assignment {counter++} ");
                    string SolCode = AssignmentsAccessor[code].FindSolution(ref Solutionsid);
                    Console.Write($"{(SolCode != null ? "submitted" : "NOT submitted")} ");
                    if (SolCode != null)
                    {
                        if(SolutionsAccessor[SolCode].grade==-1)
                            Console.WriteLine($"- NA / { AssignmentsAccessor[code].MaxGrade}"); 
                        else
                            Console.WriteLine($"- {SolutionsAccessor[SolCode].grade} / { AssignmentsAccessor[code].MaxGrade}");

                    }
                    else
                        Console.WriteLine($"- NA / { AssignmentsAccessor[code].MaxGrade}");
                }
            }
        }
        public void ListUnSubmittedAssignment(ref List<string> Solutionsid , string StudentId)
        {
            if (AssignmentsCode.Count > 0)
            {
                int counter = 1, c = 1;
                List<string> UnSubmittedAssignment = new List<string>();
                foreach (var code in AssignmentsCode)
                {

                    string SolCode = AssignmentsAccessor[code].FindSolution(ref Solutionsid);
                    if (SolCode == null)
                    {
                        Console.Write($"{c++}- Assignment {counter++} \n");
                        UnSubmittedAssignment.Add(code);
                    }
                    else
                        counter++;
                }
                if (UnSubmittedAssignment.Count > 0)
                {
                    int choise = HelperFunctions.ChoiceFromList("Assignment", UnSubmittedAssignment.Count);
                    if (choise != 0)
                    {
                        string SolCode = AssignmentsAccessor[UnSubmittedAssignment[choise - 1]].CreateSolution(StudentId);
                        StudentsAccessor[StudentId].Solutionsid.Add(SolCode);
                    }
                }
                else
                    Console.WriteLine("You have delivered all of them.");

            }
            else
                Console.WriteLine("There are no assignments.");
        }

    }
}
