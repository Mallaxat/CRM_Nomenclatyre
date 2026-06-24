using Test2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Test2.Models
{
    public class UnitArt
    {
        [Key]
        [ForeignKey(nameof(Article))]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public decimal CostPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Logistics { get; set; }
        public decimal Comission { get; set; }

        public decimal Profit {
            get => field; 
            set
            {
                field = Price - CostPrice - Logistics - (Price * Comission);
            }
        }
        
        public virtual Articles Article { get; set; }
    }
}
