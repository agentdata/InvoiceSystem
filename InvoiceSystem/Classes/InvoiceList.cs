using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.OtherClasses
{
    public class InvoiceList: INotifyPropertyChanged
    {
        #region Properties
        public ObservableCollection<Invoice> InvoicesCollection { get; set; } = new ObservableCollection<Invoice>();
        public int totalInvoices { get { return InvoicesCollection.Count(); } }
        public int totalItems
        {
            get
            {
                int totalNumItems = 0;
                foreach (Invoice invoice in this.InvoicesCollection)
                {
                    totalNumItems += invoice.TotalItems;
                }
                return totalNumItems;
            }
        }
        public int totalRevenue { get; set; } = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Properties

        /// <summary>
        /// Use this function when adding new invoice to the list, don't directly modify invoices collection outside of this function
        /// </summary>
        /// <param name="Invoice"></param>
        public void addInvoice(Invoice Invoice)
        {
            InvoicesCollection.Add(Invoice);
            int TotalInvoiceRevenue;
            if(Int32.TryParse(Invoice.TotalCost, out TotalInvoiceRevenue))
            {
                totalRevenue += TotalInvoiceRevenue;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(totalInvoices)));
        }
    }
}