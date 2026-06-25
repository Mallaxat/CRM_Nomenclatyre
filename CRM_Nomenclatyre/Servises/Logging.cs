using CRM_Nomenclatyre.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using CRM_Nomenclatyre.Servises;


namespace CRM_Nomenclatyre.Servises
{

    public static class Logging
    {
        //Метод, для проверки наличия и входа
        public static bool IsLogin(Users user)
        {
            if(user==null) return false;
            if (SqlService.UserSQL.FindUser(user)) return true;
            return false;
        }
        
        public static bool FindUser(Users user)
        {
            if (user == null) return false;
            return SqlService.UserSQL.FindUser(user);
        }
        public static bool IsRegist(Users user)
        { 
            return SqlService.UserSQL.AddUser(user); ;
        }


    }
}
