using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Nomenclatyre.Models
{
    /*    [Table(name: "tab_TypeTovar")]
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
        }*/
    
    [Table(Name = "dbo.tab_TypeTovar")]
    public class TypeTovar
    {
        private EntitySet<Articles> _articles;
        public TypeTovar()
        {
            _articles = new EntitySet<Articles>(
                attach_Articles,
                detach_Articles
            );
        }
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(Name = "Name", CanBeNull = false)]
        public string Name { get; set; }
        [Association(
                    //где внутри класса хранится коллекция.
                    Storage = "_articles",
                    // ThisKey — поле текущего класса 
                    ThisKey = "Id",
                    // OtherKey — поле связанного класса 
                    OtherKey = "Sort"
                )]
        public EntitySet<Articles> Articles
        {
            get { return _articles; }
            set { _articles.Assign(value); }
        }


        // Метод вызывается, когда артикул добавляют в TypeTovar.Articles.
        private void attach_Articles(Articles article)
        {
            article.TypeTovar = this;
        }  
        private void detach_Articles(Articles article)
        {
            // Убираем обратную связь.
            article.TypeTovar = null;
        }

    }
}
