using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Nomenclatyre.Models;

namespace CRM_Nomenclatyre.Servises
{
    public class Settings
    {
        public readonly MessageeServise serviseMessege;
        public readonly WindowService serviseWindow;

        public string Password { get; set; }

        public Users user;

        public Settings(MessageeServise serviseMessege, WindowService serviseWindow, Users user)
        {
            this.serviseMessege = serviseMessege;
            this.serviseWindow = serviseWindow;
            this.user = user;
            if (user == null) user = new Users();
        }

    }

}
