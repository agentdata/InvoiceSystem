using System;
using InvoiceSystem.OtherClasses;
using InvoiceSystem.Main;
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
using InvoiceSystem.Search;
using InvoiceSystem.Items;

namespace InvoiceSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            MainView = new wndMain(viewNavigationController);
            viewNavigationController.CurrentViewChanged += ViewNavigationController_CurrentViewChanged;
            DataContext = this;
        }

        //Properties
        private UserControl mainView;
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
        private readonly ViewNavigationController viewNavigationController = new ViewNavigationController();

        //Events
        public event PropertyChangedEventHandler PropertyChanged;

        private void ViewNavigationController_CurrentViewChanged(object sender, UserControl userControl)
        {
            MainView = userControl;
        }

        #region Menu Buttons

        private void MenuBarExitButton_Action(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void AddNewInvoiceMenuButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new AddOrModifyInvoice(viewNavigationController));
        }

        private void SearchInvoicesMenuButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new wndSearch(viewNavigationController));
        }

        private void EditItemsMenuButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new wndItems(viewNavigationController));
        }

        private void MenuBarMainButton_Action(object sender, RoutedEventArgs e)
        {
            viewNavigationController.ChangeCurrentView(new wndMain(viewNavigationController));
        }

        private void MenuBarAboutItem_Action(object sender, RoutedEventArgs e)
        {
            AboutWindow window = new AboutWindow();
            window.Show();
        }

        #endregion

    }
}