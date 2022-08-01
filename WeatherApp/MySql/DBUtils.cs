using MySql.Data.MySqlClient;

namespace WeatherApp.MySql
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "WeatherDB";
            string username = "root";
            string password = "Aa051333";

            return MySqlUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}