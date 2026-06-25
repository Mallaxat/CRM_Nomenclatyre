using CRM_Nomenclatyre.Models;
using CRM_Nomenclatyre.Pages;
using CRM_Nomenclatyre.Servises;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CRM_Nomenclatyre.Windows
{
    public class VM_Main : INotifyPropertyChanged
    {


        //Свойства
        private static VM_Main Instanse {  get; set; }
        private mSettings _setting { get; set; }
        private  Page сurrentPage;
        public Page CurrentPage
        {
            get => сurrentPage;
            set
            {
                if (value == null) return;
                сurrentPage = value;
                OnPropertyChanged();
            }
        }


        //Команды
        public ICommand cPageArt { get; }
        public ICommand cPageUnitArt { get; }

        //Коснтруктор
        private VM_Main(mSettings _setting) 
        { 
            this._setting = _setting;
            CurrentPage = _setting.serviseWindow.PageOpen<VM_Articuls, ArticulsPage>(_setting);
            cPageArt = new RelayCommand(_ =>
            {
                //VM_Main main = VM_Main.Initialize(_setting);
                //CurrentPage=_setting.serviseWindow.PageOpen<VM_Unit, UnitPage>(_setting);
                CurrentPage = _setting.serviseWindow.PageOpen<VM_Articuls, ArticulsPage>(_setting);
            });
            cPageUnitArt = new RelayCommand(_ =>
            {
                //VM_Main main = VM_Main.Initialize(_setting);
                CurrentPage =_setting.serviseWindow.PageOpen<VM_Unit, UnitPage>(_setting);       
            });
        }

        //Методы
        public static VM_Main Initialize(mSettings Setting)
        {
            if (Instanse == null) Instanse = new VM_Main(Setting);
            return Instanse;
        }

        public void UpdatePage<T>(T page) where T : Page
        {
            CurrentPage= page;
            OnPropertyChanged("CurrentPage");
        }



        //Интерфейс
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
