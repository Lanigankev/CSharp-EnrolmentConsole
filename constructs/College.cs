using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Kevin Lanigan 10186146

namespace constructs
{
    class College
    {
        static void Main(string[] args)
        {   
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            //instantiate classes that contain the methods used in the main class
            GeneralMethods generalMethod = new GeneralMethods();
            StudentMethods studentMethod = new StudentMethods();
            TeacherMethods teacherMethod = new TeacherMethods();

            // variable choice to determine main switch menu
            
            int choice = 1;
            
            // variable bool access to determine if login password has been correct or not

            bool access;

            //Header method to give appearance
            
            generalMethod.Header();

            
            //Option to choose to login or exit;
            if (generalMethod.YesNo("Login", "Exit"))
            {
                choice = 1;
            }
            else
            {
                choice = 0;
            }
             
            
            while(choice == 1)
            {
                //assigning bool value to the returned value of the login method

                access = generalMethod.Login();

                if (access == true)
                {
                    //Populate lists with example items for demonstrative purposes
                    studentMethod.PopulateStudentList();
                    
                    teacherMethod.PopulateTeacherList();
                    
                    Console.WriteLine("Lists have been populated with example entries");
                    
                    //Method for any key to continue
                    generalMethod.AnyKey();
                    

                    while (choice != 0)
                    {   
                        menu:
                        Console.WriteLine("********************Welcome to DBS Management Software********************\n\n");
                        Console.WriteLine("Please select choice\n\n*********************\n1: Student Options.\n2: Teacher Options\n3: Compare Teachers to Students\n0: Exit.");
                        
                        //user inputs choice to select menu option. try catch to ensure input is int format, if 0 is entered the program will complete and close
                        try
                        {
                            choice = int.Parse(Console.ReadLine());
                        }
                        catch 
                        {
                            Console.WriteLine("\nInvalid input\n");
                            generalMethod.AnyKey();
                            goto menu;

                        }

                        // switch statement to call different menu methods in different classes or to exit
                        switch (choice)
                        {
                            case 1: studentMethod.StudentMenu();
                                break;
                            case 2: teacherMethod.TeacherMenu();
                                break;
                            case 3: generalMethod.Comparison();
                                break;
                            case 0: break;
                            default: Console.WriteLine("\nInvalid input\n");
                                generalMethod.AnyKey();
                                goto menu;
                                

                        }
                    }
                }

                    //if login method returns false (invalid login) then this else statement will run

                else
                {
                    
                    Console.WriteLine("********************Welcome to DBS Management Software********************\n\n");
                    Console.WriteLine("Do you wish to try login again?\n");

                    if (generalMethod.YesNo("Yes", "No"))
                    {
                        choice = 1;
                    }
                    else 
                    {
                        choice = 0;
                    }
                    Console.Clear();
                }
            }
            
        }
    }
}
