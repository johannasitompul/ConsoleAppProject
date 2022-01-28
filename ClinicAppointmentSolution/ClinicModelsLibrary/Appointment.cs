

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicModelsLibrary
{
    public class Appointment : IComparable
    {
        public int ApptId { get; set; }
        public DateTime Timing { get; set; }
        public string DocId { get; set; }
        public string PatId { get; set; }
        public bool isComplete { get; set; }
        public double Cost { get; set; }
        public bool isPaid { get; set; }

        public Appointment()
        {
            isComplete = false;
            isPaid = false;
        }

        public int CompareTo(object obj)
        {
            Appointment a1, a2;
            a1 = this;
            a2 = (Appointment)obj;
            return a1.Timing.CompareTo(a2.Timing);
        }

        public void CreateAppt(string pat_id, string doc_id)
        {
            PatId = pat_id;

            DocId = doc_id;

            Console.Write("Enter date (DD/MM/YYYY) : ");
            DateTime dt;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, DateTimeStyles.None, out dt))
            {
                Console.Write("Invalid input. Please enter date in (DD/MM/YYYY) format : ");
            }

            while (dt < DateTime.Today)
            {
                Console.Write("Invalid input. Please enter a future date : ");
                DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
            }

            DateTime tm;
            Console.Write("Enter time in 24-hour format (HH:MM) : ");
            while (!DateTime.TryParseExact(Console.ReadLine(), "HH:mm", null, DateTimeStyles.None, out tm))
            {
                Console.WriteLine("Invalid input. Please re-enter : ");
            }
            DateTime dttm = dt.Date + tm.TimeOfDay;

            Timing = dttm;
        }

        public override string ToString()
        {
            return "Appointment ID : " + ApptId
                + "\nDate : " + Timing.ToString("dd/MM/yyyy")
                + "\nTime : " + Timing.ToString("HH:mm")
                + "\nDoctor's ID : " + DocId
                + "\nPatient's ID : " + PatId
                + "\nCompleted : " + isComplete
                + "\nPayable Amount : " + Cost
                + "\nPaid : " + isPaid;
        }
    }
}
