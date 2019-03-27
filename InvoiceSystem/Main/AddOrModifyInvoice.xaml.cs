using InvoiceSystem.Items;
using InvoiceSystem.OtherClasses;
using InvoiceSystem.Search;
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
    public partial class AddOrModifyInvoice : UserControl
    {
        private readonly ViewNavigationController viewNavigationController;
        private string NewOrEdit;

        public AddOrModifyInvoice(ViewNavigationController viewNavigationController)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            NewOrEdit = "new";
            DataContext = this;
        }

        public AddOrModifyInvoice(ViewNavigationController viewNavigationController, InvoiceList invoices)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            NewOrEdit = "edit";
            DataContext = this;
        }

        private void SubmitButton_Button(object sender, RoutedEventArgs e)
        {
            //send input to the 
            clsMainLogic helper = new clsMainLogic();



            viewNavigationController.ChangeCurrentView(new wndMain(viewNavigationController));
        }
    }
}
