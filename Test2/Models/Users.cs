using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Test2.Models
{

        public class Users
        {
            //Свойства
            [Key]
            public int Id { get; set; }
            [Required]
            public string Login { get; set; }
            [Required]
            public string Password { get; set; }
            // Один пользователь — один профиль менеджера
            public virtual Managers Manager { get; set; }

        }


    }

