using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicModelsLibrary
{
    public class Patient : User
    {
        public string Remarks;

        public Patient()
        {
            Type = "Patient";
        }

        public override void GetUserData(string id)
        {
            base.GetUserData(id);
            Console.Write("Enter remarks : ");
            Remarks = Console.ReadLine();
        }

        public override string ToString()
        {
            return base.ToString()
                + "\nRemarks : " + Remarks;
        }
    }
}
