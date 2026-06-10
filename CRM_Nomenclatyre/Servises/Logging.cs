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

    public  class Logging
    {
        List<Users> List_users { get; set; }

        public Logging() 
        {
            //connect = ConfigurationManager.ConnectionStrings[CONNECT].ConnectionString;
            List_users = SqlService.SQL_User.GetTab_Of();
        }
        public bool IsLogin(Users user,out Users resultUser)
        {
            if (List_users == null)
            {
                resultUser = null;
                return false;
            }
            
            //Отключенный режим
            foreach(var item in List_users)
            {
                if (item.Login == user.Login && item.Password == user.Password) 
                {
                    resultUser= item;
                    resultUser.Manager = SqlService.SQL_Manager.GetOne_Of(resultUser.Id);
                    return true; 
                }
            }
            resultUser = null;
            return false;
        }
        public bool IsLogin(Users user)
        {
            if (List_users == null) return false;

            foreach (var item in List_users)
            {
                if (item.Login == user.Login) 
                    return false;
            }
            return true;
        }
        public  bool Regist(Users user)
        {
            if (!IsLogin(user))
            {
                return false;
            }

            List_users.Add(user);
            bool test = SqlService.SQL_User.AddTab_On(user);
            return test;

        }



    }
}
