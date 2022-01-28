using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicModelsLibrary;

namespace ClinicAppointmentApp
{
    class ManageUser
    {
        List<User> users = new List<User>();

        public void RegisterUser()
        {
            Console.Write("Is user a doctor (Y/n) : ");
            string type = Console.ReadLine();
            User user;

            switch (type)
            {
                case "Y":
                    user = new Doctor();
                    break;
                case "n":
                    user = new Patient();
                    break;
                default:
                    Console.WriteLine("Invalid Entry. User registered as 'Patient'.");
                    user = new Patient();
                    break;
            }

            Console.Write("Enter user ID : ");
            string id = Console.ReadLine();
            User u = GetUserById(id);
            while (u != null)
            {
                Console.Write("User ID taken. Please enter another user ID : ");
                u = GetUserById(Console.ReadLine());
            }
            user.GetUserData(id);
            users.Add(user);
            Console.WriteLine("Registration successful. Please login to proceed.");
        }

        public void DisplayAllUsers()
        {
            users.Sort();
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine(users[i]);
            }
        }

        public User GetUserById(string id)
        {
            User user = users.Find(u => u.UserId == id);
            return user;
        }

        public User Login()
        {
            Console.Write("Enter your user ID : ");
            string id = Console.ReadLine();

            Console.Write("Enter your password : ");
            string pass = Console.ReadLine();

            User user = GetUserById(id);

            if (user != null && user.Password == pass)
            {
                Console.WriteLine("Login successful.");
                return user;
            }
            else
                Console.WriteLine("Login error. Please check your user ID and password.");

            return null;
        }

        public void DoctorList()
        {
            Console.WriteLine("\nAvailable Doctors :");
            var myDoctors = users.Where(u => u.Type == "Doctor");
            foreach (var item in myDoctors)
            {
                Doctor d = (Doctor)item;
                Console.WriteLine("--");
                Console.WriteLine("Doctor's Name : " + d.Name);
                Console.WriteLine("User ID : " + d.UserId);
                Console.WriteLine("Specialization : " + d.specialty);
            }
        }

        public void DummyUsers()
        {
            DateTime dt;

            // doctors
            DateTime.TryParseExact("11/06/1959", "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
            Doctor d1 = new Doctor
            {
                UserId = "gregh",
                Name = "Gregory House",
                DOB = dt,
                Password = "gre",
                experience = 30,
                specialty = "Diagnostic Medicine"
            };
            users.Add(d1);

            DateTime.TryParseExact("18/11/1930", "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
            Doctor d2 = new Doctor
            {
                UserId = "stevs",
                Name = "Stephen Strange",
                DOB = dt,
                Password = "ste",
                experience = 40,
                specialty = "Neuro Surgery"
            };
            users.Add(d2);

            DateTime.TryParseExact("10/11/1969", "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
            Doctor d3 = new Doctor
            {
                UserId = "mereg",
                Name = "Meredith Grey",
                DOB = dt,
                Password = "mer",
                experience = 20,
                specialty = "General Surgery"
            };
            users.Add(d3);

            // patients
            DateTime.TryParseExact("01/01/1970", "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
            Patient p1 = new Patient
            {
                UserId = "johnd",
                Name = "John Doe",
                DOB = dt,
                Password = "joh",
                Remarks = "XXX Drug Allergy"
            };
            users.Add(p1);

            DateTime.TryParseExact("02/02/1980", "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
            Patient p2 = new Patient
            {
                UserId = "janed",
                Name = "Jane Doe",
                DOB = dt,
                Password = "jan",
                Remarks = "XXX Drug Allergy"
            };
            users.Add(p2);

            DateTime.TryParseExact("03/03/1990", "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
            Patient p3 = new Patient
            {
                UserId = "anonn",
                Name = "Anon Nymous",
                DOB = dt,
                Password = "ano",
                Remarks = "YYY Drug Allergy"
            };
            users.Add(p3);

        }
    }
}
