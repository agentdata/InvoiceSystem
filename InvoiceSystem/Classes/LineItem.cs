using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Classes
{
    public class LineItem : INotifyPropertyChanged
    {
        #region Properties

        public Item Item { get; set; }
        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set
            {
                if (value != _Quantity)
                {
                    _Quantity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quantity)));
                }
            }
        }
        #endregion Properties

        /// <summary>
        /// Constructor takes in an item and the specified quantity, this makes a Line Item.
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="Quantity"></param>
        public LineItem(Item Item, int Quantity)
        {
            this.Item = Item;
            this.Quantity = Quantity;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}