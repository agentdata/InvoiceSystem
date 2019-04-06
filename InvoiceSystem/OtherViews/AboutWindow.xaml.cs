using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public ContentPresenter ContentPresenter;
        public DockPanel Menu_DockPanel;
        public AboutWindow(ref ContentPresenter opaqueContentCover_Label, ref DockPanel menu_DockPanel)
        {
            InitializeComponent();
            aboutWindowText = "This App was written by Brantly, Juan and Walker\n\n" +
                              "Main Window Created by Brantly\n" +
                              "Invoice Search Created by Juan\n" +
                              "Items Window Created by Walker.";
            this.ContentPresenter = opaqueContentCover_Label;
            this.Menu_DockPanel = menu_DockPanel;
            DataContext = this;
            Closed += MainGameChoiceWindow_Closed;

        }
        /// <summary>
        /// This is triggered by the Closed event, if a game button to exit the window and go back has not been pressed then
        /// the button must have been the top red x, when a game button is pressed it sets shutdown or quit to quit so the 
        /// shutdown process in this method doesn't occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainGameChoiceWindow_Closed(object sender, EventArgs e)
        {
            ContentPresenter.IsEnabled = true;
            Menu_DockPanel.IsEnabled = true;
        }

    }
}