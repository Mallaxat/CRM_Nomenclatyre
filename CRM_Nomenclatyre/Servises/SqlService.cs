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
        private static SqlConnection conn = null;

        private const string CONNECT = "DB_MarketplaceMain";
        private static string connect = ConfigurationManager.ConnectionStrings[CONNECT].ConnectionString;




    }
}
