using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicModelsLibrary
{
    public class User : IComparable
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }

        public virtual void GetUserData(string id)
        {
            UserId = id;
            Console.Write("Enter name : ");
            Name = Console.ReadLine();
            Console.Write("Enter date of birth (DD/MM/YYYY) : ");
            DateTime dt;
            while(!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, DateTimeStyles.None, out dt))
            {
                Console.Write("Invalid input. Please re-enter : ");
            }
            while (dt > DateTime.Today)
            {
                Console.Write("Invalid date of birth. Please enter a past date : ");
                DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
            }
            DOB = dt;
            Console.Write("Enter password : ");
            Password = Console.ReadLine();
            
        }


        public override string ToString()
        {
            return "--\nUser ID : " + UserId
                + "\nName : " + Name
                + "\nD.O.B : " + DOB.ToString("dd/MM/yyyy") 
                + "\nType : " + Type;
        }

        public int CompareTo(object obj)
        {
            User u1, u2;
            u1 = this;
            u2 = (User)obj;
            //if (c1.Id < c2.Id) return -1;
            //else if(c1.Id > c2.Id) return 1;
            //else return 0;
            return u1.UserId.CompareTo(u2.UserId);
        }
    }
}
