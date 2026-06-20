using CRM_Nomenclatyre.Models;
using CRM_Nomenclatyre.Servises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRM_Nomenclatyre.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        VM_Login _vm;
        mSettings _setting;

        public LoginWindow()
        {
            InitializeComponent();
            _setting = mSettings.Initialize(new MessageeServise(this), new WindowService(), new Users());
            _vm = new VM_Login(_setting);
            DataContext = _vm;

        }

        private void bt_login_Click(object sender, RoutedEventArgs e)
        {
            _vm.Password=text_Password.Password;
        }
    }
}
