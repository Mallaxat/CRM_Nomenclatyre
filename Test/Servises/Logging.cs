using Test.Models;
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

namespace Test.Servises
{

    public  class Logging
    {
        List<Users> List_users { get; set; }
        private const string CONNECT = "Marketplace";
        private string connect;
        public Logging() 
        {
            connect = ConfigurationManager.ConnectionStrings[CONNECT].ConnectionString;
            List_users = GetTab_Of();

        }
        public bool IsLogin(Users user , out Users userout)
        {
            if (List_users == null)
            {
                userout = null;
                return false;
            }
            //Отключенный режим
            foreach(var item in List_users)
            {
                if (item.Login == user.Login && item.Password == user.Password)
                {
                    userout= item;
                    return true;
                }
            }
            userout = null;
            return false;

        }
        public bool Regist(Users user, out Users userout)
        {
            //Подключенный режим
            userout=null;   
            return false;

        }

        private List<Users> GetTab_Of()
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                List<Users> result=new List<Users>();   
                conn.Open();
                string comand = "Select * From tab_Users";
                SqlDataAdapter adapter = new SqlDataAdapter(comand,conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                DataSet ds_tab=new DataSet();
                //Заполняем
                adapter.Fill(ds_tab);
                //Возьми первую таблицу из DataSet, но у меня там только 1 таблица и будет
                DataTable dt_tap = ds_tab.Tables[0];

                foreach(DataRow item in dt_tap.Rows)
                {
                    result.Add(new Users
                    {
                        Id = Convert.ToInt32(item[0]),
                        Login = item[1].ToString(),
                        Password = item[2].ToString(),

                    });
                }
                return result;
            }

        }



    }
}
