using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager
{
    public class Basket : INotifyPropertyChanged
    {
        private int _idBasket;
        public int IdBasket
        {
            get => _idBasket;
            set
            {
                _idBasket = value;
                OnPropertyChanged(nameof(IdBasket));
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

        private Decimal _subtotal;
        public Decimal SubTotal
        { 
            get => _subtotal;
            set { 
                _subtotal = value;
                OnPropertyChanged(nameof(SubTotal));
            }
        }

        private DateTime _orderDate;
        public DateTime OrderDate { 
            get => _orderDate;
            set { 
                _orderDate = value;
                OnPropertyChanged(nameof(OrderDate));
            }
        }

        private int _idShopper;
        public int IdShopper { 
            get => _idShopper;
            set { 
                _idShopper = value;
                OnPropertyChanged(nameof(IdShopper));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
