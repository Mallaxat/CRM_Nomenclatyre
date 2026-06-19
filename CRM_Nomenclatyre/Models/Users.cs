using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Nomenclatyre.Models
{

    /*    [Table(name: "tab_Users")]
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
    */

        [Table(Name = "dbo.tab_Users")]
        public class Users
        {
            private EntityRef<Managers> _manager;

            public Users()
            {
                _manager = new EntityRef<Managers>();
            }

            [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
            public int Id { get; set; }

            [Column(Name = "Login", CanBeNull = false)]
            public string Login { get; set; }

            [Column(Name = "Password", CanBeNull = false)]
            public string Password { get; set; }

            // Один пользователь — один профиль менеджера
            [Association(
                Storage = "_manager",
                ThisKey = "Id",
                OtherKey = "UserId",
                IsForeignKey = false
            )]
            public Managers Manager
            {
                get { return _manager.Entity; }
                set { _manager.Entity = value; }
            }
        }
    }

