using CRM_Nomenclatyre.Servises;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Nomenclatyre.Pages
{ 
    public class VM_Unit : INotifyPropertyChanged
    {
        //Свойства
        public Settings Setting {  get; set; }
        //Команда

        //Конструктор
        public VM_Unit(Settings Setting)
        {
            this.Setting = Setting;

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
