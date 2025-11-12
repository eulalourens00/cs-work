using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.Json;
using MVVM.models;

namespace MVVM.viewModels
{
    class PersonVM:INotifyPropertyChanged
    {
        private Person person;
        private string message;

        // пошло поехало
        public PersonVM()
        {
            Person = new Person
            {
                birthday = DateTime.Now.AddYears(-18)
            };
            SaveCommand = new Command(async () => await SavePersonAsync());
        }

        public Person Person
        {
            get => person;
            set
            {
                person = value;
                OnPropertyChanged(nameof(Person));
            }
        }
        public string Message
        {
            get=> message;
            set
            {
                Message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public List<string> Gender { get; } = new List<string>()
        {
            "Male", "Female"
        };

        public List<string> Relationship_Status { get; } = new List<string>()
        {
            "Single", "Married", "Brake Up", "Vdova"
        };

        public Command SaveCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task SavePersonAsync()
        {
            try
            {
                //господи, помоги
                if (string.IsNullOrWhiteSpace(Person.firstName) || string.IsNullOrWhiteSpace(Person.firstName)
                    || string.IsNullOrWhiteSpace(Person.fatherName) || string.IsNullOrWhiteSpace(Person.gender))
                {
                    message = "Add your data in space";
                    return;
                }

                var personData = new
                {
                    Person.lastName,
                    Person.firstName,
                    Person.fatherName,
                    Person.gender,
                    birthday = Person.birthday.ToShortDateString(),
                    Person.rel_status,
                    Person.extraInfo,

                    SavedAt = DateTime.Now.ToShortDateString()
                };

                string json = JsonSerializer.Serialize(personData, new JsonSerializerOptions()
                {
                    WriteIndented = true
                });

                string fileName = $"person_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                await File.WriteAllTextAsync(filePath, json);

                message = $"Saved at {fileName}"; //ура он прошел проверку ура

                await Task.Delay(1000);
                CleanForm();
            }
            catch (Exception ex) { message = $"Something's wrong.\n{ex.Message}"; };
            // мамаа
        }
        public void CleanForm()
        {
            Person = new Person
            {
                birthday = DateTime.Now.AddYears(-18)
            };
            message = string.Empty;
        }
    }
}
