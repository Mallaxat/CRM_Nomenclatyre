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

    public class VM_Articuls : INotifyPropertyChanged
    {
        //Свойства 
        private const string TABLENAME = "Articles";
        private const string TABLENAME2 = "UnitArts";
        
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

        private int _filterIndex;
        public int FilterIndex
        {
            get => _filterIndex;
            set
            {
                if (value < 0) return;
                _filterIndex = value;
                OnPropertyChanged();
            }
        }

        //Команды
        public ICommand cUpdate { get; }
        public ICommand cNext { get; }

        public ICommand cFilterAdd {  get; }
        public ICommand cFilterDelete { get; }
        
        //Коснтруктор
        public VM_Articuls(mSettings _setting)
        {
            this._setting = _setting;

            //ListDataSet = SqlService.TableSQL.LoadSetBD(TABLENAME, _setting.user.Id);
            List<string> list = new List<string> { TABLENAME, TABLENAME2 };
            ListDataSet = SqlService.TableSQL.LoadSetBD(list);
            ListDataTable = ListDataSet.Tables[0];

            ListArticles = SqlService.ArticulSQL.GetArticuls(_setting.user.Id);

            TypeList = SqlService.DirectorySQL.GetTypeTovar();
            SetNums();
            ListDataTable.TableNewRow -= SetNums;
            ListDataTable.TableNewRow += SetNums;

            cUpdate = new RelayCommand(_ =>
                        {
                            UpdateDataSet();
                        });

            cNext = new RelayCommand(_ =>
            {
                VM_Main main = VM_Main.Initialize(_setting);
                main.UpdatePage(_setting.serviseWindow.PageOpen<VM_Unit, UnitPage>(_setting));
            });

            cFilterAdd = new RelayCommand(_ =>
            {
                FilterStart();
            });
            cFilterDelete = new RelayCommand(_ =>
            {
                FilterStop();
            });

        }
        
        
        //Методы
        private void UpdateDataSet()
        {
            SqlService.TableSQL.UpDateBD(TABLENAME, ListDataSet);
            DataTable tabUnit = ListDataSet.Tables["UnitArts"];
            int id = -1;
            decimal tovartype = -1;
            bool UnitIdexist = true;
            foreach (DataRow row in ListDataTable.Rows)
            {
                id = row.Field<int>("Id");
                tovartype = row.Field<int>("TypeTovarID");

                //Переводим к Linq и если не пустой и если айдишник равен вернет тру
                UnitIdexist = tabUnit.AsEnumerable().Any(x => !x.IsNull("Id") && x.Field<int>("Id") == id);

                if (!UnitIdexist)
                {
                    SqlService.TableSQL.AddUnitArts(id, tovartype);
                }
            }
        }

        private void SetNums(object sender, DataTableNewRowEventArgs e)
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

        private void FilterStart()
        {
            ListDataTable=ListDataSet.Tables[0];
            if (FilterIndex <0 )
            {
                _setting.serviseMessege.Show("Не выбран фильтр","Ошибка фильтрации");
                return;
            }
            DataTable FilterTable = new DataTable();
            //создаем строку
            var row = ListDataTable.AsEnumerable().Where(x => x.Field<int>("TypeTovarID") == FilterIndex);
            //копируем эти значения коллекций в новую таблицу
            //Если в строках есть значения, то мы их копируем, если нет то копернем старую таблицу
            FilterTable= row.Any()?row.CopyToDataTable():ListDataTable.Clone();
            ListDataTable = FilterTable;
        }
       
        private void FilterStop()
        {
            ListDataTable = ListDataSet.Tables[0];

        }
       
        
        //Интерфейс
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }




    }
}
