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

namespace CRM_Nomenclatyre.Servises
{
    enum TAB_NAME
    {
        tab_Article,
        tab_Manager,
        tab_Users,
        tab_TypeTovar
    }

    enum SQL_PROC
    {
        GET_MANAGER,
        ADD_USER,
        GET_USER,
        GET_ARTICLES_BY_MANAGER,
        FIND_BAR,
        FIND_ARTICLE,
        ADD_ARTICLE,
        CHECK_USER_LOGIN


    }

    public static class SqlService
    {
        private static DataTable dataTable;
        private static DataSet dataSet;
        private static SqlDataAdapter adapter;
        private static SqlConnection conn =null;

        private const string CONNECT = "DB_MarketplaceMain";
        private static string connect = ConfigurationManager.ConnectionStrings[CONNECT].ConnectionString;

        public static List<T> GetDataSet<T>() where T: class
        {
            using (DataContext db = new DataContext(connect))
            {
                return db.GetTable<T>().ToList();
            }
        }
        public static void UpdateTableBD(DataSet dataSet, string tableName,int id)
        {

            using (conn = new SqlConnection(connect))
            {
                conn.Open();
                string sqlCommand = $"Select * from {tableName} where ManagerId={id}";
                adapter = new SqlDataAdapter(sqlCommand, conn);

                SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);

                DataTable table=dataSet.Tables[tableName];

                dataTable=dataSet.Tables[tableName];
                adapter.Update(table);
                table.Clear();
                adapter.Fill(dataSet,tableName);

            }


        }


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
        public static DataTable LoadTableBD(string tableName, int id)
        {
            try
            {
                using (conn = new SqlConnection(connect))
                {
                    adapter = new SqlDataAdapter($"Select * from {tableName} where ManagerId={id}", conn);
                    SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);

                    dataSet = new DataSet();
                    adapter.Fill(dataSet, tableName);
                    dataTable = dataSet.Tables[0];
                    return dataTable;
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



        public static class SQL_User
        {
/*            public static List<Users> GetTab_Of()
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
            }*/
   
            public static void  AddTab_On(Users user)
            {
                using (SqlConnection con= new SqlConnection(connect))
                {
                    
                    con.Open();
                    SqlCommand cmd=new SqlCommand(SQL_PROC.ADD_USER.ToString(), con);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Login",user.Login);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@FirstName", user.Manager.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.Manager.LastName);
                    cmd.ExecuteNonQuery();

                }
            }
            public static bool Check_One(string login)
            {
                using (SqlConnection con = new SqlConnection(connect))
                {
                    int result = 0;
                    con.Open();
                    SqlCommand cmd = new SqlCommand(SQL_PROC.CHECK_USER_LOGIN.ToString(), con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Login", login);

                    SqlParameter outPar = cmd.Parameters.Add("@Result", SqlDbType.Int);
                    outPar.Direction = ParameterDirection.Output;

                   cmd.ExecuteNonQuery();
                   result = Convert.ToInt32(outPar.Value);

                    return (result > 0) ? true : false;
                }
            }



        }
        public static class SQL_Manager
        {
            public static List<Managers> GetTab_Of()
            {
                using (conn = new SqlConnection(connect))
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
                using (conn = new SqlConnection(connect))
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
        public static class SQL_TypeTovar
        {
            public static List<TypeTovar> GetTab_Of()
            {
                using (conn = new SqlConnection(connect))
                {
                    List<TypeTovar> result = new List<TypeTovar>();
                    conn.Open();
                    string comand = $"Select * From {TAB_NAME.tab_TypeTovar.ToString()}";
                    SqlDataAdapter adapter = new SqlDataAdapter(comand, conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    DataSet ds_tab = new DataSet();
                    //Заполняем
                    adapter.Fill(ds_tab);
                    //Возьми первую таблицу из DataSet, но у меня там только 1 таблица и будет
                    DataTable dt_tap = ds_tab.Tables[0];

                    foreach (DataRow item in dt_tap.Rows)
                    {
                        result.Add(new TypeTovar
                        {
                            Name = item["Name"].ToString(),
                            Id = Convert.ToInt32(item["Id"]),

                        });
                    }
                    return result;
                }

            }
        }
        
    
        public static class SQL_Article
        {

            public static List<Articles> GetArticlesOn(int id)
            {
                using(conn = new SqlConnection(connect))
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
                            Sort = Convert.ToInt32(reader["Sort"]),
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
                using (var bd = new Context())
                {
                    int result = bd.FIND_BAR(bar);
                    return (result > 0) ? true : false;
                }
            }
            public static bool FindArticule(string articule)
            {
                //Подключенный режим, чтобы не возникло ситуаций дубля баркода
                using (conn = new SqlConnection(connect))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_PROC.FIND_ARTICLE.ToString(), conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Articul", articule);
                    SqlParameter outPar = cmd.Parameters.Add("@Result", SqlDbType.Int);
                    outPar.Direction = ParameterDirection.Output;

                    int result = cmd.ExecuteNonQuery();
                    return (result > 0) ? true : false;
                }
            }

        }
    }
}
