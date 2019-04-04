using InvoiceSystem.Classes;
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
using System.Windows.Shapes;

namespace InvoiceSystem.Main
{
    /// <summary>
    /// Interaction logic for ViewItemDesc.xaml
    /// </summary>
    public partial class ViewItemDesc : Window
    {
        public Item SelectedItem {get; set;}

        public ViewItemDesc(Item selectedItem)
        {
            InitializeComponent();
            SelectedItem = selectedItem;
            DataContext = this;
        }

        private void CloseButton_Action(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
