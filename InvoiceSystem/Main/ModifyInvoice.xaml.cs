using InvoiceSystem.Classes;
using InvoiceSystem.Items;
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

namespace InvoiceSystem.Main
{
    /// <summary>
    /// Interaction logic for AddNewInvoice.xaml
    /// </summary>
    public partial class ModifyInvoice : UserControl
    {
        private readonly ViewNavigationController viewNavigationController;
        private string NewOrEdit;
        public Invoice CurrentInvoice { get; set; }

        #region Constructors
        public ModifyInvoice(ViewNavigationController viewNavigationController, Invoice invoice)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            CurrentInvoice = invoice;
            NewOrEdit = "edit";
            
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
            switch (NewOrEdit)
            {
                case "edit":
                    //sql command to edit existing entry
                    break;
                case "new":
                    //sql command to add new entry
                    break;
            }

            //then return to main screen
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
        #endregion UI Actions
    }
}