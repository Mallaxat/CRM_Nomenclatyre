using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Servise
{
    public static class Logging
    {
        public static bool Login(Users user)
        {
            //Задача из списка найти соответсвующего юзера
            using (var db = new Context())
            {
                foreach (var item in db.DbUsers)
                {
                    if (item.Login == user.Login && item.Password==user.Password)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

    }
}
