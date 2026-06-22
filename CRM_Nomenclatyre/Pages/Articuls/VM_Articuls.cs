using CRM_Nomenclatyre.Models;
using CRM_Nomenclatyre.Properties;
using CRM_Nomenclatyre.Servises;
using CRM_Nomenclatyre.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Linq;
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
        Articul,
        Sort
    }

    public class VM_Articuls:INotifyPropertyChanged
    {
        //Свойства 
        private const string TABLENAME = "Articles";
        private mSettings _setting { get; set; }

        private List<Articles> _listarticles;

        public List<Articles> ListArticles
        {
            get => _listarticles;
            set
            {
                if (value == null) return;
                _listarticles = value;
                OnPropertyChanged();
            }
        }

        private Articles _seletArticul;
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

        private List<TypeTovar> _typeList;
        public List<TypeTovar> TypeList
        {
            get => _typeList;
            set
            {
                if (value == null) return;
                _typeList = value;
                OnPropertyChanged();
            }
        }
        
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

        //Команды
        public ICommand cUpdate { get; }
        public ICommand cNext { get; }
        


        //Коснтруктор
        public VM_Articuls(mSettings _setting)
        {
            this._setting = _setting;

            ListDataSet = SqlService.TableSQL.LoadSetBD(TABLENAME, _setting.user.Id);
            ListDataTable = ListDataSet.Tables[0];

            TypeList = SqlService.DirectorySQL.GetTypeTovar();
            SetNums();

            cUpdate = new RelayCommand(_ =>
            {
                UpdateDataSet();
            });

            cNext = new RelayCommand(_ =>
            {            
                VM_Main main = VM_Main.Initialize(_setting);
                main.UpdatePage(_setting.serviseWindow.PageOpen<VM_Unit, UnitPage>(_setting));
            });
        }
        //Методы
       private void UpdateDataSet()
        {

            SqlService.TableSQL.UpDateBD(TABLENAME, ListDataSet);

        }


        private void SetNums()
        {
            if (ListDataTable != null && ListDataTable.Columns.Contains(TabNameArticle.Barcod.ToString()))
            {
                ListDataTable.Columns[TabNameArticle.Barcod.ToString()].DefaultValue = mSettings.Rand(12);
            }

            if (ListDataTable != null && ListDataTable.Columns.Contains(TabNameArticle.Articul.ToString()))
            {
                ListDataTable.Columns[TabNameArticle.Articul.ToString()].DefaultValue = mSettings.Rand(5, false);
            }

            if (ListDataTable != null && ListDataTable.Columns.Contains(TabNameArticle.ManagerId.ToString()))
            {
                ListDataTable.Columns[TabNameArticle.ManagerId.ToString()].DefaultValue = _setting.user.Id;
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
