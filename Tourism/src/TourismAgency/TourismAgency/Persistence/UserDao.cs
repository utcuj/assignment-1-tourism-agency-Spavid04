using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismAgency.Models;

namespace TourismAgency.Persistence
{
    public static class UserDao
    {
        public static User InsertUser(User u)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "INSERT INTO USER(username,password,userlevel) VALUES(@username,@password,@userlevel)";
            cmd.Parameters.AddWithValue("@username", u.Username);
            cmd.Parameters.AddWithValue("@password", u.Password);
            cmd.Parameters.AddWithValue("@userlevel", (int)u.UserLevel);

            u.Id = (int) cmd.ExecuteScalar();

            return u;
        }

        public static User Find(int id)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM USER WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);

            User u = null;

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.NextResult())
                {
                    u = new User
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                        UserLevel = (UserLevel) reader.GetInt32(3)
                    };
                }
            }

            return u;
        }

        public static User Find(string username)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM USER WHERE username=@username";
            cmd.Parameters.AddWithValue("@username", username);

            User u = null;

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.NextResult())
                {
                    u = new User
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                        UserLevel = (UserLevel)reader.GetInt32(3)
                    };
                }
            }

            return u;
        }

        public static List<User> GetAll(UserLevel level)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM USER WHERE userlevel=@userlevel";
            cmd.Parameters.AddWithValue("@userlevel", (int)level);

            List<User> users = new List<User>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.NextResult())
                {
                    users.Add(new User
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                        UserLevel = (UserLevel) reader.GetInt32(3)
                    });
                }
            }

            return users;
        }

        public static void Update(User u)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "UPDATE USER SET username=@username,password=@password,userlevel=@userlevel WHERE id=@id";
            cmd.Parameters.AddWithValue("@username", u.Username);
            cmd.Parameters.AddWithValue("@password", u.Password);
            cmd.Parameters.AddWithValue("@userlevel", (int)u.UserLevel);
            cmd.Parameters.AddWithValue("@id", u.Id);

            cmd.ExecuteNonQuery();
        }

        public static void Delete(int id)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "DELETE FROM USER WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
