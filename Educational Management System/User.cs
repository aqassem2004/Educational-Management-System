using System;

namespace Educational_Management_System
{
    class User
    {
        private string userName;
        private string password;
        public string UserId { get; set; }
        public UserRoles Role { get; set; } // Using enum for roles

        public string UserName
        {
            get { return userName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    userName = value;
                else
                    throw new ArgumentException("Username cannot be empty.");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (value.Length >= 8)
                    password = value;
                else
                    throw new ArgumentException("Password must be at least 8 characters long.");
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string InterfaceMassege()
        {
            return "==============="+Enum.GetName(typeof(UserRoles), this.Role) + " Interface ================";
        }
    }
}
