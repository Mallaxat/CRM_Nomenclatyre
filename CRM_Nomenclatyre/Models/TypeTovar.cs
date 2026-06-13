using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Nomenclatyre.Models
{
    [Table(name: "tab_TypeTovar")]
    public class TypeTovar
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // Один тип товара — много артикулов
        public virtual ICollection<Articles> Articles { get; set; }
        public TypeTovar()
        {
            Articles = new HashSet<Articles>();
        }
    }
}
