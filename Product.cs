using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager
{
    public class Product : INotifyPropertyChanged
    {
        private short _idProduct;
        public short IdProduct
        {
            get => _idProduct;
            set
            {
                _idProduct = value;
                OnPropertyChanged(nameof(IdProduct));
            }
        }

        private string? _productName;
        public string? ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        private string? _description;
        public string? Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private Decimal _price;
        public Decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
