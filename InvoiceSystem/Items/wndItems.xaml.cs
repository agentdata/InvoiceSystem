using InvoiceSystem.Main;
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

namespace InvoiceSystem.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : UserControl
    {
        //Fields
        private readonly ViewNavigationController viewNavigationController;

        /// <summary>
        /// Flag to determine if the window is in editing mode.
        /// </summary>
        private bool IsEditing;

        public wndItems(ViewNavigationController viewNavigationController)
        {
            InitializeComponent();
            this.viewNavigationController = viewNavigationController;
            IsEditing = false;
        }

        private void BeginningProperties()
        {
            //disable CodeText, CostText, and DescText -- Possible just read-only
            //disable CancelButton & SubmitButton

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //Changes grid select to be -1 (or similar)
            //Clears CodeText, CostText, and DescText
            //IsEditing = true;
            //enable Submit button;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //isEditing = true;
            //disable Addbutton and DeleteButton
            //Enable CodeText, CostText, and DescText
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //calls SQL statement to delete current selected item
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //can only be used if isEditing is true
            //end iwht isEditing = false;
            // disable CodeText, CostText, and DescText and CANCEL/SUBMIT buttons
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //can only be used if isEditing is true
            //SQL update for current selected Item
            //end iwht isEditing = false;
            // disable CodeText, CostText, and DescText and CANCEL/SUBMIT buttons
        }

        /// <summary>
        /// Returns back to the Main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new wndMain(viewNavigationController));
        }
    }
}