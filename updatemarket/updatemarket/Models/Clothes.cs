using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
namespace updatemarket.Models
{
    public enum Size
    {
        Small,
        Medium,
        Big
    }
    public class Clothes : INotifyPropertyChanged
    {
        private string _name;
        private double _price;
        private double _rating;
        private string _imageUrl;
        private string _color;
        private string _size;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public double Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); OnPropertyChanged(nameof(FormattedPrice)); }
        }

        public double Rating
        {
            get => _rating;
            set { _rating = value; OnPropertyChanged(); }
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set { _imageUrl = value; OnPropertyChanged(); }
        }

        public string Color
        {
            get => _color;
            set { _color = value; OnPropertyChanged(); }
        }

        public string Size
        {
            get => _size;
            set { _size = value; OnPropertyChanged(); }
        }
        public string FormattedPrice => $"$ {Price:N0}";
    }
}
