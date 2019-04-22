using InvoiceSystem.Classes;
using InvoiceSystem.Items;
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
    /// Interaction logic for AddNewInvoice.xaml
    /// </summary>
    public partial class ModifyInvoice : UserControl, INotifyPropertyChanged
    {
        #region Properties
        public ObservableCollection<Item> AvailableItems { get; set; }
        private Invoice _CurrentInvoice;
        private string v;

        public Invoice CurrentInvoice
        {
            get { return _CurrentInvoice; }
            set
            {
                if (value != _CurrentInvoice)
                {
                    _CurrentInvoice = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentInvoice)));
                }
            }
        }
        #endregion Properties

        #region Fields

        // Navigation controller which is passed around so that the view can be updated.
        private readonly ViewNavigationController viewNavigationController;
        // Event to be raised whena property is modified so the view stays up to date.
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Fields

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewNavigationController"></param>
        /// <param name="invoice"></param>
        public ModifyInvoice(ViewNavigationController viewNavigationController, Invoice invoice)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            CurrentInvoice = invoice;
            AvailableItems = clsMainLogic.GetAllItems();
            DataContext = this;
        }

        /// <summary>
        /// This constructur takes in the invoice and string command,
        /// if command is getinvoiceitems then it will query and get the items for this specific invoice and fill it in to the invoice object.
        /// This is called from the search window.
        /// </summary>
        /// <param name="viewNavigationController"></param>
        /// <param name="invoice"></param>
        /// <param name="Command"></param>
        public ModifyInvoice(ViewNavigationController viewNavigationController, Invoice invoice, string Command)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            CurrentInvoice = invoice;
            AvailableItems = clsMainLogic.GetAllItems();
            DataContext = this;
            if (Command == "GetInvoiceItems")
            {
                //gets all the line items and adds them to current invoice
                clsMainLogic.getLineItems(ref _CurrentInvoice);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentInvoice)));
            }
            
        }
        #endregion Constructors

        #region UI Actions
        /// <summary>
        /// This is the submit button which will modify the invoice, really the only thing that can be modified in this setup is the quantity of item
        /// so this button action updates the quantity of item in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Button(object sender, RoutedEventArgs e)
        {
            //clsMainLogic.addNewLineItems();
            
            //update the invoice total cost.
            clsMainLogic.updateInvoiceTotalCost(CurrentInvoice);
            viewNavigationController.ChangeCurrentView(new wndMain(viewNavigationController));
        }

        /// <summary>
        /// This method is called when an item in the available items datagrid is double clicked.
        /// It adds a new line item to the invoice, or adds 1 if the item already exists in the current invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if(CurrentInvoice.LineItems.Where())
            DataGridRow row = sender as DataGridRow;
            Item selectedItem = (row.Item as Item);
            if (selectedItem != null)
            {
                bool AlreadyInInvoice = false;
                foreach (LineItem LineItem in CurrentInvoice.LineItems.lineItems)
                {
                    //if currentinvoice contains item add +1 to the quantity
                    if (LineItem.Item.ItemCode == selectedItem.ItemCode)
                    {
                        AlreadyInInvoice = true;
                        var newLineItem = LineItem;
                        clsMainLogic.updateLineItemQuantity(ref _CurrentInvoice, ref newLineItem, "increase");
                    }
                }

                if (!AlreadyInInvoice)
                {
                    clsMainLogic.AddNewLineItemToExistingInvoice(ref _CurrentInvoice, selectedItem, 1);
                    CurrentInvoice.addItem(selectedItem, 1);
                }
                _CurrentInvoice.updateTotalCost();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentInvoice)));
            }
        }
        #endregion UI Actions
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

        /// <summary>
        /// the increasequantity button that shows up in the grid.
        /// it adds 1 quantity to the row item from the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IncreaseQuantityByOneButton_Action(object sender, RoutedEventArgs e)
        {

            LineItem selectedLineItem = null;

            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var rows = GetDataGridRowsForButtons(existingLineItem_DataGrid);
                    foreach (DataGridRow dr in rows)
                    {
                        selectedLineItem = (dr.Item as LineItem);
                        
                        break;
                    }
                    break;
                }
            if (selectedLineItem != null)
            {
                clsMainLogic.updateLineItemQuantity(ref _CurrentInvoice, ref selectedLineItem, "increase");
                _CurrentInvoice.updateTotalCost();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentInvoice)));
            }
        }

        /// <summary>
        /// the decrease quantity button that shows up in the grid.
        /// it decreases the row item quantity from the invoice, if the quantity is at 1 then it deletes the row item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecreaseQuantityByOneButton_Action(object sender, RoutedEventArgs e)
        {
            LineItem selectedLineItem = null;
            
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    // var row = (DataGrid)vis;

                    var rows = GetDataGridRowsForButtons(existingLineItem_DataGrid);
                    foreach (DataGridRow dr in rows)
                    {
                        selectedLineItem = (dr.Item as LineItem);
                        break;
                    }
                    break;
                }
            if (selectedLineItem != null)
            {
                clsMainLogic.updateLineItemQuantity(ref _CurrentInvoice, ref selectedLineItem, "decrease");
                _CurrentInvoice.updateTotalCost();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentInvoice)));
            }
        }

        /// <summary>
        /// the deletelineitembutton that shows up in the grid.
        /// it deletes the row item from the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteLineItemButton_Action(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                LineItem selectedLineItem = null;

                for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                    if (vis is DataGridRow)
                    {
                        // var row = (DataGrid)vis;

                        var rows = GetDataGridRowsForButtons(existingLineItem_DataGrid);
                        foreach (DataGridRow dr in rows)
                        {
                            selectedLineItem = (dr.Item as LineItem);
                            break;
                        }
                        break;
                    }
                if (selectedLineItem != null)
                {
                    clsMainLogic.updateLineItemQuantity(ref _CurrentInvoice, ref selectedLineItem, "delete");
                    _CurrentInvoice.updateTotalCost();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentInvoice)));
                }
            }
        }
    }
}