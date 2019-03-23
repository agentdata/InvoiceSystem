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
    }
}