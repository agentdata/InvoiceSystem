using System;
using InvoiceSystem.OtherClasses;
using InvoiceSystem.Main;
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
using InvoiceSystem.Search;
using InvoiceSystem.Items;
using System.Data;

namespace InvoiceSystem.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : UserControl
    {
        #region Fields
        private readonly ViewNavigationController viewNavigationController;
        #endregion Fields

        #region Properties
        public InvoiceList Invoices { get; set; } = new InvoiceList();
        public int totalNumItems { get { return clsMainSQL.totalItems(); } }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// This is the only constructor which takes a ViewNavigationController so that the current view being displayed can be changed
        /// </summary>
        /// <param name="viewNavigationController"></param>
        public wndMain(ViewNavigationController viewNavigationController)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            IntitialWindowLoad();
            DataContext = this;
        }
        #endregion

        #region Other Methods
        
        /// <summary>
        /// Tasks to complete when this view initially loads
        /// </summary>
        private void IntitialWindowLoad()
        {
            Invoices = clsMainSQL.getAllInvoices(Invoices);
        }

        #endregion

        #region UI Action
        private void InvoiceRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            if (row != null)
            {
                Invoice selectedInvoice = row.Item as Invoice;
                viewNavigationController.ChangeCurrentView(new AddOrModifyInvoice(this.viewNavigationController, selectedInvoice));
            }
        }
        #endregion UI Action


        private void GridViewRightClickDelete_Action(object sender, RoutedEventArgs e)
        {

            DataGridRow row = sender as DataGridRow;
            //Invoice selectedInvoice = row.Item as Invoice;
            MessageBoxResult shouldBeDeleted = MessageBox.Show("Are you sure you want to delete invoice #"+ sender.GetType(),"Delete Invoice?",MessageBoxButton.YesNo);
            
            if (row != null)
            {
                
                //Invoices.InvoicesCollection.Remove(Invoices.InvoicesCollection.Where(x => x.InvoiceNum == (row.Item as Invoice).InvoiceNum).FirstOrDefault());
            }
        }

        private void InvoiceRowRightClickEdit_Action(object sender, RoutedEventArgs e)
        {
            //Get the clicked MenuItem
            var menuItem = (MenuItem)sender;

            //Get the ContextMenu to which the menuItem belongs
            var contextMenu = (ContextMenu)menuItem.Parent;

            //Find the placementTarget
            var row = (DataGridRow)contextMenu.PlacementTarget;

            if (row != null)
            {
                Invoice selectedInvoice = row.Item as Invoice;

                viewNavigationController.ChangeCurrentView(new AddOrModifyInvoice(this.viewNavigationController, Invoices.InvoicesCollection.Where(x=> x.InvoiceNum == selectedInvoice.InvoiceNum).FirstOrDefault()));

            }
        }
    }
}