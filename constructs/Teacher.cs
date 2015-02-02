using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Kevin Lanigan 10186146

namespace constructs
{
    class Teacher:Employee
    {
        public string Subject { get; set; }

        //teacher constructor inherits first name, last name, email, phone from Person Class
        //teacher constructor inherits salary from Employee Class

        public Teacher(string fname, string lname, string phone, string email, double salary, string subject)
            : base(fname, lname, phone, email, salary)
        {
            Fname = fname;
            Lname = lname;
            Phone = phone;
            Email = email;
            Salary = salary;
            Subject = subject;
        }

        //override to string method to allow streamwriter to write to .csv
        public override string ToString()
        {
            return  Fname + "," + Lname + "," + Phone + "," + Email + "," + Salary + "," + Subject;
        }


        //display method to allow a legible display of list items
        public string Display()
        {
            return string.Format("{0, -20} | {1, -10} | {2,-18} | {3,-7} | {4}", (Lname+", "+ Fname), Phone, Email, Salary, Subject);
        }
    }
}
