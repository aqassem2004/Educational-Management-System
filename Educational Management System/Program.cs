using System.Text;
using System.Threading.Tasks;

namespace Educational_Management_System
{
    enum UserRoles
    {
        Doctor = 1,
        Student,
        TeacherAssistant
    }
    class Program
    {
        static void Main(string[] args)
        {
            EducationalSystem system = new EducationalSystem();
            system.SystemRun();
        }
    }
}
