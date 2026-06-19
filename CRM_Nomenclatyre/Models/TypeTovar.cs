using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;

using System.Text;
using System.Threading.Tasks;

namespace CRM_Nomenclatyre.Models
{

        public class TypeTovar
        {

            [Key]
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }

            public virtual ICollection<Articles> Articles { get; set; }


            public TypeTovar()
            {
                Articles = new HashSet<Articles>();
            }
        }
    

    
}
