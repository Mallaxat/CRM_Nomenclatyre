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

            //CreateBD();
            /*            Users userbuf=new Users();
                        Users userbuf2 =new Users();
                        userbuf2.Login = "1";
                        userbuf2.Password = "1";

                        bool result = Logging.IsLogin(userbuf2,out userbuf);
                        Console.WriteLine(result);
            */
            Managers man = new Managers
            {
                FirstName ="Petr",
                LastName = "Petr",
            };

            Users user = new Users
            {
                Login = "2",
                Password="2",
                Manager = man
            };

            SqlService.SQL_User.AddTab_On(user);

            List<Users> list = new List<Users>();
            list = SqlService.SQL_User.GetTab_Of();
            foreach(var i in list)
            {
                Console.WriteLine($"{i.Id} {i.Login} {i.Manager.LastName}");
            }

        }
    }
}
