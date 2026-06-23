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

        public static class TableSQL
        {
            public static DataSet LoadSetBD(string tableName, int id)
            {
                try
                {
                    using (conn = new SqlConnection(connect))
                    {
                        adapter = new SqlDataAdapter($"Select * from {tableName} where ManagerId={id}", conn);
                        SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);

                        dataSet = new DataSet();
                        adapter.Fill(dataSet, tableName);
                        return dataSet;
                    }              
                }
               catch
                {
                    return null;
                }
                finally
                {
                    if (conn != null || conn.State == ConnectionState.Open) conn.Close();

                }
            }
            public static DataSet LoadSetBD(List<string> tableNames)
            {
/*                try
                {*/
                    using (conn = new SqlConnection(connect))
                    {

                    dataSet = new DataSet();
                    foreach (var table in tableNames)
                        {
                            adapter = new SqlDataAdapter($"Select * from {table}", conn);
                            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);

                            adapter.Fill(dataSet, table);
                        }

                        return dataSet;
                    }
/*                }
                catch
                {
                    return null;
                }
                finally
                {
                    if (conn != null || conn.State == ConnectionState.Open) conn.Close();

                }*/
            }
            public static void UpDateBD(string tableName,DataSet dataSet)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connect))
                    {
                        con.Open();
                        adapter = new SqlDataAdapter($"Select * from {tableName}", connect);
                        var bulder = new SqlCommandBuilder(adapter);

                        adapter.Update(dataSet, tableName);
                        dataSet.Tables[tableName].Clear();
                        adapter.Fill(dataSet, tableName);
                    }
                }
                catch
                {
                    throw;
                }
            } 
            public static void AddUnitArts(int id)
            {
                using(var db =new Context())
                {
                    // если не нашел такой юнит
                    if(!db.DbUnitArts.Any(x => x.Id == id))
                    {
                        db.DbUnitArts.Add(new UnitArt
                        {
                            Id = id,
                            CostPrice = 0,
                            Price = 0,
                            Logistics = 0,
                        });
                        db.SaveChanges();
                    }
                }
            }
            public static void AddUnitArts(int id,decimal tovar)
            {
                using (var db = new Context())
                {
                    TypeCommission result = db.DbTypeCommissions.FirstOrDefault(x => x.TovarId == tovar);
                    // если не нашел такой юнит
                    if (!db.DbUnitArts.Any(x => x.Id == id))
                    {

                        db.DbUnitArts.Add(new UnitArt
                        {
                            Id = id,
                            CostPrice = 0,
                            Price = 0,
                            Logistics = 0,
                            Comission=result.NameValue,
                        });
                        db.SaveChanges();

                    }
                }
            }

        }

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
                    return db.DbUsers.Include(m=>m.Manager).FirstOrDefault(u=>u.Login==user.Login);
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
        public static class ArticulSQL
        {
            public static List<Articles> GetArticuls(int id)
            {
                using (var db = new Context())
                {
                    
                   return db.DbArticles.Include(u => u.Unit).Where(m=>m.ManagerId==id).ToList();

                }
            }
            public static bool FindBarcode(string bar)
            {
                using(var db = new Context())
                {
                    if(db.DbArticles.FirstOrDefault(x=>x.Barcod==bar)==null) return false;
                    return true;
                }
            }
            public static bool FindArticul(string art)
            {
                using (var db = new Context())
                {
                    if (db.DbArticles.FirstOrDefault(x => x.Articul == art) == null) return false;
                    return true;
                }
            }

        }

        public static class DirectorySQL
        {
            public static List<TypeTovar> GetTypeTovar()
            {
                List<TypeTovar> result;
                using ( var db = new Context())
                {
                    result = db.DbTypeTovars.ToList();
                    return result;
                }
            }
            public static List<TypeCommission> GetTypeCommission()
            {
                List<TypeCommission> result;
                using (var db = new Context())
                {
                    result = db.DbTypeCommissions.ToList();
                    return result;
                }
            }

        }



    }
}
