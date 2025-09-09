using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
namespace ado.net_начало
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"here need a path to Data Base";
            string sqlexp = "";
            using (SqlConnection sql = new SqlConnection(path))
            {
                sql.Open();
                    //Console.WriteLine("Enter ur name: ");
                    //string name = Console.ReadLine();
                    

                SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "CREATE table Users";

                    //cmd.CommandText = "Insert into Users(name) Values('{name}')";

                cmd.CommandText = "Select * From Users";
                    
                cmd.Connection = sql;

                //SqlCommand cmd = new SqlCommand(sqlexp, sql);
                cmd.ExecuteNonQuery();
                // cmd.ExecuteReader() - SELECT 
                // cmd.ExecuteScalar() - MIN MAX СКАЛЯРНЫЕ ВЫРАЖЕНИЯ

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    string col1 = reader.GetName(0);
                    string col2 = reader.GetName(1);
                    Console.WriteLine($"{col1}\t{col2}");
                }
                while (reader.Read()) { 
                    object id = reader.GetValue(0);
                    object name = reader.GetValue(1);

                    Console.WriteLine($"{id}\t{name}");
                }
                reader.Close();
            }
        }
    }
}
