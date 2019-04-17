﻿using System;
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
        //Navigation controller which is passed around so that the view can be updated.
        private readonly ViewNavigationController viewNavigationController;
        #endregion Fields

        #region Properties
        //List of all invoices pulled when this view is initialized, used to populate the datagrid in the view.
        public InvoiceList Invoices { get; set; } = new InvoiceList();
        //This number is queried when the view is initialized, it is to show the total number of items in the database for the stats column.
        public int totalNumItems { get { return clsMainLogic.totalNumItems(); } }
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
            Invoices = clsMainLogic.getAllInvoices(Invoices);
        }
        #endregion

        #region UI Action
        /// <summary>
        /// Double CLick action for the datagrid displaying the invoices.
        /// Double clicking will pass the selected invoice to a new modifyinvoice window and the user can then update the items, or item quantity.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            if (row != null)
            {
                Invoice selectedInvoice = row.Item as Invoice;
                viewNavigationController.ChangeCurrentView(new ModifyInvoice(this.viewNavigationController, selectedInvoice));
            }
        }

        /// <summary>
        /// Edit action when a row is right clicked and edit is selected.
        /// This will pass the selected invoice to a new modifyInvoice window and the user can then update the items, or item quantity.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                viewNavigationController.ChangeCurrentView(new ModifyInvoice(this.viewNavigationController, Invoices.InvoicesCollection.Where(x => x.InvoiceNum == selectedInvoice.InvoiceNum).FirstOrDefault()));
            }
        }

        /// <summary>
        /// Delete action when a row is right clicked and delete is selected.
        /// The user will get a confirmation screen
        /// This will delete the invoice from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewRightClickDelete_Action(object sender, RoutedEventArgs e)
        {
            //Get the clicked MenuItem
            var menuItem = (MenuItem)sender;

            //Get the ContextMenu to which the menuItem belongs
            var contextMenu = (ContextMenu)menuItem.Parent;

            //Find the placementTarget
            var row = (DataGridRow)contextMenu.PlacementTarget;
            MessageBoxResult shouldBeDeleted = MessageBox.Show("Are you sure you want to delete invoice #" + row.GetType(), "Confirm Delete Invoice?", MessageBoxButton.YesNo);

            if (row != null && shouldBeDeleted == MessageBoxResult.Yes)
            {
                clsMainSQL.deleteInvoice(row.Item as Invoice);
            }
            //trigger stats update by just reloading the view
            viewNavigationController.ChangeCurrentView(new wndMain(viewNavigationController));
        }
        #endregion UI Action
    }
}