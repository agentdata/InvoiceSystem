using InvoiceSystem.Main;
using InvoiceSystem.OtherClasses;
using InvoiceSystem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        #region Variables
        /// <summary>
        /// Instance of class that navigates through the different user Controls.
        /// </summary>
        private readonly ViewNavigationController viewNavigationController;

        /// <summary>
        /// Item Object to hold selected Items and it's properties.
        /// </summary>
        private Item item;

        /// <summary>
        /// Boolean setting for noting when the Edit button has been pressed and
        /// the user is editing an item.
        /// </summary>
        private bool IsEditing;
        #endregion

        #region HelperMethods
        /// <summary>
        /// Contructor for the wndItems window.
        /// </summary>
        /// <param name="viewNavigationController"></param>
        public wndItems(ViewNavigationController viewNavigationController)
        {
            try
            {
                InitializeComponent();
                this.viewNavigationController = viewNavigationController;
                item = new Item("", "", "");
                
                BeginningProperties();

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            
        }

        /// <summary>
        /// Sets the properites of window objects for initial use.
        /// </summary>
        private void BeginningProperties()
        {
            try
            {
                clsItemsLogic.GetItems();
                ItemDataGrid.ItemsSource = clsItemsLogic.ItemsList;

                //disable CodeText, CostText, and DescText
                CostText.IsReadOnly = true;
                DescText.IsReadOnly = true;

                //disable CancelButton & SubmitButton
                CancelButton.IsEnabled = false;
                SubmitButton.IsEnabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns window objects to a proper state after a user completes
        /// an add, edit, or delete.
        /// </summary>
        private void ReadView()
        {
            try
            {
                CodeText.IsEnabled = false;

                AddButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;

                //Text Boxes non-editable
                CostText.IsReadOnly = true;
                DescText.IsReadOnly = true;

                //Submit and Cancel Disabled
                SubmitButton.IsEnabled = false;
                CancelButton.IsEnabled = false;
                EditButton.IsEnabled = true;
                AddButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        #endregion

        #region Selection/Validation
        /// <summary>
        /// Event that is triggered when a selection is made in the Item Data Grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemSelection(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (!IsEditing)
                {
                    int index = ItemDataGrid.SelectedIndex;

                    CodeText.Text = clsItemsLogic.ItemsList[index].ItemCode;
                    CostText.Text = clsItemsLogic.ItemsList[index].Cost;
                    DescText.Text = clsItemsLogic.ItemsList[index].ItemDesc;
                }

            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Error Handling method for Friendly Error output.
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Allows the user to only use numbers and a period, as well as
        /// backspace, delete, tab, and enter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidation(object sender, KeyEventArgs e)
        {
            try
            {
                //Number
                if (!(e.Key >= Key.D0 && e.Key <= Key.D9))
                {
                    //KeyPad
                    if (!(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
                    {
                        //Allow backspace, delete, tab and enter
                        if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.Decimal))
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// allows the user to use alpha numeric keys along with backspace,
        /// delete, tab, and enter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyValidation(object sender, KeyEventArgs e)
        {
            try
            {
                //Letters & numbers
                if (!(e.Key >= Key.D0 && e.Key <= Key.Z))
                {
                    //Allow backspace, delete, tab and enter
                    if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.Space))
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion

        #region Buttons
        /// <summary>
        /// Action for the Add Button.  Disables buttons and 
        /// changes the cost, code, and description text boxes
        /// to not be read-only.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Changes grid select to be -1 (or similar)
                //Clears CodeText, CostText, and DescText
                //enable Submit/Cancel button;
                
                CodeText.Text = "";
                DescText.Text = "";
                CostText.Text = "";

                CodeText.IsEnabled = true;
                CostText.IsReadOnly = false;
                DescText.IsReadOnly = false;

                SubmitButton.IsEnabled = true;
                CancelButton.IsEnabled = true;
                AddButton.IsEnabled = false;
                EditButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                
                


            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Action for the Edit Button.  Disables, buttons and changes cost and description
        /// text boxes from read-only.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                IsEditing = true;

                AddButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                EditButton.IsEnabled = false;

                //Text Boxes editable
                CostText.IsReadOnly = false;
                DescText.IsReadOnly = false;

                //Submit and Cancel Enabled
                SubmitButton.IsEnabled = true;
                CancelButton.IsEnabled = true;
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Action for the Delete Button.  Removes item from the Data Grid and Database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                int index = ItemDataGrid.SelectedIndex;
                string code = CodeText.Text;
                string desc = DescText.Text;
                string cost = CostText.Text;

                if (index > 0)
                {
                    ItemDataGrid.SelectedIndex = index -1;
                }
                else
                {
                    ItemDataGrid.SelectedIndex = -1;
                }


                clsItemsLogic.DeleteItem(code, desc, cost, index);
                CodeText.Text = "";
                CostText.Text = "";
                DescText.Text = "";
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Action for the Cancel Button.  Returns window to the standard view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                item = (Item)ItemDataGrid.SelectedItem;

                CodeText.Text = item.ItemCode;
                CostText.Text = item.Cost;
                DescText.Text = item.ItemDesc;

                ReadView();

                IsEditing = false;
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Action for the Submit Button.  If the user is editing, it updates the Data grid
        /// and database.  If not, it adds to the list and the database.  Then returns to 
        /// a normal view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (IsEditing)
                {
                    clsItemsLogic.UpdateItem(CodeText.Text, DescText.Text, CostText.Text, ItemDataGrid.SelectedIndex);
                    ReadView();
                    IsEditing = false;
                }
                else
                {
                    clsItemsLogic.AddItem(CodeText.Text, DescText.Text, CostText.Text);
                    ReadView();
                    ItemDataGrid.SelectedIndex = clsItemsLogic.ItemsList.Count - 1;
                }

                
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Returns back to the Main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReadView();
                viewNavigationController.ChangeCurrentView(new wndMain(viewNavigationController));
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        
    }
}