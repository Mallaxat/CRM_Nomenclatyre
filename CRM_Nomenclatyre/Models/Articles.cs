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
      [Table(Name = "dbo.tab_Article")]
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
      }

}

