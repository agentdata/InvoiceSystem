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
        #region variables
        private readonly ViewNavigationController viewNavigationController;
        clsMainLogic mainLogic;
        #endregion

        #region Constructors
        /// <summary>
        /// This is the only constructor which takes a ViewNavigationController so that the current view being displayed can be changed
        /// </summary>
        /// <param name="viewNavigationController"></param>
        public wndMain(ViewNavigationController viewNavigationController)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            mainLogic = new clsMainLogic();
            DataContext = this;
        }
        #endregion

        #region Other Methods

        //This is a temporary button for testing that refreshes the view
        private void GetInvoicesButton_Action(object sender, RoutedEventArgs e)
        {
            mainLogic.LoadInvoices();
        }
        #endregion
    }
}