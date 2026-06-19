using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRM_Nomenclatyre.Models
{
    /*    [Table(name: "tab_Manager")]
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

        }*/

    [Table(Name = "dbo.tab_Manager")]
    public class Managers
    {
        private EntityRef<Users> _user;
        private EntitySet<Articles> _articles;

        public Managers()
        {
            _user = new EntityRef<Users>();

            _articles = new EntitySet<Articles>(
                attach_Articles,
                detach_Articles
            );
        }

        [Column(Name = "UserId", IsPrimaryKey = true, CanBeNull = false)]
        public int UserId { get; set; }

        [Column(Name = "FirstName", CanBeNull = true)]
        public string FirstName { get; set; }

        [Column(Name = "LastName", CanBeNull = true)]
        public string LastName { get; set; }

        // Один менеджер — много артикулов
        [Association(
            Storage = "_articles",
            ThisKey = "UserId",
            OtherKey = "ManagerId"
        )]
        public EntitySet<Articles> Articles
        {
            get { return _articles; }
            set { _articles.Assign(value); }
        }

        // Один менеджер — один пользователь
        [Association(
            Storage = "_user",
            ThisKey = "UserId",
            OtherKey = "Id",
            IsForeignKey = true
        )]
        public Users User
        {
            get { return _user.Entity; }
            set { _user.Entity = value; }
        }

        private void attach_Articles(Articles article)
        {
            article.Manager = this;
        }

        private void detach_Articles(Articles article)
        {
            article.Manager = null;
        }
    }
}
