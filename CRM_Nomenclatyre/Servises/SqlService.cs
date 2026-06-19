using CRM_Nomenclatyre.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Data.Entity;


namespace CRM_Nomenclatyre.Servises
{
    public static class SqlService
    {
        private static DataTable dataTable;
        private static DataSet dataSet;
        private static SqlDataAdapter adapter;
        private static SqlConnection conn = null;

        private const string CONNECT = "DB_MarketplaceMain";
        private static string connect = ConfigurationManager.ConnectionStrings[CONNECT].ConnectionString;

        public static class UserSQL
        {
            public static Users GetUser(Users user)
            {
                using (var db=new Context())
                {
                    Users result = db.DbUsers.FirstOrDefault(u => u.Login == user.Login && u.Password==user.Password); 
                    return result;
                }
            }
            ///Метод, для получения связки юзер+менеджер
            public static Users GetFullUser(Users user)
            {
                using (var db = new Context())
                {
                    return db.DbUsers.Include(m=>m.Manager).FirstOrDefault(u=>u.Id==user.Id);
                }
            }

            public static bool FindUser(Users user)
            {
                if(GetUser(user)==null) return false;
                return true;

            }
            public static bool AddUser(Users user)
            {
                using (var db = new Context())
                {
                    db.DbUsers.Add(user);
                    db.SaveChanges();
                    return FindUser(user);
                }
                  
            }

        }



    }
}
