using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InvoiceSystem.OtherClasses
{
    public class ViewNavigationController
    {
        //Events
        public event EventHandler<UserControl> CurrentViewChanged;

        //Methods
        public void ChangeCurrentView(UserControl usercontrol)
        {
            CurrentViewChanged?.Invoke(this, usercontrol);
        }
    }
}