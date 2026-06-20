using CRM_Nomenclatyre.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CRM_Nomenclatyre.Models
{
    public class UnitArt
    {
        [Key]
        public int Id { get; set; }

        public decimal CostPrice { get; set; }

        public decimal Price { get; set; }

        public decimal Logistics { get; set; }

        public decimal Comission { get; set; }
    }
}
