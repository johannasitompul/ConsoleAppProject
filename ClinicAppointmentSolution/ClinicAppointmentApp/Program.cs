using ClinicModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClinicAppointmentApp
{
    class Program
    {
        ManageUser mu = new ManageUser();
        ManageAppointment ma = new ManageAppointment();

        static void MainMenu()
        {
            Console.WriteLine("\nWelcome to the clinic!");
            Console.WriteLine("--");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register");
            Console.WriteLine("0: Exit");
        }

        static void PatMenu()
        {
            Console.WriteLine("\nHello patient!");
            Console.WriteLine("--");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1: View my upcoming appointments");
            Console.WriteLine("2: View my past appointments");
            Console.WriteLine("3: Schedule a new appointment");
            Console.WriteLine("4: View my profile");
            Console.WriteLine("0: Back");
        }

        static void DocMenu()
        {
            Console.WriteLine("\nHello Doctor!");
            Console.WriteLine("--");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1: View my upcoming appointments");
            Console.WriteLine("2: View my past appointments");
            Console.WriteLine("3: View my profile");
            Console.WriteLine("0: Back");
        }

        static void SubMenu(string s)
        {
            Console.WriteLine("--");
            if (s == "raise")
                Console.WriteLine("1: Raise payment");
            else if (s == "pay")
                Console.WriteLine("1: Pay for a visit");
            else if (s == "delete")
                Console.WriteLine("1: Delete an appointment");
            Console.WriteLine("0: Back");
        }

        void MainMenuLoop()
        {
            int choice;
            User u = null;

            // login
            do
            {
                MainMenu();
                Console.Write("Your choice : ");
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.Write("Invalid input. Please enter the number of your choice : ");
                }
                switch (choice)
                {
                    case 1:
                        u = mu.Login();
                        if (u != null)
                        {
                            // user is doctor
                            if(u.Type == "Doctor")
                            {
                                int doc_choice;
                                do
                                {
                                    DocMenu();
                                    Console.Write("Your choice : ");
                                    while (!int.TryParse(Console.ReadLine(), out doc_choice))
                                    {
                                        Console.Write("Invalid input. Please enter the number of your choice : ");
                                    }
                                    switch (doc_choice)
                                    {
                                        case 1:
                                            ma.ViewFutureAppts(mu, u);
                                            break;
                                        case 2:
                                            ma.ViewPastAppts(mu, u);
                                            int doc_choice2;
                                            do
                                            {
                                                SubMenu("raise");
                                                Console.Write("Your choice : ");
                                                while (!int.TryParse(Console.ReadLine(), out doc_choice2))
                                                {
                                                    Console.Write("Invalid input. Please enter the number of your choice : ");
                                                }
                                                switch(doc_choice2)
                                                {
                                                    case 1:
                                                        ma.RaisePayment(mu);
                                                        break;
                                                    case 0:
                                                        Console.WriteLine("Going back to dashboard...");
                                                        break;
                                                    default:
                                                        Console.Write("Invalid input. Enter the number of your choice : ");
                                                        break;
                                                }
                                            } while (doc_choice2 != 0);
                                            break;
                                        case 3:
                                            Console.WriteLine(u);
                                            break;
                                        case 0:
                                            Console.WriteLine("Going back to main menu...");
                                            break;
                                        default:
                                            Console.Write("Invalid input. Enter the number of your choice : ");
                                            break;
                                    }
                                } while (doc_choice != 0);
                                
                                                              
                            // user is patient
                            }
                            if(u.Type == "Patient")
                            {
                                int pat_choice;

                                do
                                {
                                    PatMenu();
                                    Console.Write("Your choice : ");
                                    while (!int.TryParse(Console.ReadLine(), out pat_choice))
                                    {
                                        Console.Write("Invalid input. Please enter the number of your choice : ");
                                    }
                                    switch(pat_choice)
                                    {
                                        case 1:
                                            ma.ViewFutureAppts(mu, u);
                                            int pat_choice1;
                                            do
                                            {
                                                SubMenu("delete");
                                                Console.Write("Your choice : ");
                                                while (!int.TryParse(Console.ReadLine(), out pat_choice1))
                                                {
                                                    Console.Write("Invalid input. Please enter the number of your choice : ");
                                                }
                                                switch (pat_choice1)
                                                {
                                                    case 1:
                                                        ma.DeleteAppt(mu);
                                                        break;
                                                    case 0:
                                                        Console.WriteLine("Going back to dashboard...");
                                                        break;
                                                    default:
                                                        Console.Write("Invalid input. Enter the number of your choice : ");
                                                        break;
                                                }
                                            } while (pat_choice1 != 0);
                                            break;
                                        case 2:
                                            ma.ViewPastAppts(mu, u);
                                            int pat_choice2;
                                            do
                                            {
                                                SubMenu("pay");
                                                Console.Write("Your choice : ");
                                                while (!int.TryParse(Console.ReadLine(), out pat_choice2))
                                                {
                                                    Console.Write("Invalid input. Please enter the number of your choice : ");
                                                }
                                                switch (pat_choice2)
                                                {
                                                    case 1:
                                                        ma.PayVisit(mu);
                                                        break;
                                                    case 0:
                                                        Console.WriteLine("Going back to dashboard...");
                                                        break;
                                                    default:
                                                        Console.Write("Invalid input. Enter the number of your choice : ");
                                                        break;
                                                }
                                            } while (pat_choice2 != 0);
                                            break;
                                        case 3:
                                            ma.AddAppt(mu, u.UserId);
                                            break;
                                        case 4:
                                            Console.WriteLine(u);
                                            break;
                                        case 0:
                                            Console.WriteLine("Going back to main menu...");
                                            break;
                                        default:
                                            Console.Write("Invalid input. Enter the number of your choice : ");
                                            break;
                                    }
                                } while (pat_choice != 0);
                            }
                        }
                        break;
                    case 2:
                        mu.RegisterUser();
                        break;
                    case 0:
                        Console.WriteLine("Exiting application. Goodbye...");
                        break;
                    default:
                        Console.Write("Invalid input. Enter the number of your choice : ");
                        break;
                }

            } while (choice != 0);



        }

        public void DummyInputs()
        {
            mu.DummyUsers();
            ma.DummyAppts();
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.DummyInputs();

            p.MainMenuLoop();

            Console.ReadKey();
        }

    }
}
