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
        
        clsSearchLogic logic;
        bool startsearch;

        //Fields
        private readonly ViewNavigationController viewNavigationController;

        public wndSearch(ViewNavigationController viewNavigationController)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
        
            logic = new clsSearchLogic();
            startsearch = false;

            logic.getInvoiceNums();
            logic.getTotalCosts();
        }

        private void ReturnToMainButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new wndMain(viewNavigationController));
        }

        //loads the Invoice info from the DataBase
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            InvoiceNumbersBox.ItemsSource = logic.InvoiceNumbers;
            TotalCostsBox.ItemsSource = logic.InvoiceTotalCosts;
            InvoiceDataGrid.ItemsSource = logic.GetInvoices();
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
            //resets for a new search 
            logic.resetSearch();  
            //set the datagrid back to how it was when the window first opens
            InvoiceDataGrid.ItemsSource = logic.GetInvoices();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (startsearch == true)
            {

                //search invoice number, invoice date and the total costs
                if (logic.SearchInvoiceNum() == true && logic.SearchDate() == true && logic.SearchTotalCosts() == true)
                {
                    InvoiceDataGrid.ItemsSource = logic.SearchAll();
                    //reset the search
                    logic.resetSearch();
                    startsearch = false;
                }

                //search invoice numbers and invoice date
                else if (logic.SearchInvoiceNum() == true && logic.SearchDate() == true)
                {
                    InvoiceDataGrid.ItemsSource = logic.SearchNumber_Date();
                    //reset the search
                    logic.resetSearch();
                    startsearch = false;
                }

                //search invoice number and total costs
                else if (logic.SearchInvoiceNum() == true && logic.SearchTotalCosts() == true)
                {
                    InvoiceDataGrid.ItemsSource = logic.SearchNumber_Cost();
                    //reset the search
                    logic.resetSearch();
                    startsearch = false;

                }


                //search date and total costs
                else if (logic.SearchTotalCosts() == true && logic.SearchDate() == true)
                {
                    InvoiceDataGrid.ItemsSource = logic.SearchDate_Cost();
                    //reset the search
                    logic.resetSearch();
                    startsearch = false;
                }

                //lets start here
                //invoice number searches
                else if (logic.SearchInvoiceNum() == true)
                {

                    InvoiceDataGrid.ItemsSource = logic.SearchInvoiceNumbers();
                    //reset the search for new search 
                    logic.resetSearch();
                    startsearch = false;
                }
                //search the date 
                else if (logic.SearchDate() == true)
                {
                    InvoiceDataGrid.ItemsSource = logic.SearchInvoiceDates();
                    //reset the search
                    logic.resetSearch();
                    startsearch = false;
                }

                //search the total costs 
                else if (logic.SearchTotalCosts() == true)
                {
                    InvoiceDataGrid.ItemsSource = logic.SearchTotalCost();
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
            InvoiceInfo info;

            for (int i = 0; i < InvoiceNumbersBox.Items.Count; i++)
            {
                info = (InvoiceInfo)InvoiceNumbersBox.Items[i];
                if (InvoiceNumbersBox.SelectedValue.ToString() == info.InvoiceNumber)
                {
                    logic.getNumber = info.InvoiceNumber;
                }
            }

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
            InvoiceInfo info;

            for (int i = 0; i < InvoiceNumbersBox.Items.Count; i++)
            {
                info = (InvoiceInfo)TotalCostsBox.Items[i];
                if (TotalCostsBox.SelectedValue.ToString() == info.TotalCosts)
                {
                    logic.getCosts = info.TotalCosts;
                }
            }

            startsearch = true;
        }

        /// <summary>
        /// this sets the date for the search 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? date = InvoiceDate.SelectedDate;
            if (date.HasValue)
            {
                logic.getDate = date.Value.ToShortDateString();
                startsearch = true;
            }
        }
    }

}
