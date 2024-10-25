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
        List<string> AssignmentsCode = new List<string>();
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

    }
}
