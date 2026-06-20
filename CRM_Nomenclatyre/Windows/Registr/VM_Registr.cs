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
        private mSettings _setting { get; set; }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
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
                _lastName = value;
                OnPropertyChanged();
            }
        }


        //Команды

        public ICommand cRegistration {  get;}

        //Коснтруктор
        public VM_Registr(mSettings _setting)
        { 
            this._setting = _setting;

            cRegistration = new RelayCommand(_ =>
            {
                Regist();
            });
        }
        //Методы
        public void Regist()
        {
           Users userNow = AddUser();

            if (Logging.IsRegist(userNow))
            {
                _setting.serviseMessege.Show("Регистрация прошла успешно!", "Регистрация");

            }
            else _setting.serviseMessege.Show("Пользователь уже существует!", "Регистрация");

        }

        public Users AddUser()
        {

            if(IsEmpty(Login) || IsEmpty(Password) ||
               IsEmpty(FirstName)|| IsEmpty(LastName)) return null;

            Users _user = new Users
            {
                Login = this.Login,
                Password = this.Password, 
                Manager= new Managers
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                }
            };

            return _user;
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
