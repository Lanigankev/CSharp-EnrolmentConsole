using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//Kevin Lanigan 10186146

namespace constructs
{
    class TeacherMethods
    {
        public static List<Teacher> teacherList = new List<Teacher>();

        GeneralMethods generalMethod = new GeneralMethods();


        //teacher menu method called in main class which uses switch statement to call various methods in this class

        public void TeacherMenu()
        {
            int choice;
            generalMethod.Header();
            
            Console.WriteLine("Please select choice\n\n*********************\n1: Add Teacher To List.\n2: Display Working List Of Teachers.\n3: Search List By Teacher First Name.\n4: Save Working Teacher List To .csv File\n5: Import List From Existing .csv File\n0: Exit.");

            choice = int.Parse(Console.ReadLine());
            switch(choice)
            {
                case 1: TeacherInput();
                    break;
                case 2: DisplayListReturn();
                    break;
                case 3: SearchFname();
                    break;
                case 4: SaveToCsv();
                    break;
                case 5: ReadFromText();
                    break;
                case 6: Console.WriteLine("hello");
                    foreach(Teacher tit in GetTeacherList())
                    { Console.WriteLine(tit.ToString()); }
                    generalMethod.AnyKey();
                    break;

                case 0: break;

            }
            Console.Clear();
        }

        //Teacher Methods----------------------------------------------------------------------------------------------------------
        
        //method to put variables into teacher class constructor
        
        private void AddTeacher(string fname, string lname, string phone, string email, double salary, string subject)
        {
            Teacher tea = new Teacher(fname, lname, phone, email, salary, subject);
            teacherList.Add(tea);

        }

        //method to input variables to put into AddTeacher method

        private void TeacherInput()
        {
            generalMethod.Header();

            string fname, lname, phone, email, subject, salarystring;
            double salary;

            //taking info to put in constructor for employee class

            fname = generalMethod.EmptyEntryPreventer("Please input employee's first name").ToUpper();


            lname = generalMethod.EmptyEntryPreventer("Please input employee's last name").ToUpper();

            phone = generalMethod.EmptyEntryPreventer("Please input employee's phone number");

            email = generalMethod.EmptyEntryPreventer("Please input employee's email address");

            if (generalMethod.IsValidEmail(email))
            {
            salaryredo:
                salarystring = generalMethod.EmptyEntryPreventer("What is the employee's salary?");


                try
                {
                    salary = double.Parse(salarystring);
                }
                catch
                {
                    Console.WriteLine("Invalid input: Input should be in numeric form\n");
                    goto salaryredo;
                }

                Console.WriteLine("What subject does the employee teach?\n1: Maths.\n2: Science.\n3: English.\n");
                int choice;
            subjredo:
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please reenter the number of the subject taught.");
                    goto subjredo;
                }

                switch (choice)
                {
                    case 1: subject = "MATHS";
                        break;
                    case 2: subject = "SCIENCE";
                        break;
                    case 3: subject = "ENGLISH";
                        break;
                    default: Console.WriteLine("Invalid input. Please reenter the number of the subject taught.");
                        goto subjredo;
                }




                //using below method to construct student class

                AddTeacher(fname, lname, phone, email, salary, subject);

                Console.WriteLine("\nSaved to list.\n");

                generalMethod.AnyKey();

                TeacherMenu();
            }
        }

        //Method to display list of Teachers

        private void DisplayList()
        {
            Console.Clear();

            Console.WriteLine("\tNAME\t\t PHONE\t\tEMAIL\t\tSALARY \t SUBJECT TAUGHT");

            Console.WriteLine("******************************************************************************");

            foreach (Teacher t in teacherList)
            {
                Console.WriteLine(t.Display());
            }

            generalMethod.AnyKey(); 
        }

        private void DisplayListReturn()
        {
            DisplayList();
            Console.Clear();
            TeacherMenu();
        }
        // search teachers by first name

