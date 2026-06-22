using CRM_Nomenclatyre.Servises;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace CRM_Nomenclatyre.Models
{
    public class Articles
    {
        //Свойства
        [Key]
        public int Id { get; set; }
        [Required]
        public string Named { get; set; }
        [Required]
        public int Sort { get; set; }
        [ForeignKey("Sort")]
        public virtual TypeTovar TypeTovar { get; set; }
        // Внешний ключ на менеджера
        [Required]
        public int ManagerId { get; set; }

        // Навигационное свойство
        [ForeignKey("ManagerId")]
        public virtual Managers Manager { get; set; }

        public virtual UnitArt Unit { get; set; } = new UnitArt();

        //Свойства артикула
        public string? Size { get; set; } = "0";
        public string Barcod { get; set; } = mSettings.Rand(12);
        public int Count { get; set; }
        public string Articul { get; set; } = mSettings.Rand(5,false);


    }
}

