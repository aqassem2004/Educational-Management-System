using System;
using static Educational_Management_System.EducationalSystem;
namespace Educational_Management_System
{
    class DoctorInterface
    {
        string DoctorUserName;
        public DoctorInterface(string DoctorId)
        {
            this.DoctorUserName = DoctorId;
            RunDoctorInterface();
        }
        public void RunDoctorInterface()
        {
            Console.WriteLine(DoctorsAccessor[DoctorUserName].InterfaceMassege());
            while (true)
            {
                Console.WriteLine("Choose the command\n" +
                   "\t1- List Courses\n" +
                   "\t2- Create course\n" +
                   "\t3- View Course\n" +
                   "\t4- Log out"
                   );
                short command = HelperFunctions.CheckCommand(1, 4);
                switch (command)
                {
                    case 1:
                        DoctorsAccessor[DoctorUserName].ListCourse();
                        break;
                    case 2:
                        DoctorsAccessor[DoctorUserName].CreateCourse();
                        break;
                    case 3:
                        DoctorsAccessor[DoctorUserName].ViewCourse();
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
