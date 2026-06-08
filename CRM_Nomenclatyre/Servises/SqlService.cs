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
    }

    public static class SqlService
    {
        private const string CONNECT = "DB_MarketplaceMain";
        private static string connect = ConfigurationManager.ConnectionStrings[CONNECT].ConnectionString;

        public static class SQL_User
        {
            public static List<Users> GetTab_Of()
            {
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    List<Users> result = new List<Users>();
                    conn.Open();
                    string comand = $"Select * From {TAB_NAME.tab_Users.ToString()}";
                    SqlDataAdapter adapter = new SqlDataAdapter(comand, conn);
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
                            Login = item[1].ToString(),
                            Password = item[2].ToString(),
                        });
                    }
                    return result;
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
    }
}
