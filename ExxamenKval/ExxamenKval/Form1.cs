using System;
using System.Linq;
using System.Windows.Forms;
using ExxamenKval.ViewModel;
using ExxamenKval.Models;

namespace ExxamenKval
{
    public partial class Form1 : Form
    {

        private TourRepository _repository;

        public Form1()
        {
            InitializeComponent();

            _repository = new TourRepository();
            LoadComboBoxes();

            numAdults.ValueChanged += OnTouristCountChanged;
            numChildren.ValueChanged += OnTouristCountChanged;
            cmbTours.SelectedIndexChanged += OnTourChanged;
        }

        private void InitializeDatabase()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Database.EnsureCreated();

                    if (!context.TourOperators.Any())
                    {
                        // пошел
                        var operators = new List<TourOperator>
                        {
                            new TourOperator { Name = "Pegas Touristik", INN = "7712345678", Phone = "8-800-555-01-01" },
                            new TourOperator { Name = "Coral Travel", INN = "7722334455", Phone = "8-800-555-02-02" }
                        };
                        context.TourOperators.AddRange(operators);
                        context.SaveChanges();

                        // в баню
                        var tours = new List<Tour>
                        {
                            new Tour { Name = "Выходные в Сочи", Country = "Россия", City = "Сочи",
                                       HotelName = "Radisson", NightsCount = 3,
                                       PricePerAdult = 25000, PricePerChild = 15000,
                                       TourOperatorId = 1 },
                            new Tour { Name = "Пляжный Египет", Country = "Египет", City = "Хургада",
                                       HotelName = "Sunrise", NightsCount = 7,
                                       PricePerAdult = 60000, PricePerChild = 40000,
                                       TourOperatorId = 2 }
                        };
                        context.Tours.AddRange(tours);
                        context.SaveChanges();

                        // меня заепло это
                        var clients = new List<Client>
                        {
                            new Client { FullName = "Иван Петров", PassportNumber = "4510 123456",
                                         Phone = "+7-999-111-22-33", BirthDate = new DateTime(1990, 05, 15), IsVip = false },
                            new Client { FullName = "Мария Смирнова", PassportNumber = "4510 654321",
                                         Phone = "+7-999-444-55-66", BirthDate = new DateTime(1985, 10, 20), IsVip = true }
                        };
                        context.Clients.AddRange(clients);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации БД: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxes()
        {
            var clients = _repository.GetAllClients();
            cmbClients.DataSource = clients;
            cmbClients.DisplayMember = "FullName";
            cmbClients.ValueMember = "Id";

            var tours = _repository.GetAllTours();
            cmbTours.DataSource = tours;
            cmbTours.DisplayMember = "Name";
            cmbTours.ValueMember = "Id";
        }

        private void OnTouristCountChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void OnTourChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            if (cmbTours.SelectedItem == null) return;

            int tourId = (int)cmbTours.SelectedValue;
            var tour = _repository.GetTourById(tourId);
            if (tour == null) return;

            int adults = (int)numAdults.Value;
            int children = (int)numChildren.Value;

            decimal total = (adults * tour.PricePerAdult) + (children * tour.PricePerChild);

            txtTotalPrice.Text = total.ToString("F2");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbClients.SelectedItem == null || cmbTours.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента и тур!");
                return;
            }

            decimal total = Convert.ToDecimal(txtTotalPrice.Text);

            if (total < 10000)
            {
                MessageBox.Show("Сумма заявки слишком мала (менее 10 000 руб.). Проверьте количество туристов.",
                                "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Order newOrder = new Order
            {
                ClientId = (int)cmbClients.SelectedValue,
                TourId = (int)cmbTours.SelectedValue,
                DepartureDate = dtpDeparture.Value,
                AdultCount = (int)numAdults.Value,
                ChildCount = (int)numChildren.Value,
                TotalPrice = total,
                Status = "New"
            };

            _repository.AddOrder(newOrder);

            MessageBox.Show($"Заявка для {cmbClients.Text} успешно создана! Сумма: {total:F2} руб.",
                            "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            numAdults.Value = 0;
            numChildren.Value = 0;
            txtTotalPrice.Text = "0,00";
        }
    }
}
