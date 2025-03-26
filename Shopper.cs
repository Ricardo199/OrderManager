using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager
{
    using System.ComponentModel;

    public class Shopper : INotifyPropertyChanged
    {
        private int _idShopper;
        public int IdShopper {
            get => _idShopper;
            set {
                _idShopper = value;
                OnPropertyChanged(nameof(IdShopper));
            }
        }

        private string? _email;
        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string? _firstName;
        public string? FirstName { 
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string? _lastName;
        public string? LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string? _address;
        public string? Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string? _city;
        public string? City
        {
            get => _city;
            set {  
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private string? _stateProvince;
        public string? StateProvince
        {
            get => _stateProvince;
            set {  
                _stateProvince = value;
                OnPropertyChanged(nameof(StateProvince));
            }
        }

        private string? _country;
        public string? Country
        {
            get => _country;
            set { 
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        private string? _zipCode;
        public string? ZipCode
        {
            get => _zipCode;
            set {
                _zipCode = value;
                OnPropertyChanged(nameof(ZipCode));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
