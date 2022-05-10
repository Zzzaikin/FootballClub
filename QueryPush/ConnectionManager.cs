using MySql.Data.MySqlClient;
using System.Data;

namespace QueryPush
{
    public class ConnectionManager
    {
        private static ConnectionManager _instance;

        private static readonly object _locker = new();

        public MySqlConnection Connection { get; private set; }

        protected ConnectionManager(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public static ConnectionManager GetInstance(string conneсtionString)
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new ConnectionManager(conneсtionString);
                    }
                }
            }

            return _instance;
        }

        public void OpenConnection()
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
        }

        public void CloseConnection()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Close();
        }
    }
}
