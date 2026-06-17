using Test.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Test.Servises;
using System.IO;

namespace Test.Models
{
    public class Context:DbContext
    {
        public DbSet<Users> DbUsers { get; set; }
        public DbSet<Managers> DbManagers { get; set; }
        public DbSet<Articles> DbArticles { get; set; }
        public DbSet<UnitArt> DbUnitArts { get; set; }
        public DbSet<TypeTovar> DbTypeTovars { get; set; }
        public DbSet<TypeCommission> DbTypeCommissions { get; set; }

        //Конструктор 
        public Context() : base("DB_MarketplaceMain") { }

        //Процедуры?

        //Это контекст и здесь мы получается указываем параметры для нашей процедуры
        public virtual int FIND_BAR(string bar)
        {

            var cmd=this.Database.Connection.CreateCommand();
            cmd.CommandText = "FIND_BAR";
            cmd.CommandType = CommandType.StoredProcedure;

            //Это тернарный оператор, если не пустой создаем параметр, если пустой создаем параметр с типом данных
            // Входной параметр @Barcod
            var BarParametr = cmd.CreateParameter();
            BarParametr.ParameterName = "@Barcod";
            BarParametr.Value = (object)bar ?? DBNull.Value;
            cmd.Parameters.Add(BarParametr);

            var ResultParametr=cmd.CreateParameter();
            ResultParametr.ParameterName = "@Result";
            ResultParametr.DbType=DbType.Int32;
            ResultParametr.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ResultParametr);

            // Вызываем процедуру
            // Открываем подключение, если оно закрыто
            if (this.Database.Connection.State != ConnectionState.Open)
            {
                this.Database.Connection.Open();
            }

            // Выполняем процедуру
            cmd.ExecuteNonQuery();

            //возвращение результата
            return Convert.ToInt32(ResultParametr.Value);

        }
    }
}
