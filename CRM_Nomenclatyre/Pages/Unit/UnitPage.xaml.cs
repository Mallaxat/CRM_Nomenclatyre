using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM_Nomenclatyre.Pages
{
    /// <summary>
    /// Логика взаимодействия для UnitPage.xaml
    /// </summary>
    public partial class UnitPage : Page
    {
        public UnitPage()
        {
            InitializeComponent();
        }

        private void bt_filtrEnd_Click(object sender, RoutedEventArgs e)
        {
            bt_update.IsEnabled = true;
            dg_Unit.IsReadOnly = false;
        }

        private void bt_filtr_Click(object sender, RoutedEventArgs e)
        {
            bt_update.IsEnabled = false;
            dg_Unit.IsReadOnly = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox.SelectedIndex == 9 || comboBox.SelectedIndex == 10)
            {
                tb_nums.Visibility = Visibility.Visible;
            }
            else
            {
                tb_nums.Visibility = Visibility.Hidden;
            }


        }
    }
}
