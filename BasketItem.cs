using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager
{
    public class BasketItem : INotifyPropertyChanged
    {
        private int _idBasketItem;
        public int IdBasketItem {
            get => _idBasketItem;
            set {  _idBasketItem = value; 
                OnPropertyChanged(nameof(IdBasketItem));
            }
        }

        private short _idProduct;
        public short IdProduct { 
            get => _idProduct;
            set {
                _idProduct = value;
                OnPropertyChanged(nameof(IdProduct));
            }
        }

        private Byte _quantity;
        public Byte Quantity { 
            get => _quantity;
            set {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private int _idBasket;
        public int IdBasket { 
            get => _idBasket;
            set { 
                _idBasket = value;
                OnPropertyChanged(nameof(IdBasket));
            }
        }

    public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
