using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace InvoiceSystem.OtherClasses
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public string aboutWindowText { get; set; }
        public AboutWindow()
        {
            InitializeComponent();
            aboutWindowText = "This App was written by Brantly, Juan and Walker\n\n" +
                              "Main Window Created by Brantly\n" +
                              "Invoice Search Created by Juan\n" +
                              "Items Window Created by Walker.";
                              
            DataContext = this;

        }
    }
}