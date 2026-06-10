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

namespace CRM_Nomenclatyre.Pages
{

    public class VM_Articuls:INotifyPropertyChanged
    {   
        //Свойства 
        private Settings Setting { get; set; }
        public List<Articles> ListArticules { get; set; } = new List<Articles>();
        public DataSet ListData {  get; set; } = new DataSet();
       
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
        public ICommand AddTovar { get; }
        //Коснтруктор
        public VM_Articuls(Settings Setting)
        {
            this.Setting = Setting;
            ListData = SqlService.LoadBD("tab_Article");
            ListArticules = SqlService.SQL_Article.GetArticlesOn(Setting.user.Id);
            
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
