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
using CRM_Nomenclatyre.Servises;

namespace CRM_Nomenclatyre.Pages
{
    /// <summary>
    /// Логика взаимодействия для ArticulsPage.xaml
    /// </summary>
    public partial class ArticulsPage : Page
    {
        public ArticulsPage()
        {
            InitializeComponent();
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            Commit();
        }


        private void Commit()
        {
            dg_artList.CommitEdit(DataGridEditingUnit.Cell, true);
            dg_artList.CommitEdit(DataGridEditingUnit.Row, true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Commit();
        }
    }
}
