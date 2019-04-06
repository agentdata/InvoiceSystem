using InvoiceSystem.Classes;
using InvoiceSystem.Items;
using InvoiceSystem.OtherClasses;
using System;
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
        List<LineItem> newLineItems = new List<LineItem>();
        List<LineItem> updateLineItems = new List<LineItem>();
        // Navigation controller which is passed around so that the view can be updated.
        private readonly ViewNavigationController viewNavigationController;
        // This bool is changed when a lineitem quantity is changed.
        public bool wasModified = false;
        //event to be raised whena property is modified so the view stays up to date.
        public event PropertyChangedEventHandler PropertyChanged;
        bool firstTime = true;
        #endregion Fields

        #region Constructors
        public ModifyInvoice(ViewNavigationController viewNavigationController, Invoice invoice)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            CurrentInvoice = invoice;
            AvailableItems = clsMainSQL.GetAllItems();
            firstTime = false;
            DataContext = this;
            
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
            
            foreach (LineItem LineItem in newLineItems)
            {
                //add new lineitem
                clsMainSQL.addNewLineItemSQL(CurrentInvoice.InvoiceNum, LineItem, "0", LineItem.Quantity);
            }

            foreach (LineItem LineItem in CurrentInvoice.LineItems.lineItems)
            {
                
                //update existing line item with new quantity
                clsMainSQL.UpdateLineItemSQL(CurrentInvoice.InvoiceNum, LineItem.Item.ItemCode, LineItem.Quantity);
            }

            //add new line item
            //update the invoice total cost.
            if (newLineItems.Count > 0 || updateLineItems.Count > 0)
            {
                clsMainLogic.updateInvoiceTotalCost(CurrentInvoice);
            }
            viewNavigationController.ChangeCurrentView(new wndMain(viewNavigationController));
        }

        /// <summary>
        /// This doubleclick action brings up a small item window which displays everything about the item selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            if (row != null && row.Item != null) { (new ViewItemDesc((row.Item as LineItem).Item)).Show(); }
        }

        /// <summary>
        /// 
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
                bool alreadyInInvoice = false;
                foreach (LineItem LineItem in CurrentInvoice.LineItems.lineItems)
                {
                    //if currentinvoice contains item add +1 to the quantity
                    if (LineItem.Item.ItemCode == selectedItem.ItemCode)
                    {
                        //add item to current
                        CurrentInvoice.LineItems.lineItems.Where(x => x.Item.ItemCode == selectedItem.ItemCode).FirstOrDefault().Quantity++;
                        alreadyInInvoice = true;
                        updateLineItems.Add(new LineItem(LineItem.Item, CurrentInvoice.LineItems.lineItems.Where(x => x.Item.ItemCode == selectedItem.ItemCode).FirstOrDefault().Quantity));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentInvoice)));
                    }
                }

                if (!alreadyInInvoice)
                {
                    CurrentInvoice.addItem(selectedItem, 1);
                    //add lineitem to newlineitem list which will but updated at
                    newLineItems.Add(new LineItem(selectedItem, 1));
                }
            }
        }
        #endregion UI Actions
    }
}
