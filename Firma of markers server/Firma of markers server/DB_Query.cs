using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Firma_of_markers_server
{

    // начинаем наше лудо-приключение
    // да-да нет-нет да будет свет
    // нет-нет да-да
    internal class DB_Query
    {
        private SqliteConnection _connection;

        public DB_Query(SqliteConnection connection)
        {
            _connection = connection;
        }

        public void DisplayAllProducts()
        {
            string query = @"
                SELECT p.ProductID, p.ProductName, t.TypeName, p.Quantity, p.CostPrice
                FROM Products p
                INNER JOIN ProductTypes t ON p.TypeID = t.TypeID
                ORDER BY p.ProductName";

            ExecuteAndDisplayQuery(query, "Все канцтовары:");
        }

        public void DisplayAllProductTypes()
        {
            string query = "SELECT TypeID, TypeName FROM ProductTypes ORDER BY TypeName";
            ExecuteAndDisplayQuery(query, "Все типы канцтоваров:");
        }

        public void DisplayAllManagers()
        {
            string query = "SELECT ManagerID, ManagerName, Phone, Email FROM Managers ORDER BY ManagerName";
            ExecuteAndDisplayQuery(query, "Все менеджеры по продажам:");
        }

        public void DisplayProductsWithMaxQuantity()
        {
            string query = @"
                SELECT p.ProductID, p.ProductName, t.TypeName, p.Quantity, p.CostPrice
                FROM Products p
                INNER JOIN ProductTypes t ON p.TypeID = t.TypeID
                WHERE p.Quantity = (SELECT MAX(Quantity) FROM Products)";

            ExecuteAndDisplayQuery(query, "Канцтовары с максимальным количеством единиц:");
        }

        public void DisplayProductsWithMinQuantity()
        {
            string query = @"
                SELECT p.ProductID, p.ProductName, t.TypeName, p.Quantity, p.CostPrice
                FROM Products p
                INNER JOIN ProductTypes t ON p.TypeID = t.TypeID
                WHERE p.Quantity = (SELECT MIN(Quantity) FROM Products)";

            ExecuteAndDisplayQuery(query, "Канцтовары с минимальным количеством единиц:");
        }

        public void DisplayProductsWithMinCostPrice()
        {
            string query = @"
                SELECT p.ProductID, p.ProductName, t.TypeName, p.Quantity, p.CostPrice
                FROM Products p
                INNER JOIN ProductTypes t ON p.TypeID = t.TypeID
                WHERE p.CostPrice = (SELECT MIN(CostPrice) FROM Products)";

            ExecuteAndDisplayQuery(query, "Канцтовары с минимальной себестоимостью:");
        }

        public void DisplayProductsWithMaxCostPrice()
        {
            string query = @"
                SELECT p.ProductID, p.ProductName, t.TypeName, p.Quantity, p.CostPrice
                FROM Products p
                INNER JOIN ProductTypes t ON p.TypeID = t.TypeID
                WHERE p.CostPrice = (SELECT MAX(CostPrice) FROM Products)";

            ExecuteAndDisplayQuery(query, "Канцтовары с максимальной себестоимостью:");
        }

        public void DisplayProductsByType(string typeName)
        {
            string query = @"
                SELECT p.ProductID, p.ProductName, t.TypeName, p.Quantity, p.CostPrice
                FROM Products p
                INNER JOIN ProductTypes t ON p.TypeID = t.TypeID
                WHERE t.TypeName = @TypeName
                ORDER BY p.ProductName";

            ExecuteAndDisplayQueryWithParameter(query, $"Канцтовары типа: {typeName}",
                new SqliteParameter("@TypeName", typeName));
        }

        public void DisplayProductsSoldByManager(string managerName)
        {
            string query = @"
                SELECT DISTINCT p.ProductID, p.ProductName, t.TypeName, p.Quantity, p.CostPrice
                FROM Products p
                INNER JOIN ProductTypes t ON p.TypeID = t.TypeID
                INNER JOIN Sales s ON p.ProductID = s.ProductID
                INNER JOIN Managers m ON s.ManagerID = m.ManagerID
                WHERE m.ManagerName = @ManagerName
                ORDER BY p.ProductName";

            ExecuteAndDisplayQueryWithParameter(query, $"Канцтовары, проданные менеджером: {managerName}",
                new SqliteParameter("@ManagerName", managerName));
        }

        public void DisplayProductsBoughtByFirm(string firmName)
        {
            string query = @"
                SELECT DISTINCT p.ProductID, p.ProductName, t.TypeName, p.Quantity, p.CostPrice
                FROM Products p
                INNER JOIN ProductTypes t ON p.TypeID = t.TypeID
                INNER JOIN Sales s ON p.ProductID = s.ProductID
                INNER JOIN CustomerFirms cf ON s.FirmID = cf.FirmID
                WHERE cf.FirmName = @FirmName
                ORDER BY p.ProductName";

            ExecuteAndDisplayQueryWithParameter(query, $"Канцтовары, купленные фирмой: {firmName}",
                new SqliteParameter("@FirmName", firmName));
        }

        public void DisplayMostRecentSale()
        {
            string query = @"
                SELECT s.SaleID, p.ProductName, m.ManagerName, cf.FirmName, 
                       s.QuantitySold, s.UnitPrice, s.SaleDate
                FROM Sales s
                INNER JOIN Products p ON s.ProductID = p.ProductID
                INNER JOIN Managers m ON s.ManagerID = m.ManagerID
                INNER JOIN CustomerFirms cf ON s.FirmID = cf.FirmID
                ORDER BY s.SaleDate DESC, s.SaleID DESC
                LIMIT 1";

            ExecuteAndDisplayQuery(query, "Самая недавняя продажа:");
        }

        public void DisplayAverageQuantityByType()
        {
            string query = @"
                SELECT t.TypeName, 
                       AVG(p.Quantity) as AverageQuantity
                FROM Products p
                INNER JOIN ProductTypes t ON p.TypeID = t.TypeID
                GROUP BY t.TypeID, t.TypeName
                ORDER BY t.TypeName";

            ExecuteAndDisplayQuery(query, "Среднее количество товаров по типам:");
        }

        // господи
        private void ExecuteAndDisplayQuery(string query, string header)
        {
            try
            {
                using (var command = new SqliteCommand(query, _connection))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine($"\n{header}");
                    Console.WriteLine(new string('-', 80));

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("Данные не найдены.");
                        return;
                    }

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader.GetName(i),-20} ");
                    }
                    Console.WriteLine();
                    Console.WriteLine(new string('-', 80));

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string value = reader[i].ToString();
                            Console.Write($"{value,-20} ");
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private void ExecuteAndDisplayQueryWithParameter(string query, string header, SqliteParameter parameter)
        {
            try
            {
                using (var command = new SqliteCommand(query, _connection))
                {
                    command.Parameters.Add(parameter);
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine($"\n{header}");
                        Console.WriteLine(new string('-', 80));

                        if (!reader.HasRows)
                        {
                            Console.WriteLine("Данные не найдены.");
                            return;
                        }

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"{reader.GetName(i),-20} ");
                        }
                        Console.WriteLine();
                        Console.WriteLine(new string('-', 80));

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string value = reader[i].ToString();
                                Console.Write($"{value,-20} ");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
        }
    }
}
