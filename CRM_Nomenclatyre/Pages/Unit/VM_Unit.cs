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
        public Settings Setting {  get; set; }

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

        //Команда

        //Конструктор
        public VM_Unit(Settings Setting)
        {
            this.Setting = Setting;
            //TableValue = SqlService.GetDataSet<UnitArt>();
        }
        //Методы

        //Интерфейс
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
