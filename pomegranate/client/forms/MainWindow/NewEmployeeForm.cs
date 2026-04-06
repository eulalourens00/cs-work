using client.models;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqliteDB;
using System.Data.SQLite;

namespace client.forms.MainWindow
{
    public partial class NewEmployeeForm : Form
    {
        private readonly DBController _controller = new DBController(@"C:\Hackathon\dataBase.db");
        private readonly int _userId;
        private readonly string _username;
        private readonly string _email;

        public NewEmployeeForm()
        {
            InitializeComponent();


            LoadRolesAndPositions();
            SetupControls();
        }

        private void SetupControls()
        {
            UsernameBox.Text = "";
            PasswordBox.Text = "";
            EmailBox.Text = "";
            FnameBox.Text = "";
            LnameBox.Text = "";

            UsernameBox.ReadOnly = false;
            PasswordBox.ReadOnly = false;
            EmailBox.ReadOnly = false;
        }

        private void LoadRolesAndPositions()
        {
            try
            {
                string dbPath = @"Data Source=C:\Hackathon\dataBase.db;Version=3;";

                RoleComboBox.Items.Clear();
                using (var connection = new SQLiteConnection(dbPath))
                using (var command = new SQLiteCommand("SELECT id, name FROM roles", connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RoleComboBox.Items.Add(new RoleItem
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }

                RoleComboBox.DisplayMember = "Name";
                RoleComboBox.ValueMember = "Id";

                PositionComboBox.Items.Clear();
                using (var connection = new SQLiteConnection(dbPath))
                using (var command = new SQLiteCommand("SELECT id, name FROM positions", connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PositionComboBox.Items.Add(new PositionItem
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }

                PositionComboBox.DisplayMember = "Name";
                PositionComboBox.ValueMember = "Id";

                if (RoleComboBox.Items.Count > 0)
                    RoleComboBox.SelectedIndex = 0;

                if (PositionComboBox.Items.Count > 0)
                    PositionComboBox.SelectedIndex = 0;
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка загрузки данных: {ex.Message}"); }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FnameBox.Text) ||  string.IsNullOrWhiteSpace(LnameBox.Text))
            {  MessageBox.Show("Заполните все обязательные поля");  return; }

            if (RoleComboBox.SelectedItem == null || PositionComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль и должность");
                return;
            }

            try
            {
                var newEmployee = new Employees
                {
                    first_name = FnameBox.Text,
                    last_name = LnameBox.Text,
                    role_id = ((RoleItem)RoleComboBox.SelectedItem).Id,
                    position_id = ((PositionItem)PositionComboBox.SelectedItem).Id
                };

                int result = _controller.AddEmployeeWithUser(
                    newEmployee,
                    UsernameBox.Text.Trim(),
                    PasswordBox.Text.Trim(),
                    EmailBox.Text.Trim()
                );

                if (result > 0)
                {
                    MessageBox.Show($"Сотрудник {FnameBox.Text} {LnameBox.Text} успешно добавлен!\nЛогин: {UsernameBox.Text}");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось добавить сотрудника");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }
    }
    public class RoleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PositionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
