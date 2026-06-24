using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Nomenclatyre.Models
{

    /*        [Table(name: "tab_TypeCommission")]
            public class TypeCommission 
            {
            [Key]
            public int Id { get; set; }


            [Required]
            [Index("IX_TypeCommission_SortId", IsUnique = true)]
            public int SortId { get; set; }

            [Required]
            public decimal NameValue { get; set; }

            [ForeignKey("SortId")]
            public virtual TypeTovar TypeTovar { get; set; }

        }*/

    [Table(Name = "dbo.tab_TypeCommission")]
    public class TypeCommission
    {
        private EntityRef<TypeTovar> _typeTovar;

        // Конструктор
        public TypeCommission()
        {
            // Инициализируем связь с TypeTovar.
            _typeTovar = new EntityRef<TypeTovar>();
        }

        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(Name = "SortId", CanBeNull = false)]
        public int SortId { get; set; }

        [Column(Name = "NameValue", CanBeNull = false)]
        public decimal NameValue { get; set; }

        //связь между TypeCommission и TypeTovar.

        [Association(
            //где хранится связанный объект.
            Storage = "_typeTovar",
            // ThisKey — поле текущего класса 
            ThisKey = "SortId",
            // OtherKey — поле связанного класса
            OtherKey = "Id",

            // IsForeignKey = true ключ в текущей таблице 
            IsForeignKey = true
        )]
        public TypeTovar TypeTovar
        {
            get { return _typeTovar.Entity; }
            set { _typeTovar.Entity = value; }
        }
    }

}
