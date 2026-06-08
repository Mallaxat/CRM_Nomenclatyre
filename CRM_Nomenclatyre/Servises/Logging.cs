using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Nomenclatyre.Models;

namespace CRM_Nomenclatyre.Servises
{

    public static class Logging
    {
        public static bool isLogin(Users user)
        {
            //Задача из списка найти соответсвующего юзера
            using (var db = new Context())
            {
               bool result= db.DbUsers.Any(x=>
               x.Login==user.Login);
               return result;
            }
        }

    }
}
