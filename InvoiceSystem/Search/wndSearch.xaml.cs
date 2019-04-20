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
using System.Reflection;

namespace InvoiceSystem.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : UserControl
    {
        #region Attributes
        //attributes
        /// <summary>
        /// this is the object for the clsSearchLogic class
        /// </summary>
        clsSearchLogic logic;
        /// <summary>
        /// this is the boolean that checks that a search filter was selected
        /// </summary>
        bool startsearch;
        /// <summary>
        /// this boolean checks to see if an invoice was selected 
        /// </summary>
        bool selected;
        #endregion

        #region Fields
        //Fields
        private readonly ViewNavigationController viewNavigationController;
        public wndSearch(ViewNavigationController viewNavigationController)
        {
            try
            {
                InitializeComponent();
                this.viewNavigationController = viewNavigationController;

                logic = new clsSearchLogic();
                startsearch = false;
                selected = false;
                logic.getInvoiceNums();
                logic.getTotalCosts();
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void ReturnToMainButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new wndMain(viewNavigationController));
        }

        /// <summary>
        ///this loads the page with all the information it needs 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                InvoiceNumbersBox.ItemsSource = logic.InvoiceNumbers;
                TotalCostsBox.ItemsSource = logic.InvoiceTotalCosts;
                InvoiceDataGrid.ItemsSource = logic.GetInvoices();
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Actions

        /// <summary>
        /// this takes the selected invoice from the datagrid and opens up the modify invoice with the 
        /// selected invoice 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selected == true)
                {
                    viewNavigationController.ChangeCurrentView(new ModifyInvoice(this.viewNavigationController,
                        new Invoice(logic.SelectedInvoiceNum,logic.SelectedInvoiceDate,logic.SelectedTotalCost)));
                    //the selected invoice number , the date and the total cost go into the invoice field 
                    selected = false;
                }

                else
                {
                    MessageBox.Show("Please select an invoice you would like to modify", "Alert"
                        , MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// set the window back to its default state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //resets for a new search 
                logic.resetSearch();
                //set the datagrid back to how it was when the window first opens
                InvoiceDataGrid.ItemsSource = logic.GetInvoices();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// this button checks to see if a value was selected and does the search accroding to the values selected 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
               
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// gets the item selected from the InvoiceNumbersBox and sets the item to the clsSearchLogic.getNumber Property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceNumbersBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {

                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// gets the item selected from the TotalCostsBox and sets the item selected to the clsSearchLogic 
        /// getCosts property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalCostsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {

                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// this sets the date for the search 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime? date = InvoiceDate.SelectedDate;
                if (date.HasValue)
                {
                    logic.getDate = date.Value.ToShortDateString();
                    startsearch = true;
                }
            }
            catch (Exception ex)
            {

                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// this sets the invoice from the datagrid to be selected and sets it to be passed on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var cellInfo = InvoiceDataGrid.SelectedCells[0]; //gets the selected item at the first cell of the datagrid 
                logic.SelectedInvoiceNum = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
                var cellInfo2 = InvoiceDataGrid.SelectedCells[1]; //gets the selected item at the second cell of the datagrid 
                logic.SelectedInvoiceDate = (cellInfo2.Column.GetCellContent(cellInfo2.Item) as TextBlock).Text;
                var cellInfo3 = InvoiceDataGrid.SelectedCells[2]; //gets the selected item at the third cell of the datagrid 
                logic.SelectedTotalCost = (cellInfo3.Column.GetCellContent(cellInfo3.Item) as TextBlock).Text;

                selected = true;
            }
            catch (Exception ex)
            {

                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }
        #endregion
    }

}
