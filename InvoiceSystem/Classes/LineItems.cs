using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Classes
{
    public class LineItems
    {
        public ObservableCollection<LineItem> lineItems { get; set; } = new ObservableCollection<LineItem>();
        public int TotalLineItems { get; set; } = 0;

        public void addLineItem(LineItem LineItem)
        {
            lineItems.Add(LineItem);
            TotalLineItems += LineItem.Quantity;
        }

        public bool containsItem(string ItemCode)
        {
            foreach(LineItem LineItem in lineItems)
            {
                if(LineItem.Item.ItemCode == ItemCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
