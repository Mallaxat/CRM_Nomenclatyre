using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;


namespace CRM_Nomenclatyre.Models
{
       [Table(name: "tab_Manager")]
        public class Managers
        {
            //Свойства
            [Key]
            [ForeignKey("User")]
            public int UserId { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            // Один менеджер — много артикулов
            public ICollection<Articles> articles { get; set; }

            public virtual Users User { get; set; }

  


    }
}
