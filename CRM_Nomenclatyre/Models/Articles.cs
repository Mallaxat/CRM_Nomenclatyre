using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CRM_Nomenclatyre.Servises;

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
        public int Sort { get; set; }
        [ForeignKey("Sort")]
        public virtual TypeTovar TypeTovar { get; set; }
        // Внешний ключ на менеджера
        [Required]
        public int ManagerId { get; set; }

        // Навигационное свойство
        [ForeignKey("ManagerId")]
        public virtual Managers Manager { get; set; }

        public virtual UnitArt Unit {  get; set; }

        //Свойства артикула
        public string? Size { get; set; } = "0";
        public string  Barcod { get; set; }
        public int Count { get; set; }  
        public string Articul {  get; set; }
     
        //конструктор

    
        //Методы


    }
}
