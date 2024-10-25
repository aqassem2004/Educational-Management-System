using System;
using System.Collections.Generic;
using static Educational_Management_System.EducationalSystem;

namespace Educational_Management_System
{
    class Solution
    {
        public int SolutionId { get; set;}
        public string SolutionText { get; set; }
        public int grade { get; set; }
        public int Notification { get; set; }
        public List<string> Comment { get; set; }
        public string AssignmentId { get; set; }
        public string StudentId { get; set; }

        public Solution(string SolutionText, string StudentId, string AssignmentId)
        {
            grade = -1;
            Notification = 0;
            this.SolutionText = SolutionText;
            this.StudentId = StudentId;
            this.AssignmentId = AssignmentId;
            Comment = new List<string>();
        }
        public void ShowInfo()
        {
            Console.WriteLine($"Solution ID: {SolutionId}  Student username: {StudentsAccessor[StudentId].UserName}  " +
                $"Student name: {StudentsAccessor[StudentId].FirstName + " " + StudentsAccessor[StudentId].LastName}");
            if (grade != -1)
            {
                Console.WriteLine($"  Status-> Evaluated  Grade: {grade}");
            }
            else
            {
                Console.WriteLine($"  Status: Not evaluated");
            }
            Console.WriteLine(SolutionText);
            Console.WriteLine();
            Console.WriteLine("To Set/Edit grade, enter 1\n" + "To Back, enter 0");
            int choise = HelperFunctions.CheckCommand(0, 1);
            if(choise == 1)
            {
                SetEditGrade();
            }
        }
        void SetEditGrade()
        {
            int NewGrade = Convert.ToInt32(HelperFunctions.ValidateInput("Enter grade", "Invalid Entry", (string input) => !int.TryParse(Console.ReadLine(), out _) || Convert.ToInt32(input)>AssignmentsAccessor[AssignmentId].MaxGrade));
            this.grade = NewGrade;
        }

        public void SetGrade()
        {
            ShowInfo();
        }
        public void SetComment()
        {
            Console.Write("Enter Your Comment: ");
            string Comment = Console.ReadLine();
            this.Comment.Add(Comment);
        }

    }
}
