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

namespace wndSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        clsSearchSQL Sql;
        InvoiceData invoice;

        public MainWindow()
        {
            InitializeComponent();
            Sql = new clsSearchSQL();
        }

        //loads the Invoice info from the DataBase
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
          
            InvoiceNumbersBox.ItemsSource = Sql.getInvoiceNums();
            TotalCostsBox.ItemsSource = Sql.getTotalCosts();
            //pass in collection into the datagrid?
            //how to you pass in the parts of the list into it 
            InvoiceDataGrid.ItemsSource = Sql.GetInvoices();
        }

        private void SelectInvoiceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
