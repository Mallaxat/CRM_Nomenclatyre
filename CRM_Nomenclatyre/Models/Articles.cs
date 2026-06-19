using CRM_Nomenclatyre.Servises;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace CRM_Nomenclatyre.Models
{
    /*  [Table(Name = "dbo.tab_Article")]
      public class Articles
      {
          [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
          public int Id { get; set; }

          [Column(Name = "Named", CanBeNull = false)]
          public string Named { get; set; }

          [Column(Name = "Sort", CanBeNull = false)]
          public int Sort { get; set; }

          [Column(Name = "ManagerId", CanBeNull = false)]
          public int ManagerId { get; set; }

          [Column(Name = "Size", CanBeNull = true)]
          public string Size { get; set; } = "0";

          [Column(Name = "Barcod", CanBeNull = true)]
          public string Barcod { get; set; }

          [Column(Name = "Count", CanBeNull = false)]
          public int Count { get; set; }

          [Column(Name = "Articul", CanBeNull = true)]
          public string Articul { get; set; }
      }*/
    [Table(Name = "dbo.tab_Article")]        
    public class Articles
    {
        private EntityRef<Managers> _manager;

        public Articles()
        {
            _manager = new EntityRef<Managers>();
            _typeTovar = new EntityRef<TypeTovar>();
        }

        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(Name = "Named", CanBeNull = false)]
        public string Named { get; set; }

        [Column(Name = "Sort", CanBeNull = false)]
        public int Sort { get; set; }

        [Column(Name = "ManagerId", CanBeNull = false)]
        public int ManagerId { get; set; }

        [Column(Name = "Size", CanBeNull = true)]
        public string Size { get; set; } = "0";

        [Column(Name = "Barcod", CanBeNull = true)]
        public string Barcod { get; set; }

        [Column(Name = "Count", CanBeNull = false)]
        public int Count { get; set; }

        [Column(Name = "Articul", CanBeNull = true)]
        public string Articul { get; set; }

        // Один артикул — один менеджер
        [Association(
            Storage = "_manager",
            ThisKey = "ManagerId",
            OtherKey = "UserId",
            IsForeignKey = true
        )]
        public Managers Manager
        {
            get { return _manager.Entity; }
            set { _manager.Entity = value; }
        }
        private EntityRef<TypeTovar> _typeTovar;

        [Association(
            Storage = "_typeTovar",
            ThisKey = "Sort",
            OtherKey = "Id",
            IsForeignKey = true
        )]
        public TypeTovar TypeTovar
        {
            get { return _typeTovar.Entity; }
            set { _typeTovar.Entity = value; }
        }
    }
}

