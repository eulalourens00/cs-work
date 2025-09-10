using Microsoft.Data.SqlClient;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace ado.net__sql
{
    internal class Program
    {
        static string insStr = @"Insert into Authors
                (FirstName, LastName) values ('Asweea', 'Asya')";

        static string selStr = "SELECT * FROM Authors";

        static string path = @"Data Source =  (localdb)\MSSQLLocalDB; Initial Catalog = Test;  Integrated Security = True; Connect Timeout = 30; Encrypt = False;  Trust Server Certificate = True; Application Intent = ReadWrite;  Multi Subnet Failover = False";
        //Trust Server Certificate = True; изначально false, надо менять на true
        static void Main(string[] args)
        {
            //using (SqlConnection conn = new SqlConnection(path)) 
            //{ 
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand(insStr, conn);
            //    cmd.ExecuteNonQuery();
            //}

            ReadData2();
        }

        public void InsertQuery()
        {
            using (SqlConnection conn = new SqlConnection(path))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(insStr, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public static void ReadData()
        {
            SqlDataReader reader = null;
            using (SqlConnection conn = new SqlConnection(path))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(selStr, conn);
                reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    Console.WriteLine(reader[1] + " " + reader[2]);
                }
                reader.Close();
            }
        }
        public static void ReadData2()
        {
            SqlDataReader reader = null;
            using (SqlConnection conn = new SqlConnection(path))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select * from Authors; select * from Books", conn);
                reader = cmd.ExecuteReader();
                int line = 0;

                do
                {
                    while (reader.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write(reader.GetName(i).ToString() + "\t");
                            }
                            Console.WriteLine();
                        }
                        line++;
                        Console.Write(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t");
                        Console.WriteLine();
                    }
                    Console.WriteLine($"Total records " + line.ToString());
                }
                while (reader.NextResult());

                reader.Close();
            }
        }
    }
}
