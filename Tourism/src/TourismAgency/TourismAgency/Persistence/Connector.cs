using System.Data.SqlClient;

namespace TourismAgency.Persistence
{
    public static class Connector
    {
        private const string ConnectionString = @"";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            connection.Open();

            return connection;
        }
    }
}
