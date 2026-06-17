using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;


namespace Test.Models
{

    [Table(name: "tab_UnitArt")]
    public class UnitArt
    {

        [Key]
        [ForeignKey("Article")]
        public int Id { get; set; }

        public decimal CostPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Logistics { get; set; }

        public decimal Comission { get; set; }

        public virtual Articles Article { get; set; }




    }
}
