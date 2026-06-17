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

namespace CRM_Nomenclatyre.Windows
{
    public class VM_Main : INotifyPropertyChanged
    {


        //Свойства
        private static VM_Main Instanse {  get; set; }
        private Settings Setting { get; set; }
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

        //Коснтруктор
        private VM_Main(Settings Setting) 
        { 
            this.Setting = Setting;
            CurrentPage = Setting.serviseWindow.PageOpen<VM_Articuls, ArticulsPage>(Setting);
        }

        //Методы
        public static VM_Main Initialize(Settings Setting)
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
