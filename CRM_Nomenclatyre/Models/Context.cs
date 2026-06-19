using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRM_Nomenclatyre.Models
{
    public class Context:DbContext
    {
        //Этот класс хранит ЗНАЧЕНИЯ в списке всех таблиц, которые есть в БД
        public DbSet<Users> DbUsers { get; set; }
        public DbSet<Managers> DbManagers { get; set; }
        public DbSet<Articles> DbArticles { get; set; }
        public DbSet<UnitArt> DbUnitArts { get; set; }
        public DbSet<TypeTovar> DbTypeTovars { get; set; }
        public DbSet<TypeCommission> DbTypeCommissions { get; set; }

        //Конструктор 
        public Context() : base("DB_MarketplaceMain") { }

    }
}
