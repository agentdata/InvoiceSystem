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
    /// Interaction logic for NewInvoice.xaml
    /// </summary>
    public partial class NewInvoice : UserControl
    {
        private readonly ViewNavigationController viewNavigationController;
        private string NewOrEdit;
        public Invoice CurrentInvoice { get; set; }

        #region Constructors

        public NewInvoice(ViewNavigationController viewNavigationController)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            NewOrEdit = "new";
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

        private void ResetButton_Button(object sender, RoutedEventArgs e)
        {
            //Return reset the screen screen
            viewNavigationController.ChangeCurrentView(new NewInvoice(viewNavigationController));
        }
        #endregion UI Actions


    }
}