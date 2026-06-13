using CRM_Nomenclatyre.Models;
using CRM_Nomenclatyre.Servises;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CRM_Nomenclatyre.Pages
{
    enum TabNameArticle
    {
        ManagerId,
        Barcod,
        Articul
    }
    public enum TovarList
    {
        юбки,
        брюки,
        куртки
    }

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

        public List<string> _typeList;
        public List<string> TypeList
        {
            get => _typeList;
            set
            {
                if (value == null) return;
                _typeList = value;
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
            TypeList = SqlService.SQL_TypeTovar.GetTab_Of();

            //Вносим дефолтное значение для менеджера

            SetNums();
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


        private void SetNums()
        {
            if (ListDataTable != null && ListDataTable.Columns.Contains(TabNameArticle.Barcod.ToString())) 
            { 
                ListDataTable.Columns[TabNameArticle.Barcod.ToString()].DefaultValue = Settings.Rand(12); 
            }

            if (ListDataTable != null && ListDataTable.Columns.Contains(TabNameArticle.Articul.ToString())) 
            { 
                ListDataTable.Columns[TabNameArticle.Articul.ToString()].DefaultValue = Settings.Rand(5, false);
            }

            if (ListDataTable != null && ListDataTable.Columns.Contains(TabNameArticle.ManagerId.ToString()))
            {
                ListDataTable.Columns[TabNameArticle.ManagerId.ToString()].DefaultValue = Setting.user.Id;
            }

        }


    }
}
