using CRM_Nomenclatyre.Models;
using CRM_Nomenclatyre.Servises;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace CRM_Nomenclatyre.Pages
{ 
    public class VM_Unit : INotifyPropertyChanged
    {
        //Свойства
        public mSettings _setting {  get; set; }

        private DataSet _listDataSet;
        public DataSet ListDataSet
        {
            get => _listDataSet;
            set
            {
                if (value == null) return;
                _listDataSet = value;
                OnPropertyChanged();
            }
        }

        private DataTable _listDataTabe;
        public DataTable ListDataTable
        {
            get => _listDataTabe;
            set
            {
                if (value == null) return;
                _listDataTabe = value;
                OnPropertyChanged();
            }
        }
        
        private List<UnitArt> tableValue;
        public List<UnitArt> TableValue
        {
            get => tableValue;
            set
            {
                if (value == null) return;
                tableValue = value;
                OnPropertyChanged();
            }
        }

        private UnitArt selectRow;
        public UnitArt SelectRow
        {
            get => selectRow;
            set
            {
                if (value == null) return;
                selectRow = value;
                OnPropertyChanged();
            }
        }

        //Команды


        //Конструктор
        public VM_Unit(mSettings Setting)
        {
            this._setting = Setting;
      
            TableValue = SqlService.UnitSQL.GetUnitArt(_setting.user.Id);
            TypeTovar();
        }
        //Методы

        private void TypeTovar ()
        {
            List<TypeTovar> com = SqlService.DirectorySQL.GetTypeTovar();

            int i = 0;

            foreach(var item in TableValue)
            {

                item.Article.TypeTovar=com.Find(x=>x.Id==item.Article.TypeTovarID);
                int i2 = 0;
            }

        }




        //Интерфейс
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
