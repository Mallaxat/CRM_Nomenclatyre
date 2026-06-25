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
        private mSettings _setting { get; set; }
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
        public VM_Login(mSettings setting)
        {
            this._setting = setting;
            cRegistration = new RelayCommand(_=>
             {
             _setting.serviseWindow.WindowOpen<RegistrWindow>(new VM_Registr(_setting));
            }) ;
            cLogin = new RelayCommand(_ =>
            {
                exLogin();
            });

        }

        //Методы
        
        private void exLogin()
        { 
            Users userNow = new Users
            {
                Login = this.Login,
                Password =this.Password
            };

            bool check = Logging.IsLogin(userNow);

            if (Logging.IsLogin(userNow))
            {
                userNow = SqlService.UserSQL.GetFullUser(userNow);
                _setting.user= userNow;

                _setting.serviseMessege.Show("Вход успешный", "Вход");
                VM_Main main = VM_Main.Initialize(_setting);   
                _setting.serviseWindow.WindowOpenAndClose<MainWindow>(main);
            } 
            else _setting.serviseMessege.Show("Не верный логин или пароль", "Вход");
        }


        //Интерфейс
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }


    }
}
