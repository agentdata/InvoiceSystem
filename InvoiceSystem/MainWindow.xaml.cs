using System;
using InvoiceSystem.OtherClasses;
using InvoiceSystem.Main;
using InvoiceSystem.Items;
using InvoiceSystem.Search;
using System.Collections.Generic;
using System.ComponentModel;
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
using InvoiceSystem.OtherViews;

namespace InvoiceSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// This constructor is only used on first run of the application.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            MainView = new wndMain(viewNavigationController);
            viewNavigationController.CurrentViewChanged += ViewNavigationController_CurrentViewChanged;
            DataContext = this;
        }

        //Properties
        //this tracks which view should be displayed.
        private UserControl mainView;
        //UI contentpresenter is bound to mainview
        public UserControl MainView
        {
            get
            {
                return mainView;
            }
            private set
            {
                if (!Equals(mainView, value))
                {
                    mainView = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainView)));
                }
            }
        }

        //Fields
        //the navigation controller which controls the view shown in the window
        private readonly ViewNavigationController viewNavigationController = new ViewNavigationController();

        //Events
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// When the viewcontroller changes the mainview is updated, the UI contentpresenter is bound to this mainview object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="userControl"></param>
        private void ViewNavigationController_CurrentViewChanged(object sender, UserControl userControl)
        {
            MainView = userControl;
        }

        #region Menu Buttons
        /// <summary>
        /// Settings button action, opens the settings view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuBarSettingsButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new Settings(viewNavigationController));
        }

        /// <summary>
        /// exit button action, exits the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuBarExitButton_Action(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// Add New Invoice button action, opens the new Invoice view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewInvoiceMenuButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new NewInvoice(viewNavigationController));
        }

        /// <summary>
        /// search invoices button action, opens the search view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchInvoicesMenuButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new wndSearch(viewNavigationController));
        }

        /// <summary>
        /// edit items button action, opens the edit items view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditItemsMenuButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new wndItems(viewNavigationController));
        }

        /// <summary>
        /// main button action, opens the main dashboard view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuBarMainButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new wndMain(viewNavigationController));
        }

        /// <summary>
        /// about button action, opens the about window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuBarAboutItem_Action(object sender, RoutedEventArgs e)
        {
            MainContentPresenter_ContentPresenter.IsEnabled = false;
            Menu_DockPanel.IsEnabled = false;

            AboutWindow window = new AboutWindow(ref MainContentPresenter_ContentPresenter, ref Menu_DockPanel);

            window.Show();
        }

        #endregion Menu Buttons

        #region UI 
        /// <summary>
        /// updates the blur feature when the menu item is visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeDisabledForegroundAndOpacity_Action(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (MainContentPresenter_ContentPresenter.IsEnabled)
            {
                OpaqueContentCover_Label.Visibility = Visibility.Hidden;
            }
            else
            {
                OpaqueContentCover_Label.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// updates the blur feature when the menu item is visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                MainContentPresenter_ContentPresenter.IsEnabled = false;
                if (!Menu_DockPanel.IsEnabled)
                {
                    Menu_DockPanel.IsEnabled = true;
                }
            }
            else
            {
                MainContentPresenter_ContentPresenter.IsEnabled = true;
                if (!Menu_DockPanel.IsEnabled)
                {
                    Menu_DockPanel.IsEnabled = true;
                }
            }
        }

        #endregion UI
    }
}