        private void SearchFname()
        {
            generalMethod.Header();

            bool foundmatch = false;

            string fname;

            Console.WriteLine("Search Teacher Name");

            fname = Console.ReadLine().ToUpper();

            Console.WriteLine("Matches:");

            Console.WriteLine("*******************************************\n");

            foreach (Teacher t in teacherList)
            {
                if (t.Fname == fname)
                {
                    Console.WriteLine(t.ToString());
                    foundmatch = true;

                    Console.WriteLine("\n\n\n\nDo you wish to delete this teacher from the list?");

                    if (generalMethod.YesNo("Yes", "No"))
                    {
                        teacherList.Remove(t);
                        Console.WriteLine("\n\nTeacher Deleted");
                        break;
                    }
                }

            }

            if (!foundmatch)
            {
                Console.WriteLine("No Teacher matching that first name");
            }

            generalMethod.AnyKey();

            TeacherMenu();

        }

        
        

        //method to present options when saving to .csv

        private void SaveToCsv()
        {
            generalMethod.Header();

            Console.WriteLine("To save file");

            string filePath = generalMethod.FilePath();

            if (!File.Exists(filePath))
            {
                Save(filePath);
            }
            else
            {
                Console.WriteLine("A file with this name exists.\nDo you wish to overwrite or append this file with the current working list?");

                if (generalMethod.YesNo("Overwrite","Append"))
                {
                    Overwrite(filePath);
                }
                else
                {
                    Append(filePath);
                }
            }

            TeacherMenu();
            
        }

        //method to save file to .csv
        private void Save(string filePath)
        {

            StreamWriter sw = new StreamWriter(filePath, true);


            foreach (Teacher t in teacherList)
            {
                sw.WriteLine(t.ToString());

            }


            Console.WriteLine("The current working list has been saved to the file");

            sw.Close();

            generalMethod.AnyKey();


        }

        //method to append to existing .csv files
        private void Append(string filePath)
        {
            
            StreamWriter sw = new StreamWriter(filePath, true);


            foreach (Teacher t in teacherList)
                {
                    sw.WriteLine(t.ToString());

                }


            Console.WriteLine("The current working list has been appended to the file");

            sw.Close();

            generalMethod.AnyKey();
         
        }



        //method to overwrite existing csv file

        private void Overwrite(string filePath)
        {

            StreamWriter sw = new StreamWriter(filePath);

                
                    foreach (Teacher t in teacherList)
                    {
                        sw.WriteLine(t.ToString());

                    }

                    Console.WriteLine("The current working list has overwritten the file");

                    generalMethod.AnyKey();

            sw.Close();


        }


        //Method for reading out from outside csv file

        private void ReadFromText()
        {
            generalMethod.Header();
            
            Console.WriteLine("To import a file");

            string filePath = generalMethod.FilePath();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("No file matches that name.");
                
            }
            else
            {
                StreamReader sr = new StreamReader(File.OpenRead(filePath));
                
                while (!sr.EndOfStream)
                {
                    //streamreader reads line
                    var line = sr.ReadLine();

                    //line broken up into items using "," and then inserted to array values
                    var values = line.Split(',');

                    string fname, lname, phone, email, subject;
                    double salary;

                    //array values then assigned to variables

                    fname = values[0];
                    lname = values[1];
                    phone = values[2];
                    email = values[3];
                    salary = double.Parse(values[4]);
                    subject = values[5];

                    //variables then input to addteacher() method
                    AddTeacher(fname, lname, phone, email, salary, subject);

                }
                sr.Close();
                DisplayList();
            }

            TeacherMenu();
        }


        //method to allow access to teacherlist in other classes
        
        public List<Teacher> GetTeacherList()
        {
            return teacherList;
        }
        
        //method to populate teacherlist with example items

        public void PopulateTeacherList()
        {
          //  Teacher t1 = new Teacher("BOB", "GLEESON", "0836422331", "robert@robb.com", 50000, "SCIENCE");
            Teacher t2 = new Teacher("EMMA", "WATSON", "0862342123", "emma@hpotter.com", 24000, "MATHS");
            Teacher t3 = new Teacher("YOLANDA", "BECOOL", "0851556931", "yolanda@email.com", 30000, "ENGLISH");
          //  Teacher t4 = new Teacher("PETER", "STRINGFELLOW", "0851556931", "yolanda@email.com", 30000, "ENGLISH");
           // Teacher t5 = new Teacher("JOHN", "CONNOR", "087324234", "johnconnor@gmail.com", 50000, "SCIENCE");
            //teacherList.Add(t1);
            teacherList.Add(t2);
            teacherList.Add(t3);
           // teacherList.Add(t4);
            //teacherList.Add(t5);

        }
    }
}