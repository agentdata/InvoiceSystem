using InvoiceSystem.Main;
using InvoiceSystem.OtherClasses;
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

namespace InvoiceSystem.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : UserControl
    {
        //attributes
        clsSearchSQL Sql;
        clsSearchLogic logic;

        //Fields
        private readonly ViewNavigationController viewNavigationController;

        public wndSearch(ViewNavigationController viewNavigationController)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            Sql = new clsSearchSQL();
            logic = new clsSearchLogic();
        }

        private void ReturnToMainButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new wndMain(viewNavigationController));
        }

        public void setDate()
        {
            // not sure if this is correct
            logic.getDate = Date.SelectedDate.ToString();
        }

        //loads the Invoice info from the DataBase
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            InvoiceNumbersBox.ItemsSource = Sql.getInvoiceNums();
            TotalCostsBox.ItemsSource = Sql.getTotalCosts();
            InvoiceDataGrid.ItemsSource = Sql.GetInvoices();
        }

        private void SelectInvoiceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// set the window back to its default state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            logic.getCosts = "";
            logic.getDate = "";//set to a default date
            logic.getNumber = "";
            //set the datagrid back to how it was when the window first opens
            InvoiceDataGrid.ItemsSource = Sql.GetInvoices();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (logic.SearchInvoiceNum() == true)
            {
                InvoiceDataGrid.ItemsSource = Sql.SearchInvoiceNumbers(logic.getNumber);

            }
        }


        /// <summary>
        /// gets the item selected from the InvoiceNumbersBox and sets the item to the clsSearchLogic.getNumber Property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceNumbersBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            logic.getNumber = InvoiceNumbersBox.Items.GetItemAt(InvoiceNumbersBox.SelectedIndex).ToString();
        }

        /// <summary>
        /// gets the item selected from the TotalCostsBox and sets the item selected to the clsSearchLogic 
        /// getCosts property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalCostsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            logic.getCosts = TotalCostsBox.Items.GetItemAt(TotalCostsBox.SelectedIndex).ToString();
        }
    }

}
}