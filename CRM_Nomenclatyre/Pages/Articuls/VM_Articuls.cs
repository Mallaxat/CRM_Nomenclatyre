using CRM_Nomenclatyre.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CRM_Nomenclatyre.Servises;
using System.Windows.Input;
using System.Data;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CRM_Nomenclatyre.Pages
{

    public class VM_Articuls:INotifyPropertyChanged
    {
        //Свойства 
        private const string TABLENAME = "tab_Article";
        private Settings Setting { get; set; }

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
       
        public Articles _seletArticul;
        public Articles SeletArticul
        {
            get => _seletArticul;
            set
            {
                if (value == null) return;
                _seletArticul = value;
                OnPropertyChanged();
            }
        }

        //Команды
        public ICommand cUpdate { get; }

        //Коснтруктор
        public VM_Articuls(Settings Setting)
        {
            this.Setting = Setting;
            ListDataSet = SqlService.LoadSetBD(TABLENAME, Setting.user.Id);
            ListDataTable = ListDataSet.Tables[0];

            //Вносим дефолтное значение для менеджера
            if (ListDataTable != null && ListDataTable.Columns.Contains("ManagerId"))
            {
                ListDataTable.Columns["ManagerId"].DefaultValue = Setting.user.Id;
            }

            cUpdate = new RelayCommand(_ =>
            {
                UpdateDataSet();
            });

        }
        //Методы
        private void UpdateDataSet()
        {
            
            SqlService.UpdateTableBD(ListDataSet, TABLENAME,Setting.user.Id);
        }


        //Интерфейс
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }





    }
}
