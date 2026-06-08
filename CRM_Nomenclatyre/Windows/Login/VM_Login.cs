using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CRM_Nomenclatyre.Models;
using CRM_Nomenclatyre.Servises;

namespace CRM_Nomenclatyre.Windows
{
    public class VM_Login : INotifyPropertyChanged
    {

        //Свойства 
        private Settings Setting{ get; set; }
        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                if (_login == value || value == null) return;
                _login= value;
                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (_password == value || value == null) return;
                _password = value;
                OnPropertyChanged();
            }
        }

        //Команды
        public ICommand cRegistration { get; }
        public ICommand cLogin { get; }

        //Коснтруктор
        public VM_Login(Settings setting)
        {
            this.Setting = setting;
            cRegistration = new RelayCommand(_=>
             {

            }) ;
            cLogin = new RelayCommand(_ =>
            {
                exLogin();
            });
        }


        //Методы
        
        private void exLogin()
        {
            Logging log = new Logging();
            OnPropertyChanged("Login");
            OnPropertyChanged("Password");

            Users userbuf = new Users
            {
                Login = _login,
                Password = _password
            };

            if (log.IsLogin(userbuf,out Setting.user)) 
                Setting.serviseMessege.Show("Вход успешный", "Вход");
            else Setting.serviseMessege.Show("Не верный логин или пароль", "Вход");
        }


        //Интерфейс
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
