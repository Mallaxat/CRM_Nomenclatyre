using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CRM_Nomenclatyre.Servises;
using CRM_Nomenclatyre.Models;

namespace CRM_Nomenclatyre.Windows.Registr
{
    public class VM_Registr : INotifyPropertyChanged
    {

        //Свойства
        private Settings Setting { get; set; }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                if (value == null || _login == null) return;
                _login = value;
                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (value == null || _password == null) return;
                _password = value;
                OnPropertyChanged();
            }
        }
        
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == null || _firstName == null) return;
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == null || _lastName == null) return;
                _lastName = value;
                OnPropertyChanged();
            }
        }


        //Команды

        public ICommand cAddUser {  get;}


        //Коснтруктор
        public VM_Registr(Settings setting)
        { 
            Logging log=new Logging();
            this.Setting = setting;
            cAddUser = new RelayCommand(_ =>
            {
                log.Regist(AddUser());
            });
        }
        //Методы
        public Users AddUser()
        {
            if(IsEmpty(Login) || IsEmpty(Password) ||
               IsEmpty(FirstName)|| IsEmpty(LastName)) return null;


            Managers men = new Managers
            {
                FirstName = this.FirstName,
                LastName = this.LastName

            };
            Users us = new Users
            {
                Login = this.Login,
                Password = this.Password,
                Manager = men
            };
            return us;
        }

         private bool IsEmpty<T>(T value)
        {
            return value == null;
        }




        //Интерфейс
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
