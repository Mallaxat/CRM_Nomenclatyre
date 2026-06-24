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
        #region [Таблицы и Данные]
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

        private  ObservableCollection<UnitArt> ControlTabble;

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
        #endregion

        #region [Процедуры и фильтрации]
        public List<TypeTovar> typeTovars { get; set; }

        private List<string> procCommand;
        public List<string> ProcCommand
        {
            get => procCommand;
            set
            {
                procCommand = new List<string> { "Лучшие товары", "Худшие товары", "Самая высокая цена", "Самая низкая цена","Высокие расходы","Низкие расходы" };
                OnPropertyChanged();
            }
        }

        private int selectType;
        public int SelectType
        {
            get => selectType;
            set
            {
                selectType = value;
                OnPropertyChanged();
            }
        }

        private int selectprocCommand;
        public int SelectProcCommand
        {
            get => selectprocCommand;
            set
            {
                selectprocCommand = value;
                OnPropertyChanged();
            }
        }



        #endregion

        //Команды
        public ICommand cUpdate { get; }
        public ICommand cFilterStart { get; }
        public ICommand cFilterStop { get; }

        //Конструктор
        public VM_Unit(mSettings Setting)
        {
            this._setting = Setting;
            ProcCommand = null;
            ControlTabble = SqlService.UnitSQL.GetUnitArt(_setting.user.Id);
            TableValue = ControlTabble;
            typeTovars = new List<TypeTovar>();
            typeTovars.Add(new TypeTovar());
            typeTovars.AddRange(SqlService.DirectorySQL.GetTypeTovar());
            TypeTovar();


            cUpdate = new RelayCommand(_ =>
            {
                Update();
            });
            cFilterStart = new RelayCommand(_ =>
            {
                StartFilter();
            });
            cFilterStop = new RelayCommand(_ =>
            {
                StopFilter();
            });
        }

        private void StopFilter()
        {
            TableValue = ControlTabble;
        }

        //Методы
        #region [Таблицы и Данные]
        private void TypeTovar ()
        {
            foreach(var item in TableValue)
            {
                item.Article.TypeTovar= typeTovars.Find(x=>x.Id==item.Article.TypeTovarID);
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
            ControlTabble = SqlService.UnitSQL.GetUnitArt(_setting.user.Id);
            TableValue = ControlTabble;
            TypeTovar();

        }
        #endregion

        #region [Процедуры и фильтрации]
        public void StartFilter()
        {
            Update();
            TableValue = ControlTabble;
            if (SelectProcCommand <0) return;

            if (SelectType == 0) SelectType = -1;

            switch(SelectProcCommand)
            {
                case 0:
                    {
                        TableValue = SqlService.Procedure.ResultProcedure(SQLprocedure.BEST_ART, _setting.user.Id, SelectType);
                        break;
                    }
                case 1:
                    {
                        TableValue = SqlService.Procedure.ResultProcedure(SQLprocedure.WORST_ART, _setting.user.Id, SelectType);
                        break;
                    }
                case 2:
                    {
                        TableValue = SqlService.Procedure.ResultProcedure(SQLprocedure.MAXPRICE, _setting.user.Id, SelectType);
                        break;
                    }
                case 3:
                    {
                        TableValue = SqlService.Procedure.ResultProcedure(SQLprocedure.MINPRICE, _setting.user.Id, SelectType);
                        break;
                    }
                case 4:
                    {
                        TableValue = SqlService.Procedure.ResultProcedure(SQLprocedure.HIGH_EXPENSE_ART,_setting.user.Id, SelectType);
                        break;
                    }
                case 5:
                    {
                        TableValue = SqlService.Procedure.ResultProcedure(SQLprocedure.LOW_EXPENSE_ART, _setting.user.Id, SelectType);
                        break;
                    }
            }
        }
        #endregion

        //Интерфейс
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
