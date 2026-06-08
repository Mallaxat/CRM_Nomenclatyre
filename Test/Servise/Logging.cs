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
        public static bool IsLogin(Users user, out Users resUser)
        {
            //Задача из списка найти соответсвующего юзера
            using (var db = new Context())
            {
                resUser = db.DbUsers.FirstOrDefault(x => x.Login == user.Login);
                if (resUser == null) return false;
                else return true;
            }
        }

    }
}
