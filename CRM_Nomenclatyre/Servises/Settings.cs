using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CRM_Nomenclatyre.Models;

namespace CRM_Nomenclatyre.Servises
{
    public class Settings
    {
        
        private static Settings _instance;

        public readonly MessageeServise serviseMessege;
        public readonly WindowService serviseWindow;

        public string Password { get; set; }

        public Users user;



        private Settings(MessageeServise serviseMessege,WindowService serviseWindow,Users user) 
        {
            this.serviseMessege = serviseMessege;
            this.serviseWindow = serviseWindow;
            this.user = user;
        }

        public static Settings Initialize(MessageeServise serviseMessege, WindowService serviseWindow, Users user)
        {
            if (_instance == null)
            {
                _instance = new Settings(serviseMessege, serviseWindow, user);
            }

            return _instance;
        }



    }

}
