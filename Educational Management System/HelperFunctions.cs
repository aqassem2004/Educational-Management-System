using System;
using System.Collections.Generic;
using System.Text;
using static Educational_Management_System.EducationalSystem;

namespace Educational_Management_System
{
    static class HelperFunctions
    {
        public static char MakeUpper(this char c)
        {
            return ((char)((int)c - 32));
        }
        public static string CommandError = "!Wrong command!\nPlease enter the number corresponding to the command.\n";
        public static string ValidateInput(string enterMessage, string errorMessage, Func<string, bool> validationFunc)
        {
            Console.Write(enterMessage);
            string input = Console.ReadLine();
            while (validationFunc(input))
            {
                Console.WriteLine(errorMessage);
                Console.Write(enterMessage);
                input = Console.ReadLine();
            }
            return input;
        }
        public static string ValidatePassword(string enterMessage, string errorMessage, Func<string, bool> validationFunc)
        {
            Console.Write(enterMessage);
            string input = ReadPassword();
            while (validationFunc(input))
            {
                Console.WriteLine(errorMessage);
                Console.Write(enterMessage);
                input = ReadPassword();
            }
            return input;
        }
        public static string GenerateRandomID(Func<string, bool> validationFunc)
        {

            string randomId = GenerateRandomNumericId(8);
            while (validationFunc(randomId))
            {
                randomId = GenerateRandomNumericId(8);
            }
            return randomId;
        }
        public static User AuthenticateUser(string userName, string password)
        {
            if (UsersAccessor.ContainsKey(userName) && UsersAccessor[userName].Password == password)
                return UsersAccessor[userName];
            return null;
        }

        public static bool ValidateAge(UserRoles role, int age)
        {
            switch (role)
            {
                case UserRoles.Doctor:
                    return age >= 32 && age <= 100;
                case UserRoles.TeacherAssistant:
                    return age >= 23 && age < 32;
                case UserRoles.Student:
                    return age >= 18 && age <= 27;
                default:
                    return false;
            }
        }
        public static short CheckCommand(int min , int max)
        {
            Console.Write($"Select command in [{min} - {max}]: ");
            short command;
            while (!short.TryParse(Console.ReadLine(), out command) || (command > max || command < min))
            {
                Console.WriteLine(CommandError);
            }
            return command;
        }
        public static int ChoiceFromList(string ListType , int max)
        {
            Console.WriteLine("\nIf you want to back enter 0.");
            Console.Write($"Which ith [1 - {max}] {ListType} to view? ");
            int command;
            while (!int.TryParse(Console.ReadLine(), out command) || (command > max || command < 0))
            {
                Console.WriteLine(CommandError);
            }
            return command;
        }
        static string GenerateRandomNumericId(int length)
        {
            var random = new Random();
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
                result.Append(random.Next(0, 10));

            return result.ToString();
        }

        public static string ReadPassword()
        {
            var password = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
            } while (key != ConsoleKey.Enter);

            return password;
        }
    }
}
