using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//Kevin Lanigan 10186146

namespace constructs
{
    class GeneralMethods
    {

        //Method to login to software package
        public bool Login()
        {
            Header();


            Hashcode hash = new Hashcode();
            
        //reguser = registered user (only user allowed)
            
            string pass, username, reguser = "admin";

       //encrypted password, the password is "password"
           
            string password = "91170972282011856363613037111082485127126230143216";

            Console.WriteLine("Please enter your username:");
           
            username = Console.ReadLine();

        
            if (username == reguser)
            {
                Console.WriteLine("Please enter your password:");
                pass = hash.PassHash(Console.ReadLine());
                
         //if correct password run if statement and return true
                
                if (pass == password)
                {
                    Console.WriteLine("\nWelcome {0}", username);
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    return true;
                }

        //if incorrect password run else statement return false
                else
                {
                    Console.WriteLine("\nInvalid password");
                    System.Threading.Thread.Sleep(1250);
                    Console.Clear();
                    return false;
                }

            }
            else
            {
                Console.WriteLine("\n\nInvalid user");
                System.Threading.Thread.Sleep(1250);
                Console.Clear();
                return false;
            }

        }

        
        //method for boolean decisions, which presents the user with 2 options, the user then selects the option. selecting choice1 returns true selecting choice2 returns false

        public bool YesNo(string choice1, string choice2)
        {

            string decision;

            Console.WriteLine("Please type your selection:");

            Console.WriteLine("\n1:{0}\n2:{1}", choice1, choice2);
        redo:
            decision = Console.ReadLine().ToUpper();
            if (decision == "1")
            {
                return true;
            }
            else if (decision == "2")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 1 ({0}) or 2 ({1}).", choice1, choice2);
                goto redo;
            }
        }


        // method for anykey to continue
        public void AnyKey()
        {
            Console.WriteLine("\n\n\nPress any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }
        
        
        //method for presenting header in console
        public void Header()
        {
            Console.Clear();
            Console.WriteLine("********************Welcome to DBS Management Software********************\n\n");

        }

        //method for inputting filepaths for streamreader/streamwriter etc
        public string FilePath()
        {
            Console.WriteLine("Please input the file name");
            string filename = Console.ReadLine();

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + filename + ".csv";

            return filePath;
        }

        //method to compare the two lists 
        //method to compare the two lists 
        public void Comparison()
        {
            Header();

            Console.WriteLine("The system will now compare the two lists and determine if there are any matching names.\n\n");

            // instantiate the two classes to this method can call each list using the GetStudentList() and GetTeacherList() methods in each class

            TeacherMethods tea = new TeacherMethods();
            StudentMethods stu = new StudentMethods();
            List<int> indexes = new List<int>();
            //assign the returned array from the Compare() method to indexes

            indexes = Compare(stu.GetStudentList(), tea.GetTeacherList());

            //list indexes then holds the index numbers for the items in the student list and the items in the teacher which share the same names
            //see Compare() comments for more info



            if (indexes.Count != 0)
            {
                int p = 0;
                int q = 1;
                int r = 0;

                int arraySize = (indexes.Count() / 2);

                int[] studentMatchIndex = new int[arraySize];
                int[] teacherMatchIndex = new int[arraySize];

                for (int i = 0; i < arraySize; i++)
                {
                    studentMatchIndex[p] = indexes[r];
                    teacherMatchIndex[p] = indexes[q];

                    Console.WriteLine(stu.GetStudentList()[studentMatchIndex[p]].Display());
                    Console.WriteLine(tea.GetTeacherList()[teacherMatchIndex[p]].Display());

                    //boolean decision so that user can decide if they want to ensure that the teacher list item and the student list item share the same phone and email by overwriting

                    Console.WriteLine("\n\nDo you wish ensure matching phone and emails for these two instances?");
                    if (YesNo("Yes", "No"))
                    {
                        Console.WriteLine("\nDo you wish to keep the Teacher instance details or the Student instance details?");
                        if (YesNo("Teacher", "Student"))
                        {
                            //Overwriting the student phone and email with the teacher phone and email

                            stu.GetStudentList()[p].Email = tea.GetTeacherList()[p].Email;
                            stu.GetStudentList()[p].Phone = tea.GetTeacherList()[p].Phone;

                            Console.WriteLine("\n\n The Teacher details have replaced the Student details");
                        }
                        else
                        {
                            //overwriting the teacher phone and email with the student phone and email

                            tea.GetTeacherList()[p].Email = stu.GetStudentList()[p].Email;
                            tea.GetTeacherList()[p].Phone = stu.GetStudentList()[p].Phone;

                            Console.WriteLine("\n\n The Student details have replaced the Teacher details");
                        }

                    }

                    else
                    {
                        Console.WriteLine("\nNo details have been overwritten");
                    }

                    //p increases by 1, p represents the instance of matching names, p = 0 is first instance of mathcing names, p = 1 is second instance of matching names etc
                    p++;
                    
                    //r to increase by 2 to skip to the next index value in list indexes which contains a student index
                    r = r + 2;
                   //q increases by 2 to skip to he next index value in list indexes which contains a teacher index 
                    q = q + 2;

                }

            }
            else 
            {
                Console.WriteLine("\nNo Matching names have been found\n");
            }

            AnyKey();
        }

        // Compare two lists array which returns an int list which contains the indexes of items that share names from each list
        public List<int> Compare(List<Student> stu, List<Teacher> tea)
        {
            //array matches contains 2 items

            List<int> matches = new List<int>();


            //y is teacher list item index

            for (int y = 0; y < tea.Count; y++)
            {
                //i is the student list item index

                for (int i = 0; i < stu.Count; i++)
                {
                    //if student list item has same name as teacher list item

                    if (stu[i] == tea[y])
                    {
                        //store index values in list matches, then return matches

                        matches.Add(i);
                        matches.Add(y);

                    }
                }
            }

            //if no matches are found returns an array == null
            return matches;



        }
        // Method to test email input is valid email format
        public bool IsValidEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }

        }

        //method to prevent empty entries
        public string EmptyEntryPreventer(string question)
        {
            string entry;
            redoinput:
            Console.WriteLine(question);
            entry = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(entry))
            {
                Console.WriteLine("\nInvalid entry\n");
                goto redoinput;
            }
            else
            {
                return entry;
            }

        }

    }
}
