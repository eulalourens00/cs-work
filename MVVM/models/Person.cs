using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MVVM.models
{
    public class Person : INotifyPropertyChanged
    {
        private string _lastName;
        private string _firstName;
        private string _nameOfFather;
        private string _gender;
        private DateTime _birthday;
        private string? _relationshipStatus;
        private string? _extraInfo;

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string lastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(lastName));
            }
        }

        public string firstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(firstName));
            }
        }

        public string fatherName
        {
            get => _nameOfFather;
            set
            {
                _nameOfFather = value;
                OnPropertyChanged(nameof(fatherName));
            }
        }

        public string gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(gender));
            }
        }

        public DateTime birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(birthday));
            }
        }

        public string rel_status
        {
            get => _relationshipStatus;
            set
            {
                _relationshipStatus = value;
                OnPropertyChanged(nameof(rel_status));
            }
        }

        public string extraInfo
        {
            get => _extraInfo;
            set
            {
                _extraInfo = value;
                OnPropertyChanged(nameof(extraInfo));
            }
        }
    }
}

