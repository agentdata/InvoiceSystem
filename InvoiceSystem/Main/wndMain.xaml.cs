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
        //Fields
        private readonly ViewNavigationController viewNavigationController;
        //InvoiceList Invoices = new InvoiceList();
        clsMainLogic mainLogic;
        

        public wndMain(ViewNavigationController viewNavigationController)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            mainLogic = new clsMainLogic();
            DataContext = this;
        }

        private void AddNewInvoiceButton_Action(object sender, RoutedEventArgs e)
        {
        }

        private void SearchInvoicesButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new wndSearch(viewNavigationController));
        }

        private void InvoiceItemEditButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new wndItems(viewNavigationController));
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void GetInvoicesButton_Action(object sender, RoutedEventArgs e)
        {
            mainLogic.LoadInvoices();
        }
    }
}