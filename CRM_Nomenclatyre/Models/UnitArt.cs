using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Nomenclatyre.Models;
using System.Data.Linq.Mapping;



namespace CRM_Nomenclatyre.Models
{

    [Table(Name = "dbo.tab_UnitArt")]
    public class UnitArt
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column(Name = "CostPrice")]
        public decimal CostPrice { get; set; }

        [Column(Name = "Price")]
        public decimal Price { get; set; }

        [Column(Name = "Logistics")]
        public decimal Logistics { get; set; }

        [Column(Name = "Comission")]
        public decimal Comission { get; set; }
    }
}
