using CRM_Nomenclatyre.Models;
using CRM_Nomenclatyre.Servises;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CRM_Nomenclatyre.Pages.Articuls.CreateWindow
{
    public class VM_CreateTovar:INotifyPropertyChanged
    {

        //Свойства 
        //Свойства
        private Settings Settings {  get; set; }
        private string _named;
        public string Named
        {
            get => _named;
            set
            {
                _named = value;
                OnPropertyChanged();
            }
        }

        private string _sort;
        public string Sort
        {
            get => _sort;
            set
            {
                _sort = value;
                OnPropertyChanged();
            }
        }

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }

        private string _size;
        public string Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged();
            }
        }

        private string _barcod;
        public string Barcod
        {
            get => _barcod;
            set
            {
                _barcod = value;
                OnPropertyChanged();
            }
        }

        private string _articul;
        public string Articul
        {
            get => _articul;
            set
            {
                _articul = value;
                OnPropertyChanged();
            }
        }

        //Команды  
        public ICommand AddTovar { get; }

        //Коснтруктор
        public VM_CreateTovar(Settings Settings)
        {
            this.Settings =  Settings;
            AddTovar = new RelayCommand(_ =>
            {
                CreateTovar();
            });
        }
        //Методы
        private Articles CreateTovar()
        {
            Articles result = new Articles
            {
                Named = this.Named,
                Sort=this.Sort,
                ManagerId=Settings.user.Id,
                Count=this.Count,
                Size=this.Size,
                Barcod=CreateNums(12,0),
                Articul=CreateNums(5,1),
            };
            Settings.user.Manager.articles.Add(result);
            return result;
        }
       
        private string CreateNums(int count,int var=0)
        {
            string res = String.Empty;
            Random rand = new Random();
            while(true)
            {
                for (int i = 0; i < count; i++)
                {
                    res += rand.Next(0, 9).ToString();
                }
        
                if(var == 0)
                {
                    if (!SqlService.SQL_Article.FindBar(res)) return res;
                }
                else
                {
                    if (!SqlService.SQL_Article.FindArticule(res)) return res;
                }
            }
        }


        //Интерфейс
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
