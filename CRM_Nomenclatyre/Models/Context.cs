using System;
using System.Collections.Generic;
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

        //Конструктор 
        public Context():base("Db_Marketplace"){}

    }
}
