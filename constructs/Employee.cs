using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace constructs
{
    class Employee:Person
    {
        //property Salary
        public double Salary { get; set; }

        //constructor which inherits first name, last name, phone number and email from person
        public Employee(string fname, string lname, string phone, string email, double salary)
            : base(fname, lname, phone, email)
        {
            Fname = fname;
            Lname = lname;
            Phone = phone;
            Email = email;
            Salary = salary;
        }

        
    }
}

