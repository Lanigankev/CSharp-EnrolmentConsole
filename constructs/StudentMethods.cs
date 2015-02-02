using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//Kevin Lanigan 10186146

namespace constructs
{
    class StudentMethods
    {
        //Lists
       public static List<Student> studentList = new List<Student>();
        GeneralMethods generalMethod = new GeneralMethods();

        //Student Methods----------------------------------------------------------------------------------------------------------
        
        //Student menu method, a public method called in teh main class, this switch statements calls methods in this class

        public void StudentMenu()
        {
            int select=1;
        start:
            generalMethod.Header();
            Console.WriteLine("Please select choice\n\n*********************\n1: Add student.\n2: Display Student.\n3: Search By Student ID.\n4: Save list to .csv file.\n5: Import a .csv file to list.\n0: Return to Main Menu.");
            
            try
            {
                select = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid input");
                generalMethod.AnyKey();
                goto start;
            }
            
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


        //add student to student list (studentList) using constructor
        private void AddStudent(string fname, string lname, string phone, string email, int id, string status)
        {
            Student stu = new Student(fname, lname, phone, email, id, status);
            studentList.Add(stu);

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
            
                  // checks id generated against existing ids, if they exist, flag is false and process repeats

            foreach (Student stu in studentList)
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

        //method to input variables before being adds to the student list using AddStudent()

        private void StudentInput()
        {
            int id = IdGenerator();

                Console.Clear();

                Console.WriteLine("********************Welcome to DBS Management Software********************\n\n");

                string fname, lname, phone, email, status = "";

                //taking info to put in constructor for student class

                fname = generalMethod.EmptyEntryPreventer("Please input student's first name").ToUpper();

                lname = generalMethod.EmptyEntryPreventer("Please input student's last name").ToUpper();

                phone =generalMethod.EmptyEntryPreventer("Please input student's phone number");

                emailreenter:
                Console.WriteLine("Please input student's email address");
                email = Console.ReadLine();
            
                if (generalMethod.IsValidEmail(email))
                {
                    Console.WriteLine("Is the student enrolling to a undergraduate or postgraduate course?");

                    if (generalMethod.YesNo("Undergraduate", "Postgraduate"))
                    {
                        status = "UNDER";
                    }
                    else
                    {
                        status = "POST";
                    }

                    Console.WriteLine("Student ID generated is {0}", id);
                }
                else
                {
                    Console.WriteLine("\nInvalid email entered\n");
                    goto emailreenter;
                }

                //using below method to construct student class
                AddStudent(fname, lname, phone, email, id, status);

                Console.WriteLine("Student added to list.");
                
                generalMethod.AnyKey();
                
             // return to student menu upon completion
                StudentMenu();
            }
            
     

        //method to display student in list

        private void DisplayList()
        {
            Console.Clear();
            
            Console.WriteLine("\tNAME\t\t PHONE\t\tEMAIL\t\t  ID\t    STATUS");
            
            Console.WriteLine("******************************************************************************");
            

            //Displays each student in the list in the Student class Display() format

            foreach (Student s in studentList)
            
            {
                Console.WriteLine(s.Display());
            }

                generalMethod.AnyKey(); 
        }

        //method to Display students in a list and then return to the student menu

        private void DisplayListReturn()
        {
            DisplayList();
            Console.Clear();
            StudentMenu();
        }
        

        //Display match method
        
        private void SearchId()
        {
            generalMethod.Header();

            bool foundmatch = false;
           
            int id;
            
            redo:
            Console.WriteLine("Search Student ID");

            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch 
            {
                Console.WriteLine("Invalid id entry");
                goto redo;
            }
            
            Console.WriteLine("Matches:");
            
            Console.WriteLine("*******************************************\n");

            //Searches input id against all ids of students in the student list if foundmatch is true, option to delete student is presented

            foreach (Student s in studentList)
            {
                if (s.Id == id)
                {
                    Console.WriteLine(s.ToString());
                    foundmatch = true;

                    Console.WriteLine("\n\n\n\nDo you wish to delete this student from the list?");

                    if (generalMethod.YesNo("Yes", "No"))
                    {
                        studentList.Remove(s);
                        Console.WriteLine("\n\nStudent Deleted");
                        break;
                    }
                    

                }

            }
            // if foundmatch false, this message displays
            if (!foundmatch)
            
            {
                Console.WriteLine("No Student matching that ID");
            }

            generalMethod.AnyKey();

            StudentMenu();


        }

       
        //method for saving to csv file
        
        private void SaveToCsv()
        {
            generalMethod.Header();

            Console.WriteLine("To save file");

            //assign filePath (See FilePath method in General methods)

            string filePath = generalMethod.FilePath();

            // if the file doesn't exist

            if (!File.Exists(filePath))
            {
                //use save method
                Save(filePath);
            }
            else
            {
                // if the file does exist

                Console.WriteLine("A file with this name exists.\nDo you wish to overwrite or append this file with the current working list?");
                
                //use yesno bool method to present choice

                if (generalMethod.YesNo("Overwrite", "Append"))
                {
                    //use overwrite method
                    Overwrite(filePath);
                }
                else
                {
                    //use append method0.
                    Append(filePath);
                }
            }

            StudentMenu();

        }


        //method to save to csv file using streamwriter

        private void Save(string filePath)
        {

            StreamWriter sw = new StreamWriter(filePath, true);


            foreach (Student s in studentList)
            {
                sw.WriteLine(s.ToString());

            }


            Console.WriteLine("The current working list has been saved to the file");

            sw.Close();

            generalMethod.AnyKey();


        }

        //append .csv file method
        private void Append(string filePath)
        {
            //streamwriter with true to append rather than overwrite
            StreamWriter sw = new StreamWriter(filePath, true);


            foreach (Student s in studentList)
            {
                sw.WriteLine(s.ToString());

            }


            Console.WriteLine("The current working list has been appended to the file");

            sw.Close();

            generalMethod.AnyKey();

        }



        //method to overwrite existing csv file

        private void Overwrite(string filePath)
        {
            // streamwriter without true to overwrite file rather than append
            StreamWriter sw = new StreamWriter(filePath);


            foreach (Student s in studentList)
            {
                sw.WriteLine(s.ToString());

            }

            Console.WriteLine("The current working list has overwritten the file");

            generalMethod.AnyKey();

            sw.Close();


        }


         //Method for importing list from csv file

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
                     //object line read by streamreader
                     var line = sr.ReadLine();
                     
                     //split line objects by "," and input to array values
                     var values = line.Split(',');

                     string fname, lname, phone, email, status;
                     int id;

                     //array values then assigned to variables

                     fname = values[0];
                     lname = values[1];
                     phone = values[2];
                     email = values[3];
                     id = int.Parse(values[4]);
                     status = values[5];

                     //use these variables in the addstudent method to add each line object into the working list

                     AddStudent(fname, lname, phone, email, id, status);

                 }
                 sr.Close();
                 
                
                 DisplayList();
             }

             StudentMenu();
         }

        //method to allow access to studentList in other classes
         public List<Student> GetStudentList()
         {
             return studentList;

         }

        //method to generate test items in the student list

         public void PopulateStudentList()
         {
            // Student s1 = new Student("BOB", "GLEESON", "087612345", "bob@bob.com", 24, "UNDER");
             Student s2 = new Student("KEVIN", "LANIGAN", "085634213", "kevin@kevin.com", 53, "POST");
             Student s3 = new Student("MIKE", "MAHER", "0861661240", "mike@mike.com", 91, "POST");
            // Student s4 = new Student("PETER", "STRINGFELLOW", "0851556931", "yolanda@email.com", 30, "UNDER");
            // Student s5 = new Student("JOHN", "CONNOR", "0856314121", "john@gmail.com", 50000, "SCIENCE");
            // studentList.Add(s1);
             studentList.Add(s2);
             studentList.Add(s3);
            // studentList.Add(s4);
            // studentList.Add(s5);

         }
        
    }
}
