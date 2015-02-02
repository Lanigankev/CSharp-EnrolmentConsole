using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Kevin Lanigan 10186146

namespace constructs
{
    class Student:Person
    {
        //Properties

        public int Id { get; set; }
        public string Status { get; set; }

        //student constructor inherits first name, last name, email, phone from Person Class

        public Student(string fname, string lname, string phone, string email, int id, string status)
            : base(fname, lname, phone, email)
        {
            Fname = fname;
            Lname = lname;
            Phone = phone;
            Email = email;
            Id = id;
            Status = status;
        }
        
        // overrider to string method to add in "," to work with streamwriter in writing to .csv
        
        public override string ToString()
        {
            return  Fname + "," + Lname + "," + Phone + "," + Email + "," + Id + "," + Status;
        }

        //Display organised so to present the items in a legible format

        public string Display()
        {
            return string.Format("{0, -20} | {1, -10} | {2,-18} | {3,-7} | {4}", (Lname + ", " + Fname), Phone, Email, Id, Status);
        }
        
    }
}
