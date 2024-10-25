using System;
using System.Collections.Generic;
using System.Linq;

namespace Educational_Management_System
{
    class EducationalSystem
    {
        static Dictionary<string, User> Users;
        static Dictionary<string, Doctor> Doctors;
        static Dictionary<string, Student> Students;
        static Dictionary<string, Course> Courses;
        static Dictionary<string, Solution> Solutions;
        static Dictionary<string, Assignment> Assignments;
        // Internal properties for Read only
        internal static Dictionary<string, User> UsersAccessor => Users;
        internal static Dictionary<string, Doctor> DoctorsAccessor => Doctors;
        internal static Dictionary<string, Student> StudentsAccessor => Students;
        internal static Dictionary<string, Course> CoursesAccessor => Courses;
        internal static Dictionary<string, Solution> SolutionsAccessor => Solutions;
        internal static Dictionary<string, Assignment> AssignmentsAccessor => Assignments;
        public EducationalSystem()
        {
            Users = new Dictionary<string, User>();
            Doctors = new Dictionary<string, Doctor>();
            Students = new Dictionary<string, Student>();
            Courses = new Dictionary<string, Course>();
            Solutions = new Dictionary<string, Solution>();
            Assignments = new Dictionary<string, Assignment>();
            database();
        }
        void database()
        {
            User user = new User
            {
                UserName = "aqasem",
                UserId = "12345678",
                Password = "12345678",
                FirstName = "Ahmed",
                LastName = "Qassem",
                Age = 35,
                Email = "Aqassemm2004@gmail.com",
                Role = UserRoles.Doctor,
                Sex = "Male"
                
            };
            Users.Add(user.UserName, user);
            Doctors.Add(user.UserName, CreateNewDoctor(user));
        }
        public void SystemRun()
        {
            Console.WriteLine("========Educational Management System========\n");
            while (true)
            {
                Console.WriteLine("Choose the command\n" +
                    "\t1- Sign in\n" +
                    "\t2- Sign up");
                short command = HelperFunctions.CheckCommand(1,2);
                if (command == 1)
                    SignInWeb();
                else
                    SignUpWeb();
            }
        }
        private void SignInWeb()
        {
            Console.WriteLine("============Sign In============\n");
            while (true)
            {
                Console.Write("Enter Username: ");
                string userName = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = HelperFunctions.ReadPassword();
                Console.WriteLine();

                User user = HelperFunctions.AuthenticateUser(userName, password);
                if (user == null)
                {
                    Console.WriteLine("The username and/or password you specified are not correct.\n");
                    Console.WriteLine("If you don't have an account already, you can sign up.\n" +
                        "To sign up enter 1, or to try again enter 0.");
                    short command = HelperFunctions.CheckCommand(0, 1);
                    if (command == 1)
                    {
                        SignUpWeb();
                        break;
                    }
                }
                else
                {
                    if (user.Role == UserRoles.Doctor)
                        ShowDoctorInterface(user);
                    else if (user.Role == UserRoles.Student)
                        ShowStudentInterface(user);
                    else
                        ShowTeacherAssistantInterface(user);
                    break;
                }
            }
        }

        private void SignUpWeb()
        {
            User newUser = new User();
            Console.WriteLine("============Sign Up============\n");
            newUser.UserName = HelperFunctions.ValidateInput("Enter Username: ", "Username already used.\n", (string input) => Users.ContainsKey(input));

            newUser.Password = HelperFunctions.ValidatePassword("Enter Password: ", "The password is too small.\n", (string input) => input.Length < 8);
            newUser.UserId = HelperFunctions.GenerateRandomID((string id) => Users.ContainsKey(id));
            Console.WriteLine($"User id: {newUser.UserId}");
            Console.WriteLine("Select gender: \n" +
                "\t1- Male\n" +
                "\t2- Female");
            newUser.Sex = HelperFunctions.ValidateInput("Enter gender: ", "Gender is wrong", (string input) => !(input.ToLower() == "male") && !(input.ToLower() == "female"));
            newUser.Sex[0].MakeUpper();
            foreach (var type in Enum.GetNames(typeof(UserRoles)))
                Console.WriteLine($"{(int)Enum.Parse(typeof(UserRoles), type)}- {type}");

            newUser.Role = (UserRoles)Convert.ToInt32(HelperFunctions.ValidateInput("Select your role: ", HelperFunctions.CommandError, (string input) => !int.TryParse(input, out _) || !Enum.IsDefined(typeof(UserRoles), Convert.ToInt32(input))));

            newUser.Email = HelperFunctions.ValidateInput("Enter Email: ", "This is not a valid email.\n", (string input) => !input.Contains(".com") || input.Count(c => c == '@') != 1 || input.StartsWith("@"));
            newUser.FirstName = HelperFunctions.ValidateInput("Enter first name: ", "", (string input) => !(input is  string));
            newUser.LastName = HelperFunctions.ValidateInput("Enter last name: ", "", (string input) => !(input is string));
            newUser.Age = Convert.ToInt32(HelperFunctions.ValidateInput("Enter age: ", "Really, this is your age?\n", (string input) => !int.TryParse(input, out _) || !HelperFunctions.ValidateAge(newUser.Role, Convert.ToInt32(input))));

            Users.Add(newUser.UserName, newUser);
            if (newUser.Role == UserRoles.Doctor)
                Doctors.Add(newUser.UserName, CreateNewDoctor(newUser));
            else if(newUser.Role == UserRoles.Student)
                Students.Add(newUser.UserName, CreateNewStudent(newUser));
            Console.WriteLine("The account has been created successfully.");
        }
        Doctor CreateNewDoctor(User newUser)
        {
            Doctor doctor = new Doctor
            {
                UserName = newUser.UserName,
                Password = newUser.Password,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Age = newUser.Age,
                Email = newUser.Email,
                Role = newUser.Role,
                // Add any additional fields specific to Doctor
            };
            return doctor;
        }
        Student CreateNewStudent(User newUser)
        {
            Student student = new Student
            {
                UserName = newUser.UserName,
                Password = newUser.Password,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Age = newUser.Age,
                Email = newUser.Email,
                Role = newUser.Role,
                // Add any additional fields specific to Student
            };
            return student;
        }
        private void ShowDoctorInterface(User user)
        {
            DoctorInterface doctorInterface = new DoctorInterface(user.UserName);
            //Console.WriteLine("Doctor Interface - Coming soon.");
        }

        private void ShowStudentInterface(User user)
        {
            Console.WriteLine("Student Interface - Coming soon.");
        }

        private void ShowTeacherAssistantInterface(User user)
        {
            Console.WriteLine("Teacher Assistant Interface - Coming soon.");
        }
    }
}