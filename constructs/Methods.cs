using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace constructs
{
    class Methods
    {
        //Lists
        List<Student> stulist = new List<Student>();
        GeneralMethods gm = new GeneralMethods();

        //Student Methods----------------------------------------------------------------------------------------------------------
        
        //method to put variables into student class constructor

        public void StudentMenu()
        {
            int select;
            Console.Clear();
            Console.WriteLine("********************Welcome to DBS Management Software********************\n\n");
            Console.WriteLine("Please select choice\n\n*********************\n1: Add student.\n2: Display Student.\n3: Search By Student ID.\n4: Save list to text file\n5: Read saved text file\n6:Overwrite Existing File.\n0: Return to Main Menu.");
            select = int.Parse(Console.ReadLine());
            
            
            {
               
                switch (select)
                {
                    case 1: StudentInput();
                        break;
                    case 2: DisplayListReturn();
                        break;
                    case 3: SearchId();
                        break;
                    case 4: SaveToCsv();
                        break;
                    case 5: ReadFromText();
                        break;
                    case 0: break;

                }
            }
            Console.Clear();
            
        }


        
        private void AddStudent(string fname, string lname, string phone, string email, int id, string status)
        {
            Student stu = new Student(fname, lname, phone, email, id, status);
            stulist.Add(stu);

        }

        
        
        //method to input variables which will go into student class constructor
        private int IdGenerator()
        { 
            Random rd = new Random();
            
            bool flag = true;
            int id;

            do
            
            {
                //in reality a larger range of values would be used but for ease of the search id function, a 2 digit number is employed
            id = rd.Next(10, 99);
            
            foreach (Student stu in stulist)
             {
                if (stu.Id == id)
                {
                    flag = false;    
                }
                else
                {
                    flag = true;
                }
             }

            }while (!flag);

            return id;
        }

        
        private void StudentInput()
        {
            int id = IdGenerator();

                Console.Clear();

                Console.WriteLine("********************Welcome to DBS Management Software********************\n\n");

                string fname, lname, phone, email, status = "";

                //taking info to put in constructor for student class

                Console.WriteLine("Please input student's first name");
                fname = Console.ReadLine().ToUpper();

                Console.WriteLine("Please input student's last name");
                lname = Console.ReadLine().ToUpper();

                Console.WriteLine("Please input student's phone number");
                phone = Console.ReadLine();

                Console.WriteLine("Please input student's email address");
                email = Console.ReadLine().ToUpper();

                Console.WriteLine("Is the student enrolling to a undergraduate or postgraduate course?");
                if (gm.YesNo("Undergraduate", "Postgraduate"))
                {
                    status = "UNDER";
                }
                else
                {
                    status = "POST";
                }

                Console.WriteLine("Student ID generated is {0}", id);


                //using below method to construct student class
                AddStudent(fname, lname, phone, email, id, status);
                gm.AnyKey();
                StudentMenu();
            }
            
     

        //method to display student in list

        private void DisplayList()
        {
            Console.Clear();
            
            Console.WriteLine("\tNAME\t\t PHONE\t\tEMAIL\t\t\tID\tSTATUS");
            
            Console.WriteLine("******************************************************************************");
            
            foreach (Student s in stulist)
            
            {
                Console.WriteLine(s.ToString());
            }

                gm.AnyKey(); 
        }

        private void DisplayListReturn()
        {
            DisplayList();
            Console.Clear();
            StudentMenu();
        }
        

        //Display match method
        
        private void SearchId()
        {
            gm.Header();

            bool foundmatch = false;
           
            int id;

            Console.WriteLine("Search Student ID");

            //TRY PARSE
            id = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Matches:");
            
            Console.WriteLine("*******************************************\n");

            foreach (Student s in stulist)
            {
                if (s.Id == id)
                {
                    Console.WriteLine(s.ToString());
                    foundmatch = true;

                    Console.WriteLine("\n\n\n\nDo you wish to delete this student from the list?");

                    if (gm.YesNo("Yes", "No"))
                    {
                        stulist.Remove(s);
                        Console.WriteLine("\n\nTeacher Deleted");
                        break;
                    }
                    

                }

            }
            
            if (!foundmatch)
            
            {
                Console.WriteLine("No Student matching that ID");
            }

            gm.AnyKey();

            StudentMenu();


        }

        //method for saving to csv file --- Not used
        //method for saving list to text for
        private void SaveToCsv()
        {
            gm.Header();

            Console.WriteLine("To save file");
            string filePath = gm.FilePath();

            if (!File.Exists(filePath))
            {
                Save(filePath);
            }
            else
            {
                Console.WriteLine("A file with this name exists.\nDo you wish to overwrite or append this file with the current working list?");

                if (gm.YesNo("Overwrite", "Append"))
                {
                    Overwrite(filePath);
                }
                else
                {
                    Append(filePath);
                }
            }

            StudentMenu();

        }


        //method to overwrite existing csv file

        private void Save(string filePath)
        {

            StreamWriter sw = new StreamWriter(filePath, true);


            foreach (Student s in stulist)
            {
                sw.WriteLine(s.ToString());

            }


            Console.WriteLine("The current working list has been saved to the file");

            sw.Close();

            gm.AnyKey();


        }

        //append method
        private void Append(string filePath)
        {

            StreamWriter sw = new StreamWriter(filePath, true);


            foreach (Student s in stulist)
            {
                sw.WriteLine(s.ToString());

            }


            Console.WriteLine("The current working list has been appended to the file");

            sw.Close();

            gm.AnyKey();

        }



        //method to overwrite existing csv file

        private void Overwrite(string filePath)
        {

            StreamWriter sw = new StreamWriter(filePath);


            foreach (Student s in stulist)
            {
                sw.WriteLine(s.ToString());

            }

            Console.WriteLine("The current working list has overwritten the file");

            gm.AnyKey();

            sw.Close();


        }


         //Method for reading out from outside csv file
         private void ReadFromText()
         {
             gm.Header();

             Console.WriteLine("To import a file");

             string filePath = gm.FilePath();

             if (!File.Exists(filePath))
             {
                 Console.WriteLine("No file matches that name.");

             }
             else
             {
                 StreamReader sr = new StreamReader(File.OpenRead(filePath));
                 while (!sr.EndOfStream)
                 {

                     var line = sr.ReadLine();
                     var values = line.Split(',');

                     string fname, lname, phone, email, status;
                     int id;


                     fname = values[0];
                     lname = values[1];
                     phone = values[2];
                     email = values[3];
                     id = int.Parse(values[4]);
                     status = values[5];
                     AddStudent(fname, lname, phone, email, id, status);

                 }
                 sr.Close();
                 
                
                 DisplayList();
             }

             StudentMenu();
         }


        
    }
}
