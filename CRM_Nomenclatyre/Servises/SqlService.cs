using CRM_Nomenclatyre.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRM_Nomenclatyre.Servises
{
    enum TAB_NAME
    {
        tab_Article,
        tab_Manager,
        tab_Users
    }

    enum SQL_PROC
    {
        GET_MANAGER,
        ADD_USER,
        GET_USER,
        GET_ARTICLES_BY_MANAGER,
        FIND_BAR,
        FIND_ARTICLE,
        ADD_ARTICLE


    }

    public static class SqlService
    {


        private const string CONNECT = "DB_MarketplaceMain";
        private static string connect = ConfigurationManager.ConnectionStrings[CONNECT].ConnectionString;

        public static class SQL_User
        {
            public static List<Users> GetTab_Of()
            {
                List<Users> result = new List<Users>();

                SqlDataAdapter adapter = new SqlDataAdapter(SQL_PROC.GET_USER.ToString(), connect);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                DataSet ds_tab = new DataSet();
                //Заполняем
                adapter.Fill(ds_tab);
                //Возьми первую таблицу из DataSet, но у меня там только 1 таблица и будет
                DataTable dt_tap = ds_tab.Tables[0];

                foreach (DataRow item in dt_tap.Rows)
                {
                    result.Add(new Users
                    {
                        Id = Convert.ToInt32(item[0]),
                        Login = item["Login"].ToString(),
                        Password = item["Password"].ToString(),
                        //Проверяем, что у пользователя нет менеджера
                        Manager = item["UserId"] == DBNull.Value ? null : new Managers
                        {
                            UserId = Convert.ToInt32(item["UserId"]),
                            FirstName = item["FirstName"].ToString(),
                            LastName = item["LastName"].ToString()
                        }
                    });
                }
                return result;
            }
            //ПРОБЛЕМА
            public static bool AddTab_On(Users user)
            {
                using (SqlConnection con= new SqlConnection(connect))
                {
                    int result = 0;
                    con.Open();
                    SqlCommand cmd=new SqlCommand(SQL_PROC.ADD_USER.ToString(), con);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Login",user.Login);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@FirstName", user.Manager.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.Manager.LastName);

                    SqlParameter outPar=cmd.Parameters.Add("@Id",SqlDbType.Int);
                    outPar.Direction = ParameterDirection.Output;

                    result = cmd.ExecuteNonQuery();
                    //ВОТ ТУТ ПРОБЛЕМА ВСЕГДА ВОЗВРАЩАЕТСЯ -1!!!
                    return (result > 0) ? true : false;
                }
            }
        }
        public static class SQL_Manager
        {
            public static List<Managers> GetTab_Of()
            {
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    List<Managers> result = new List<Managers>();
                    conn.Open();
                    string comand = $"Select * From {TAB_NAME.tab_Manager.ToString()}";
                    SqlDataAdapter adapter = new SqlDataAdapter(comand, conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    DataSet ds_tab = new DataSet();
                    //Заполняем
                    adapter.Fill(ds_tab);
                    //Возьми первую таблицу из DataSet, но у меня там только 1 таблица и будет
                    DataTable dt_tap = ds_tab.Tables[0];

                    foreach (DataRow item in dt_tap.Rows)
                    {
                        result.Add(new Managers
                        {
                            UserId= Convert.ToInt32(item[0]),
                            FirstName= item[1].ToString(),
                            LastName= item[2].ToString(),
                        });
                    }
                    return result;
                }
            }

            public static Managers GetOne_Of(int id)
            {
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    Managers result=new Managers();
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    adapter.SelectCommand = new SqlCommand(SQL_PROC.GET_MANAGER.ToString(), conn);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@UserId", id));

                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    DataSet ds_tab = new DataSet();
                    //Заполняем
                    adapter.Fill(ds_tab);
                    //Возьми первую таблицу из DataSet, но у меня там только 1 таблица и будет
                    DataTable dt_tap = ds_tab.Tables[0];

                    foreach (DataRow item in dt_tap.Rows)
                    {
                        result=new Managers
                        {
                            UserId = Convert.ToInt32(item[0]),
                            FirstName = item[1].ToString(),
                            LastName = item[2].ToString(),

                        };
                    }
                    return result;
                }
            }


        }
    
        public static class SQL_Article
        {
            private static DataTable dt_artic;
            private static DataSet ds_artic;
            private static SqlDataAdapter adapter_artic;

            public static List<Articles> GetArticlesOn(int id)
            {
                using(SqlConnection conn = new SqlConnection(connect))
                {
                    List<Articles> result=new List<Articles>();
                    conn.Open();
                    SqlCommand cmd=new SqlCommand(SQL_PROC.GET_ARTICLES_BY_MANAGER.ToString(), conn);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ManagerId",id);
                    SqlDataReader reader=cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        result.Add(new Articles
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Named = reader["Named"].ToString(),
                            Sort = reader["Sort"].ToString(),
                            ManagerId = Convert.ToInt32(reader["ManagerId"]),
                            Size = reader["Size"].ToString(),
                            Barcod = reader["Barcod"].ToString(),
                            Count = Convert.ToInt32(reader["Count"]),
                            Articul = reader["Articul"].ToString(),
                        }
                            );                     
                    }
                    return result;
                }
            }

            public static bool FindBar(string bar)
            {
                //Подключенный режим, чтобы не возникло ситуаций дубля баркода
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    SqlCommand cmd = new SqlCommand(SQL_PROC.FIND_BAR.ToString(), conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Barcod", bar);
                    SqlParameter outPar = cmd.Parameters.Add("@Result", SqlDbType.Int);
                    outPar.Direction = ParameterDirection.Output;

                    int result = cmd.ExecuteNonQuery();
                    return (result > 0) ? true : false;

                }
            }
            public static bool FindArticule(string articule)
            {
                //Подключенный режим, чтобы не возникло ситуаций дубля баркода
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    SqlCommand cmd = new SqlCommand(SQL_PROC.FIND_ARTICLE.ToString(), conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Barcod", articule);
                    SqlParameter outPar = cmd.Parameters.Add("@Result", SqlDbType.Int);
                    outPar.Direction = ParameterDirection.Output;

                    int result = cmd.ExecuteNonQuery();
                    return (result > 0) ? true : false;
                }
            }

            public static void AddArticuleOf(Articles articles)
            {
                //Оффалйн потому что это не критично важно
                adapter_artic = new SqlDataAdapter("Select *from dbo.tab_Article", connect);
                SqlCommandBuilder bild=new SqlCommandBuilder(adapter_artic);

                ds_artic = new DataSet();
                //Заполняем таблицу имя пусть то же будет
                adapter_artic.Fill(ds_artic);
                dt_artic = ds_artic.Tables[0];

                //Создаем новую строку и заполняем значения
                DataRow row = dt_artic.NewRow();
                row[1] = articles.Named;
                row[2] = articles.Sort;
                row[3] = articles.ManagerId;
                row[4] = articles.Size;
                row[5] = articles.Barcod;
                row[6] = articles.Count;
                row[7] = articles.Articul;

                dt_artic.Rows.Add(row);

            }
       
            public static void UpdateArticules()
            {
                adapter_artic.Update(ds_artic);
                dt_artic.Clear();
                adapter_artic.Fill(ds_artic);
            }
        }
    }
}
