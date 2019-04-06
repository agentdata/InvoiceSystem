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
        bool startsearch;

        //Fields
        private readonly ViewNavigationController viewNavigationController;

        public wndSearch(ViewNavigationController viewNavigationController)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            Sql = new clsSearchSQL();
            logic = new clsSearchLogic();
            startsearch = false;
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
            if (startsearch == true)
            {
                //invoice number searches
                if (logic.SearchInvoiceNum() == true)
                {
                    InvoiceDataGrid.ItemsSource = Sql.SearchInvoiceNumbers(logic.getNumber);
                    //reset the search for new search 
                    logic.resetSearch();
                    startsearch = false;
                }
                //search invoice numbers and invoice date
                else if (logic.SearchInvoiceNum() == true && logic.SearchDate() == true)
                {
                    InvoiceDataGrid.ItemsSource = Sql.SearchNumber_Date(logic.getNumber, logic.getDate);
                    //reset the search
                    logic.resetSearch();
                    startsearch = false;
                }

                //search invoice number, invoice date and the total costs
                else if (logic.SearchInvoiceNum() == true && logic.SearchDate() == true && logic.SearchTotalCosts() == true)
                {
                    InvoiceDataGrid.ItemsSource = Sql.SearchAll(logic.getNumber, logic.getDate, logic.getCosts);
                    //reset the search
                    logic.resetSearch();
                    startsearch = false;
                }

                //search the total costs 
                else if (logic.SearchTotalCosts() == true)
                {
                    InvoiceDataGrid.ItemsSource = Sql.SearchTotalCosts(logic.getCosts);
                    //reset the search
                    logic.resetSearch();
                    startsearch = false;
                }

                //search date and total costs
                else if (logic.SearchTotalCosts() == true && logic.SearchDate() == true)
                {
                    InvoiceDataGrid.ItemsSource = Sql.SearchDate_Cost(logic.getDate, logic.getCosts);
                    //reset the search
                    logic.resetSearch();
                    startsearch = false;
                }

                //search the date 
                else if (logic.SearchDate() == true)
                {
                    InvoiceDataGrid.ItemsSource = Sql.SearchInvoiceDates(logic.getDate);
                    //reset the search
                    logic.resetSearch();
                    startsearch = false;
                }

            }

            //show a messagebox if startsearch if false
            else
            {
                MessageBox.Show("Please choose a search filter to start the search", "Alert"
                    , MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            startsearch = true;
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
            startsearch = true;
        }
    }

}
