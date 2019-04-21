using InvoiceSystem.Classes;
using InvoiceSystem.OtherClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace InvoiceSystem.Main
{
    /// <summary>
    /// Interaction logic for NewInvoice.xaml
    /// </summary>
    public partial class NewInvoice : UserControl, INotifyPropertyChanged
    {
        #region Fields
        private readonly ViewNavigationController viewNavigationController;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Fields

        #region Properties
        public ObservableCollection<Item> AvailableItems { get; set; }
        public LineItems _shoppingCart = new LineItems();
        public LineItems shoppingCart {
            get { return _shoppingCart; }
            set
            {
                if (value != _shoppingCart)
                {
                    _shoppingCart = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(runningTotal)));
                }
            }
        }
        //The shopping cart total.
        private int _runningTotal = 0;
        public int runningTotal 
        {
            get { return _runningTotal; }
            set
            {
                if (value != _runningTotal)
                {
                    _runningTotal = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(runningTotal)));
                }
            }
        }
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor for this user control, takes the viewNavigationController which is displayed in the contentpresenter in the MainWindow.
        /// </summary>
        /// <param name="viewNavigationController"></param>
        public NewInvoice(ViewNavigationController viewNavigationController)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            //get a list of current items from the database
            AvailableItems = clsMainLogic.GetAllItems();
            DataContext = this;
        }
        #endregion Constructors

        #region UI Actions
        /// <summary>
        /// This function is called when the button labeled Submit is clicked.
        /// A new invoice is generated if there is more than 1 lineitem in the shopping cart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Button(object sender, RoutedEventArgs e)
        {
            //Call the method to create a new invoice with the selected items, current time and running total.
            clsMainLogic.submitNewInvoice(new Invoice(DateTime.Now.ToString(), runningTotal.ToString(), shoppingCart));
            
            //Return to main screen
            viewNavigationController.ChangeCurrentView(new wndMain(viewNavigationController));
        }

        /// <summary>
        /// This button action resets the view, loses reference to this and then opens a new view of NewInvoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Button(object sender, RoutedEventArgs e)
        {
            //Return reset the screen screen
            viewNavigationController.ChangeCurrentView(new NewInvoice(viewNavigationController));
        }

        /// <summary>
        /// This function is called when an item in the grid is double clicked, it adds the selected
        /// item to the shoppingCart. it either adds a new lineitem to the shopping cart or increases 
        /// the existing line items quantity.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            clsMainLogic.addItemToCart(sender, ref _shoppingCart, ref _runningTotal);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(runningTotal)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(shoppingCart)));
        }

        private IEnumerable<DataGridRow> GetDataGridRowsForButtons(DataGrid grid)
        { //IQueryable 
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row & row.IsSelected) yield return row;
            }
        }

        private void IncreaseQuantityByOneButton_Action(object sender, RoutedEventArgs e)
        {

            LineItem selectedLineItem = null;

            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    // var row = (DataGrid)vis;

                    var rows = GetDataGridRowsForButtons(SelectedItemsDataGrid_DataGrid);
                    string id;
                    foreach (DataGridRow dr in rows)
                    {
                        selectedLineItem = (dr.Item as LineItem);

                        break;
                    }
                    break;
                }
            if (selectedLineItem != null)
            {
                shoppingCart.lineItems.Where(x => x.Item.ItemCode == selectedLineItem.Item.ItemCode).FirstOrDefault().Quantity++;
                runningTotal += Int32.Parse(selectedLineItem.Item.Cost);
            }
        }

        private void DecreaseQuantityByOneButton_Action(object sender, RoutedEventArgs e)
        {
            LineItem selectedLineItem = null;

            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    // var row = (DataGrid)vis;

                    var rows = GetDataGridRowsForButtons(SelectedItemsDataGrid_DataGrid);
                    foreach (DataGridRow dr in rows)
                    {
                        selectedLineItem = (dr.Item as LineItem);
                        break;
                    }
                    break;
                }
            if (selectedLineItem != null)
            {
                if (shoppingCart.lineItems.Where(x => x.Item.ItemCode == selectedLineItem.Item.ItemCode).FirstOrDefault().Quantity == 1)
                {
                    if (MessageBox.Show("are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        shoppingCart.lineItems.Remove(shoppingCart.lineItems.Where(x => x.Item.ItemCode == selectedLineItem.Item.ItemCode).FirstOrDefault());
                    }
                }
                else shoppingCart.lineItems.Where(x => x.Item.ItemCode == selectedLineItem.Item.ItemCode).FirstOrDefault().Quantity--;
                runningTotal -= Int32.Parse(selectedLineItem.Item.Cost);
            }
        }

        private void DeleteLineItemButton_Action(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("are you sure you want to delete this item?","Confirm Deletion",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                LineItem selectedLineItem = null;

                for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                    if (vis is DataGridRow)
                    {
                        // var row = (DataGrid)vis;

                        var rows = GetDataGridRowsForButtons(SelectedItemsDataGrid_DataGrid);
                        foreach (DataGridRow dr in rows)
                        {
                            selectedLineItem = (dr.Item as LineItem);
                            break;
                        }
                        break;
                    }
                if (selectedLineItem != null)
                {
                    shoppingCart.lineItems.Remove(shoppingCart.lineItems.Where(x => x.Item.ItemCode == selectedLineItem.Item.ItemCode).FirstOrDefault());
                    runningTotal -= Int32.Parse(selectedLineItem.Item.Cost)*selectedLineItem.Quantity;
                }
            }
        }

        #endregion UI Actions
    }
}