using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_Nomenclatyre.Models
{
    [Table(name: "tab_Article")]
    public class Articles
    {
        //Свойства
        [Key]
        public int Id { get; set; }
        [Required]
        public string Named { get; set; }
        [Required]
        public string Sort { get; set; }
        // Внешний ключ на менеджера
        [Required]
        public int ManagerId { get; set; }

        // Навигационное свойство
        [ForeignKey("ManagerId")]
        public virtual Managers Manager { get; set; }


    }
}
