using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Classes
{
    public class LineItems : IEnumerable
    {
        public ObservableCollection<LineItem> lineItems { get; set; } = new ObservableCollection<LineItem>();
        public int TotalLineItems { get; set; } = 0;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return lineItems.GetEnumerator();
        }

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

        public IEnumerator GetEnumerator()
        {
            return lineItems.GetEnumerator();
        }

        internal void Remove(object p)
        {
            throw new NotImplementedException();
        }

        internal object Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}