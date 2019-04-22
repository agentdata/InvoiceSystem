using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InvoiceSystem.Classes
{
    //Logic for a LineItems class which holds a list of lineitems, and a total
    public class LineItems
    {
        public ObservableCollection<LineItem> lineItems { get; set; } = new ObservableCollection<LineItem>();
        public int TotalLineItems { get; set; } = 0;

        /// <summary>
        /// This function adds a lineItem andupdates the totalLineItems property
        /// </summary>
        /// <param name="LineItem"></param>
        public void addLineItem(LineItem LineItem)
        {
            lineItems.Add(LineItem);
            TotalLineItems += LineItem.Quantity;
        }

        /// <summary>
        /// Returns if the lineitems list contains an item with the specified itemcode.
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the enumerator for the lineitems collection
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return lineItems.GetEnumerator();
        }

        /// <summary>
        /// removes the specified object from the lineitems list.
        /// </summary>
        /// <param name="LineItemToRemove">This object is a LineItem</param>
        internal void Remove(object LineItemToRemove)
        {
            lineItems.Remove(LineItemToRemove as LineItem);
        }
    }
}