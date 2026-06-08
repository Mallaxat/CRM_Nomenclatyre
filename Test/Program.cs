using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Test.Servises;

namespace Test
{
    internal class Program
    {

        public static void CreateBD()
        {
            using (var db = new Context())
            {
                var user = new Users
                {
                    Login = "1",
                    Password = "1",
                    Manager = new Managers
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        articles = new List<Articles>
                {
                    new Articles { Named = "Tovar1", Sort = "Type1" },
                    new Articles { Named = "Tovar2", Sort = "Type2" }
                }
                    }
                };

                user.Manager.User = user;

                db.DbUsers.Add(user);
                db.SaveChanges();

                Console.WriteLine($"User Id: {user.Id}");
                Console.WriteLine($"Manager UserId: {user.Manager.UserId}");
            }
        }

        static void Main(string[] args)
        {

            CreateBD();
/*            Users userbuf=new Users();
            Users userbuf2 =new Users();
            userbuf2.Login = "1";
            userbuf2.Password = "1";

            bool result = Logging.IsLogin(userbuf2,out userbuf);
            Console.WriteLine(result);
*/


        }
    }
}
