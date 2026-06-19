using CRM_Nomenclatyre.Models;
using CRM_Nomenclatyre.Servises;
using CRM_Nomenclatyre.Windows.Registr;
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
                 Setting.serviseWindow.WindowOpen<RegistrWindow>(new VM_Registr(Setting));
            }) ;
            cLogin = new RelayCommand(_ =>
            {
                exLogin();
            });

        }


        //Методы
        
        private void exLogin()
        {
            OnPropertyChanged("Login");
            OnPropertyChanged("Password");

            Users userNow = new Users
            {
                Login = _login,
                Password = _password
            };

            if (Logging.IsLogin(userNow))
            {
                Setting.serviseMessege.Show("Вход успешный", "Вход");
                VM_Main main = VM_Main.Initialize(Setting);
                Setting.serviseWindow.WindowOpenAndClose<MainWindow>(main);
            } 
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
