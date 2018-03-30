using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismAgency.Models;

namespace TourismAgency.Persistence
{
    public static class ClientDao
    {
        public static Client InsertClient(Client c)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "INSERT INTO CLIENT(name,icn,pnc,address) VALUES(@name,@icn,@pnc,@address); SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@name", c.Name);
            cmd.Parameters.AddWithValue("@icn", c.ICN);
            cmd.Parameters.AddWithValue("@pnc", c.PNC);
            cmd.Parameters.AddWithValue("@address", c.Address);

            c.Id = Convert.ToInt32(cmd.ExecuteScalar());

            return c;
        }

        public static Client Find(int id)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM CLIENT WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);

            Client c = null;

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    c = new Client
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        ICN = reader.GetString(2),
                        PNC = reader.GetString(3),
                        Address = reader.GetString(4)
                    };
                }
            }

            return c;
        }

        public static Client Find(string name)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM CLIENT WHERE name=@name";
            cmd.Parameters.AddWithValue("@name", name);

            Client c = null;

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    c = new Client
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        ICN = reader.GetString(2),
                        PNC = reader.GetString(3),
                        Address = reader.GetString(4)
                    };
                }
            }

            return c;
        }

        public static List<Client> GetAll()
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM CLIENT";

            List<Client> clients = new List<Client>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    clients.Add(new Client
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        ICN = reader.GetString(2),
                        PNC = reader.GetString(3),
                        Address = reader.GetString(4)
                    });
                }
            }

            return clients;
        }

        public static void Update(Client c)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "UPDATE CLIENT SET name=@name,icn=@icn,pnc=@pnc,address=@address WHERE id=@id";
            cmd.Parameters.AddWithValue("@name", c.Name);
            cmd.Parameters.AddWithValue("@icn", c.ICN);
            cmd.Parameters.AddWithValue("@pnc", c.PNC);
            cmd.Parameters.AddWithValue("@address", c.Address);
            cmd.Parameters.AddWithValue("@id", c.Id);

            cmd.ExecuteNonQuery();
        }

        public static void Delete(int id)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "DELETE FROM CLIENT WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
