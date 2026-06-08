using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Test.Servise;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Users user = new Users
            {
                Id = 1,
                Login = "1",
                Password = "1"
            };

            bool result=Logging.Login(user);
            Console.WriteLine(result);  


        }
    }
}
