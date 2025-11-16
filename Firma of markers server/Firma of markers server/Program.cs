using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Data.Sql;
using System.Data.Common;
namespace Firma_of_markers_server
{
    internal class Program
    {
        public static string connectionString = "Data Source=firma.db";
        public static SqliteConnection connection;
        public static bool isConnected = false;
        public static DB_Query dbQuery;

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.Clear();
            Console.WriteLine("\t\tДобро пожаловать! Вас приветствует Фирма канцтоваров.\n");

            while (true)
            {
                if (!isConnected)
                {
                    MainMenu();
                }
                else
                {
                    ExtraMenu();
                }
            }
        }


        // менюшки
        static public void MainMenu()
        {
            Console.WriteLine("\n=== Меню подключения ===");
            Console.WriteLine("1. Подключиться к базе данных");
            Console.WriteLine("2. Создать БД и таблицы");
            Console.WriteLine("3. Выход");
            Console.Write("Выберите опцию: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ConnectToDatabase();
                    break;
                case "2":
                    CreateDatabase();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }

        static public void ExtraMenu()
        {
            Console.WriteLine("\n=== Главное меню ===");
            Console.WriteLine("1. Отключиться от базы данных");
            Console.WriteLine("\n--- Основная информация ---");
            Console.WriteLine("2. Показать все канцтовары");
            Console.WriteLine("3. Показать все типы канцтоваров");
            Console.WriteLine("4. Показать всех менеджеров");
            Console.WriteLine("\n--- Аналитика ---");
            Console.WriteLine("5. Канцтовары с максимальным количеством");
            Console.WriteLine("6. Канцтовары с минимальным количеством");
            Console.WriteLine("7. Канцтовары с минимальной себестоимостью");
            Console.WriteLine("8. Канцтовары с максимальной себестоимостью");
            Console.WriteLine("\n--- Дополнительные запросы ---");
            Console.WriteLine("9. Канцтовары заданного типа");
            Console.WriteLine("10. Канцтовары, проданные конкретным менеджером");
            Console.WriteLine("11. Канцтовары, купленные конкретной фирмой");
            Console.WriteLine("12. Информация о самой недавней продаже");
            Console.WriteLine("13. Среднее количество товаров по типам");
            Console.WriteLine("14. Выход");
            Console.Write("Выберите опцию: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisconnectFromDatabase();
                    break;
                case "2":
                    dbQuery.DisplayAllProducts();
                    break;
                case "3":
                    dbQuery.DisplayAllProductTypes();
                    break;
                case "4":
                    dbQuery.DisplayAllManagers();
                    break;
                case "5":
                    dbQuery.DisplayProductsWithMaxQuantity();
                    break;
                case "6":
                    dbQuery.DisplayProductsWithMinQuantity();
                    break;
                case "7":
                    dbQuery.DisplayProductsWithMinCostPrice();
                    break;
                case "8":
                    dbQuery.DisplayProductsWithMaxCostPrice();
                    break;
                case "9":
                    Console.Write("Введите тип канцтоваров: ");
                    string type = Console.ReadLine();
                    dbQuery.DisplayProductsByType(type);
                    break;
                case "10":
                    Console.Write("Введите имя менеджера: ");
                    string manager = Console.ReadLine();
                    dbQuery.DisplayProductsSoldByManager(manager);
                    break;
                case "11":
                    Console.Write("Введите название фирмы: ");
                    string firm = Console.ReadLine();
                    dbQuery.DisplayProductsBoughtByFirm(firm);
                    break;
                case "12":
                    dbQuery.DisplayMostRecentSale();
                    break;
                case "13":
                    dbQuery.DisplayAverageQuantityByType();
                    break;
                case "14":
                    DisconnectFromDatabase();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }


        // подклчение

        static void ConnectToDatabase()
        {
            try
            {
                connection = new SqliteConnection(connectionString);
                connection.Open();

                dbQuery = new DB_Query(connection);
                Console.WriteLine("Подключение к базе данных выполнено успешно!");
                isConnected = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения: {ex.Message}");
            }
        }

        static void DisconnectFromDatabase()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                Console.WriteLine("Отключение от базы данных выполнено успешно.");
            }
            isConnected = false;
            dbQuery = null;
        }

