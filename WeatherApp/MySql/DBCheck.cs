using System;
using MySql.Data.MySqlClient;

namespace WeatherApp.MySql
{
    public class DBCheck
    {
        public static bool CheckCity(string cityName)
        {
            string sql = "SELECT COUNT(*) FROM cities WHERE city_name = @a";

            Console.WriteLine("Getting Connection ...");
            
            MySqlConnection conn = DBUtils.GetDBConnection();

            conn.Open();

            Console.WriteLine("Connection Opened.");

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@a", cityName);
                var result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result > 0)
                {
                    Console.WriteLine("City Checked. Returning TRUE.");
                    return true;
                }
                else
                {
                    Console.WriteLine("City Checked. Returning FALSE.");
                    return false;
                }
            }
        }
    }
}

