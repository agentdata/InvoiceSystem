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
        #endregion UI Actions

        private void InvoiceRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}