using CRM_Nomenclatyre.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace CRM_Nomenclatyre.Servises
{

    public static class Logging
    {
        public static bool IsLogin(Users user , out Users userout)
        {

            //Задача из списка найти соответсвующего юзера
            using (var db = new Context())
            {
                var userss = db.DbUsers.ToList();

                userout =db.DbUsers.FirstOrDefault(x=>x.Login==user.Login);
                if (userout==null) return false;
                if (user.Login == userout.Login) return true;
                return false;
                
            }
        }

    }
}
