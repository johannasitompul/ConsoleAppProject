using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicModelsLibrary;

namespace ClinicAppointmentApp
{
    class ManageAppointment
    {
        List<Appointment> appts;

        public Appointment this[int index]
        {
            get { return appts[index]; }
            set { appts[index] = value; }
        }

        // constructor
        public ManageAppointment()
        {
            appts = new List<Appointment>();
        }

        // get doctor id from user and check if doctor id exist
        public string GetCheckDoctorId(ManageUser mu)
        {
            Console.Write("\nEnter doctor's ID : ");
            User u = mu.GetUserById(Console.ReadLine());
            while (u == null || u.Type != "Doctor")
            {
                Console.Write("Invalid doctor's ID. Please check and re-enter : ");
                u = mu.GetUserById(Console.ReadLine());
            }

            return u.UserId;
        }

        // create a new appt and add into list
        public void AddAppt(ManageUser mu, string pat_id)
        {
            mu.DoctorList();

            Appointment appt = new Appointment();
            Appointment ap;
            appt.ApptId = GenerateId();

            string doc_id = GetCheckDoctorId(mu);            
            appt.CreateAppt(pat_id, doc_id);
            
            // check if appointment overlaps
            ap = appts.Find(a => a.DocId == appt.DocId && a.Timing == appt.Timing);
            if (ap == null)
            {
                appts.Add(appt);
                Console.WriteLine("Appointment succesfully made.");
                PrintAppt(mu, appt, "Patient");
                return;
            }
            Console.WriteLine("Sorry, slot unavailable. Please try another doctor or timing.");
        }

        // auto generate appt_id
        private int GenerateId()
        {
            if (appts.Count == 0)
                return 101;
            return appts.Count + 101;
        }

        // view appointments
        public void ViewFutureAppts(ManageUser mu, User user)
        {
            var myAppts = appts.Where(a => a.Timing > DateTime.Now && (a.DocId == user.UserId || a.PatId == user.UserId));
            if (myAppts.Any())
            {
                foreach (var item in myAppts)
                {
                    PrintAppt(mu, item, user.Type);
                }
            }
            else
                Console.WriteLine("\nYou have no future appointments.\n");
            
        }

        public void ViewPastAppts(ManageUser mu, User user)
        {
            var myAppts = appts.Where(a => a.Timing < DateTime.Now && (a.DocId == user.UserId || a.PatId == user.UserId));
            if (myAppts.Any())
            {
                foreach (var item in myAppts)
                {
                    PrintAppt(mu, item, user.Type, "past");
                }
            }
            else
                Console.WriteLine("\nYou have no past appointments.\n");
        }


        public void PrintAppt(ManageUser mu, Appointment appt, string user_type, string time="future")
        {
            Console.WriteLine("--");
            Console.WriteLine("Appointment ID : " + appt.ApptId);
            Console.WriteLine("Date : " + appt.Timing.ToString("dd/MM/yyyy"));
            Console.WriteLine("Time : " + appt.Timing.ToString("HH:mm"));

            if(user_type == "Doctor")
            {
                Patient p = (Patient)mu.GetUserById(appt.PatId);
                Console.WriteLine("Patient's Name : " + p.Name);
                Console.WriteLine("Remarks : " + p.Remarks);
            }

            else if (user_type=="Patient")
                Console.WriteLine("Doctor's Name : " + mu.GetUserById(appt.DocId).Name);

            if (time == "past")
            {
                Console.WriteLine("Completed : " + appt.isComplete);
                Console.WriteLine("Payable amount : " + appt.Cost);
                Console.WriteLine("Paid : " + appt.isPaid);
            }
        }

        int GetIdFromUser()
        {
            Console.Write("Enter appointment ID : ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid input. Please re-enter appointment ID : ");
            }
            return id;
        }

        // retrieve appt using appt_id
        public Appointment GetApptById(int id)
        {
            Appointment appt = appts.Find(a => a.ApptId == id);
            return appt;
        }

        // only doctor can raise payment - past appointment only
        public void RaisePayment(ManageUser mu)
        {
            int id = GetIdFromUser();
            Appointment appt = GetApptById(id);

            var apptToPay = appts.SingleOrDefault(a => a.ApptId == id && a.Timing < DateTime.Now && a.Cost == 0);
            if (apptToPay != null)
            {
                double cost;
                Console.Write("Enter cost of visit : ");
                while (!double.TryParse(Console.ReadLine(), out cost))
                {
                    Console.Write("Invalid input. Please re-enter cost : ");
                }

                appt.Cost = cost;
                appt.isComplete = true;
                Console.WriteLine("Payment raised.");
                PrintAppt(mu, appt, "Doctor", "past"); ;
            }
            else
                Console.WriteLine("Invalid ID. Unable to raise payment. Please check your Appointment ID.");            
        }

        // only patient can delete - future appointments only
        public void DeleteAppt(ManageUser mu)
        {
            int id = GetIdFromUser();
            Appointment appt = GetApptById(id);

            var itemToRemove = appts.SingleOrDefault(a => a.ApptId == id && a.Timing > DateTime.Now);
            if (itemToRemove != null)
            {
                Console.WriteLine("Delete the following appointment?");
                PrintAppt(mu, appt, "Patient", "future");
                Console.Write("Enter [Y] to confirm delete : ");
                string check = Console.ReadLine();
                if (check == "Y" || check == "y")
                {
                    appts.Remove(itemToRemove);
                    Console.WriteLine("Appointment deleted.");
                }
                else
                    Console.WriteLine("Deletion canceled.");
            }
            else
                Console.WriteLine("Appointment not found or not available for deletion. Please check your Appointment ID.");
        }

        // payment for past appointment
        public void PayVisit(ManageUser mu)
        {
            int id = GetIdFromUser();
            Appointment appt = GetApptById(id);

            var apptToPay = appts.SingleOrDefault(a => a.ApptId == id && a.Timing<DateTime.Now && a.Cost != 0);
            if (apptToPay != null)
            {
                Console.WriteLine("Pay for the following appointment?");
                PrintAppt(mu, appt, "Patient", "past");
                Console.Write("Enter [Y] to confirm payment : ");
                string check = Console.ReadLine();
                if (check == "Y" || check == "y")
                {
                    appt.isPaid = true;
                    Console.WriteLine("Payment Successful.");
                    PrintAppt(mu, appt, "Patient", "past");
                }
                else
                    Console.WriteLine("Payment canceled.");
            }
            else
                Console.WriteLine("Appointment not found or not available for payment. Please check your Appointment ID.");
        }

        public void DisplayAllAppts()
        {
            foreach (var item in appts)
            {
                Console.WriteLine(item);
            }
        }

        public void DummyAppts()
        {
            Appointment a1 = new Appointment
            {
                ApptId = 101,
                Timing = DateTime.Parse("12/12/2021 08:30:00"),
                DocId = "mereg",
                PatId = "janed",
                isComplete = true,
                Cost = 200,
                isPaid = true
            };
            appts.Add(a1);

            Appointment a2 = new Appointment
            {
                ApptId = 102,
                Timing = DateTime.Parse("01/01/2022 08:30:00"),
                DocId = "gregh",
                PatId = "johnd",
                isComplete = true,
                Cost = 0,
                isPaid = false
            };
            appts.Add(a2);

            Appointment a3 = new Appointment
            {
                ApptId = 103,
                Timing = DateTime.Parse("02/02/2022 08:30:00"),
                DocId = "gregh",
                PatId = "johnd",
                isComplete = false,
                Cost = 0,
                isPaid = false
            };
            appts.Add(a3);
        }
    }
}
