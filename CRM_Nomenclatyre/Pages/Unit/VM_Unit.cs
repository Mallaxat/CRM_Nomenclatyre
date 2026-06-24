using CRM_Nomenclatyre.Models;
using CRM_Nomenclatyre.Servises;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace CRM_Nomenclatyre.Pages
{ 
    public class VM_Unit : INotifyPropertyChanged
    {
        //Свойства
        public mSettings _setting {  get; set; }

        
        private ObservableCollection<UnitArt> tableValue;
        public ObservableCollection<UnitArt> TableValue
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
        public ICommand cUpdate { get; }

        //Конструктор
        public VM_Unit(mSettings Setting)
        {
            this._setting = Setting;
      
            TableValue = SqlService.UnitSQL.GetUnitArt(_setting.user.Id);
            TypeTovar();

            cUpdate = new RelayCommand(_ =>
            {
                Update();
            });
        }
        //Методы

        private void TypeTovar ()
        {
            List<TypeTovar> com = SqlService.DirectorySQL.GetTypeTovar();

            int i = 0;

            foreach(var item in TableValue)
            {
                item.Article.TypeTovar=com.Find(x=>x.Id==item.Article.TypeTovarID);
            }

        }

        public void Update()
        {
            foreach (var item in TableValue)
            {
                item.CountProfit();
                OnPropertyChanged("TableValue");
            }

            SqlService.UnitSQL.UpdateUnitArt(TableValue);
            RefreshTab();
        }
        public void RefreshTab()
        {
            TableValue.Clear();
            TableValue = SqlService.UnitSQL.GetUnitArt(_setting.user.Id);
            TypeTovar();
        }

        //Интерфейс
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
