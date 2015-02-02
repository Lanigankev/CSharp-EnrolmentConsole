using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

//Kevin Lanigan 10186146

namespace constructs
{
    class Hashcode
    {
        public string PassHash(string data)
        {
            // to encrypt string values 
            
            SHA1 sha = SHA1.Create();
            
            //array of bytes = encrypted string input
            
            byte[] hashdata = sha.ComputeHash(Encoding.Default.GetBytes(data));

            StringBuilder returnValue = new StringBuilder();

            //forloop to writeout hashdata array

            for (int inst = 0; inst < hashdata.Length; inst++)
            {
                returnValue.Append(hashdata[inst].ToString());
            }
            
            //return string value

            return returnValue.ToString();
        }
    }
}
