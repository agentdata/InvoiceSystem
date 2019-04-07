using InvoiceSystem.Classes;
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

        public NewInvoice(ViewNavigationController viewNavigationController)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            AvailableItems = clsMainSQL.GetAllItems();
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
            //generate new invoice

            //insert new invoice into DB

            //add line items to DB attached to this new invoice

            //return to main screen
            viewNavigationController.ChangeCurrentView(new wndMain(viewNavigationController));
        }

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
            //if(CurrentInvoice.LineItems.Where())
            DataGridRow row = sender as DataGridRow;
            Item selectedItem = (row.Item as Item);
            if (selectedItem != null)
            {
                //ifshoppingcart already contains item with same itemcode, then just add 1
                if (shoppingCart.containsItem(selectedItem.ItemCode))
                {
                    shoppingCart.lineItems.Where(x => x.Item.ItemCode == selectedItem.ItemCode).FirstOrDefault().Quantity++;
                }

                //otherwise add selectedItem to the running list
                else
                    shoppingCart.addLineItem(new LineItem(selectedItem,1));
                runningTotal += Int32.Parse(selectedItem.Cost);
            }
        }
        #endregion UI Actions
    }
}