        // окееее летс гоу
        // сколько всео одинакв
        static void CreateDatabase()
        {
            try
            {
                connection = new SqliteConnection(connectionString);
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "PRAGMA foreign_keys = ON";
                    command.ExecuteNonQuery();

                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS ProductTypes (
                            TypeID INTEGER PRIMARY KEY AUTOINCREMENT,
                            TypeName TEXT NOT NULL
                        )";
                    command.ExecuteNonQuery();

                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Managers (
                            ManagerID INTEGER PRIMARY KEY AUTOINCREMENT,
                            ManagerName TEXT NOT NULL,
                            Phone TEXT,
                            Email TEXT
                        )";
                    command.ExecuteNonQuery();

                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS CustomerFirms (
                            FirmID INTEGER PRIMARY KEY AUTOINCREMENT,
                            FirmName TEXT NOT NULL,
                            Address TEXT,
                            ContactPerson TEXT
                        )";
                    command.ExecuteNonQuery();

                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Products (
                            ProductID INTEGER PRIMARY KEY AUTOINCREMENT,
                            ProductName TEXT NOT NULL,
                            TypeID INTEGER NOT NULL,
                            Quantity INTEGER NOT NULL DEFAULT 0,
                            CostPrice REAL NOT NULL,
                            FOREIGN KEY (TypeID) REFERENCES ProductTypes(TypeID)
                        )";
                    command.ExecuteNonQuery();

                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Sales (
                            SaleID INTEGER PRIMARY KEY AUTOINCREMENT,
                            ProductID INTEGER NOT NULL,
                            ManagerID INTEGER NOT NULL,
                            FirmID INTEGER NOT NULL,
                            QuantitySold INTEGER NOT NULL,
                            UnitPrice REAL NOT NULL,
                            SaleDate TEXT NOT NULL,
                            FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
                            FOREIGN KEY (ManagerID) REFERENCES Managers(ManagerID),
                            FOREIGN KEY (FirmID) REFERENCES CustomerFirms(FirmID)
                        )";
                    command.ExecuteNonQuery();

                    FillTestData(command);
                }

                dbQuery = new DB_Query(connection);
                Console.WriteLine("Успешно");
                isConnected = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка создания базы данных: {ex.Message}");
            }
        }

        static void FillTestData(SqliteCommand command)
        {
            string[] tables = { "Sales", "Products", "CustomerFirms", "Managers", "ProductTypes" };
            foreach (var table in tables)
            {
                command.CommandText = $"DELETE FROM {table}";
                command.ExecuteNonQuery();
            }

            command.CommandText = @"
                INSERT INTO ProductTypes (TypeName) VALUES 
                ('Ручки'),
                ('Карандаши'),
                ('Бумага'),
                ('Папки'),
                ('Клей')";
            command.ExecuteNonQuery();

            command.CommandText = @"
                INSERT INTO Managers (ManagerName, Phone, Email) VALUES 
                ('Иванов Петр Сергеевич', '+7-912-123-4567', 'ivanov@firm.ru'),
                ('Сидорова Мария Ивановна', '+7-912-765-4321', 'sidorova@firm.ru'),
                ('Петров Алексей Владимирович', '+7-912-555-8899', 'petrov@firm.ru')";
            command.ExecuteNonQuery();

            command.CommandText = @"
                INSERT INTO CustomerFirms (FirmName, Address, ContactPerson) VALUES 
                ('ООО ""Вектор""', 'г. Москва, ул. Ленина, 15', 'Смирнов А.Б.'),
                ('ЗАО ""Стройтех""', 'г. Москва, пр. Мира, 89', 'Козлова В.С.'),
                ('ИП ""Контакт""', 'г. Москва, ул. Гагарина, 45', 'Федоров П.М.')";
            command.ExecuteNonQuery();

            command.CommandText = @"
                INSERT INTO Products (ProductName, TypeID, Quantity, CostPrice) VALUES 
                ('Ручка шариковая синяя', 1, 150, 15.50),
                ('Ручка гелевая черная', 1, 80, 25.30),
                ('Карандаш чернографитный HB', 2, 200, 8.75),
                ('Карандаш цветной 12 цветов', 2, 45, 120.00),
                ('Бумага А4 80г/м 500л', 3, 30, 280.50),
                ('Папка-скоросшиватель А4', 4, 75, 35.20),
                ('Клей-карандаш 20г', 5, 120, 45.80),
                ('Клей ПВА 250мл', 5, 40, 85.60)";
            command.ExecuteNonQuery();

            command.CommandText = @"
                INSERT INTO Sales (ProductID, ManagerID, FirmID, QuantitySold, UnitPrice, SaleDate) VALUES 
                (1, 1, 1, 50, 25.00, '2024-01-15'),
                (2, 2, 2, 30, 40.00, '2024-01-16'),
                (3, 1, 3, 100, 15.00, '2024-01-17'),
                (4, 3, 1, 20, 150.00, '2024-01-18'),
                (5, 2, 2, 10, 350.00, '2024-01-19'),
                (6, 1, 3, 25, 50.00, '2024-01-20'),
                (7, 3, 1, 40, 65.00, '2024-01-21'),
                (8, 2, 2, 15, 120.00, '2024-01-22')";
            command.ExecuteNonQuery();

            Console.WriteLine("Тестовые данные добавлены успешно!");
        }
    }
}
