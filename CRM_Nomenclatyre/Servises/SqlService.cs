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
            public static List<Articles> GetTabArticuls(int id)
            {
                using (var db = new Context())
                {
                    
                   return db.DbArticles.Include(u => u.Unit).Where(m=>m.ManagerId==id).ToList();

                }
            }

            public static void Article(Articles article,int idManager)
            {
                Articles result = new Articles
                {
                    Named = article.Named,
                    Sort=article.Sort,
                    TypeTovar=article.TypeTovar,
                    ManagerId= idManager,
                    Unit= new UnitArt
                    {
                        Logistics = 0,
                        CostPrice = 0,
                        Price = 0,
                    },
                    Size=article.Size,
                    Barcod=article.Barcod,
                    Count=article.Count,
                    Articul=article.Articul
                };
   
                using (var db =new Context())
                {
                    db.DbArticles.Add(article);
                    db.SaveChanges();
                }
            }

            public static void UpdateListArticles(List<Articles> list,int ID)
            {
                using (var db = new Context())
                {
                    var listArt = db.DbArticles.Include(x => x.Unit).Where(x => x.ManagerId == ID).ToList();

                    if (list.Count() <= listArt.Count()) return;
                    foreach (var article in list)
                    {
                        if (!listArt.Contains(article))
                        {
                            Articles result = new Articles
                            {
                                Named = article.Named,
                                Sort = article.Sort,
                                TypeTovar = article.TypeTovar,
                                ManagerId = ID,
                                Unit = new UnitArt
                                {
                                    Logistics = 0,
                                    CostPrice = 0,
                                    Price = 0,
                                },
                                Size = article.Size,
                                Barcod = article.Barcod,
                                Count = article.Count,
                                Articul = article.Articul
                            };

                            db.DbArticles.Add(result);
                            db.SaveChanges();  
                        }
                    }

                }
      
            }



        }




    }
}
