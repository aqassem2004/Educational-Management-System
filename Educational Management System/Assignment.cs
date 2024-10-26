using System;
using System.Collections.Generic;
using System.Linq;
using static Educational_Management_System.EducationalSystem;
namespace Educational_Management_System
{
    class Assignment
    {
        public string AssignmentId { get; set; }
        public string Content { get; set; }
        public int MaxGrade { get; set; }
        List<string> SolutionsCode = new List<string>();
        public void ShowInfo()
        {
            Console.WriteLine($"Id: {AssignmentId}");
            Console.WriteLine($"Content: {Content}");
            Console.WriteLine($"Number of solutions: {SolutionsAccessor.Count}");
        }
        public void ShowGradesReport()
        {
            int NoEvaluated = 0;
            if (SolutionsCode.Count > 0)
            {
                int counter = 1;
                foreach (var Code in SolutionsCode)
                {
                    if (SolutionsAccessor[Code].grade != -1)
                        Console.WriteLine($"{counter++}- Student username: {StudentsAccessor[SolutionsAccessor[Code].StudentId].UserName}" +
                            $"  Student name: {StudentsAccessor[SolutionsAccessor[Code].StudentId].FirstName + " " + StudentsAccessor[SolutionsAccessor[Code].StudentId].LastName} " +
                            $" Grade: {SolutionsAccessor[Code].grade}");
                    else
                        NoEvaluated++;
                }
            }
            else
            { Console.WriteLine("No solutions have been evaluated yet.");
                if(NoEvaluated>0)
                Console.WriteLine($"You have {NoEvaluated} solutions that have not been evaluated");
            }
        }
        public void ListSolutions()
        {
            if (SolutionsCode.Count > 0)
            {
                int counter = 1;
                foreach (var Code in SolutionsCode)
                {
                    Console.Write($"{counter++}- "); SolutionsAccessor[Code].ShowInfo();
                }
            }
            else
                Console.WriteLine("No solutions have been added yet.");
           
        }
        public void ViewSolution()
        {
            ListSolutions();
            if (SolutionsCode.Count > 0)
            {
                int choise = HelperFunctions.ChoiceFromList(SolutionsAccessor.First().Value.GetType().Name, SolutionsCode.Count);
                if (choise != 0)
                    ViewSolution(SolutionsAccessor[SolutionsCode[choise - 1]]);
            }
        }
        void ViewSolution(Solution solution)
        {
            while (true)
            {
                Console.WriteLine("Choose the command\n" +
                   "\t1- Show Info\n" +
                   "\t2- Set Grade\n" +
                   "\t3- Set a Comment\n" +
                   "\t4- Back"
                   );
                short command = HelperFunctions.CheckCommand(1, 4);
                switch (command)
                {
                    case 1:
                        solution.ShowInfo();
                        break;
                    case 2:
                        { 
                            solution.SetGrade();
                        }
                        break;
                    case 3:
                        solution.SetComment();
                        break;
                    default:
                        break;
                }
                if (command == 4)
                    break;
            }
        }
        public string FindSolution(ref List<string> Solutionsid)
        {
            foreach(var code in Solutionsid)
            {
                if (SolutionsCode.Contains(code))
                    return code;
            }
            return null;
        }

        public string CreateSolution(string StudentId)
        {
            Console.Write("Enter your solution: ");
            string content = Console.ReadLine();
            Solution NewSol = new Solution(content, StudentId, AssignmentId);
            NewSol.SolutionId = HelperFunctions.GenerateRandomID((string id) => SolutionsAccessor.ContainsKey(id));
            AddSol(NewSol);
            return NewSol.SolutionId;
        }

    }
}
