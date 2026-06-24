using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Test2.Models
{
    public class TypeCommission
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [Index("IX_TypeCommission_TovarId", IsUnique = true)]
        public int TovarId { get; set; }

        [Required]
        public decimal NameValue { get; set; }

        [ForeignKey("TovarId")]
        public virtual TypeTovar TypeTovar { get; set; }

    }

}
