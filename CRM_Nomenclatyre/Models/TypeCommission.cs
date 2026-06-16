using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Nomenclatyre.Models
{

        [Table(name: "tab_TypeCommission")]
        public class TypeCommission 
        {
        [Key]
        public int Id { get; set; }


        [Required]
        [Index("IX_TypeCommission_SortId", IsUnique = true)]
        public int SortId { get; set; }

        [Required]
        public decimal NameValue { get; set; }

        [ForeignKey("SortId")]
        public virtual TypeTovar TypeTovar { get; set; }

    }
    
}
