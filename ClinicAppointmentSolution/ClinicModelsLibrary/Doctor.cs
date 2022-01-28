using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicModelsLibrary
{
    public class Doctor : User
    {
        public int experience;
        public string specialty;

        public Doctor()
        {
            Type = "Doctor";
        }

        public override void GetUserData(string id)
        {
            base.GetUserData(id);
            Console.Write("Enter experience (in years) : ");
            while(!Int32.TryParse(Console.ReadLine(), out experience))
            {
                Console.Write("Invalid input. Please re-enter : ");
            }
            Console.Write("Enter specialty : ");
            specialty = Console.ReadLine();
        }

        public override string ToString()
        {
            return base.ToString() 
                + "\nExperience : " + experience 
                + " year(s)\nSpecialty : " + specialty;
        }

    }
}
