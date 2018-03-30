using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismAgency.Models;

namespace TourismAgency.Persistence
{
    public static class ReservationDao
    {
        public static Reservation InsertReservation(Reservation r)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText =
                "INSERT INTO RESERVATION(clientid,userid,destination,hotelname,personcount,details,totalprice,paidamount,finalpaymentdate) VALUES(@clientid,@userid,@destination,@hotelname,@personcount,@details,@totalprice,@paidamount,@finalpaymentdate)";
            cmd.Parameters.AddWithValue("@clientid", r.ClientId);
            cmd.Parameters.AddWithValue("@userid", r.UserId);
            cmd.Parameters.AddWithValue("@destination", r.Destination);
            cmd.Parameters.AddWithValue("@hotelname", r.HotelName);
            cmd.Parameters.AddWithValue("@personcount", r.PersonCount);
            cmd.Parameters.AddWithValue("@details", r.Details);
            cmd.Parameters.AddWithValue("@totalprice", r.TotalPrice);
            cmd.Parameters.AddWithValue("@paidamount", r.PaidAmount);
            cmd.Parameters.AddWithValue("@finalpaymentdate", r.FinalPaymentDate.Ticks); //easier storage in long type

            r.Id = (int) cmd.ExecuteScalar();

            return r;
        }

        public static Reservation Find(int id)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM RESERVATION WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);

            Reservation r = null;

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.NextResult())
                {
                    r = new Reservation
                    {
                        Id = reader.GetInt32(0),
                        ClientId = reader.GetInt32(1),
                        UserId = reader.GetInt32(2),
                        Destination = reader.GetString(3),
                        HotelName = reader.GetString(4),
                        PersonCount = reader.GetInt32(5),
                        Details = reader.GetString(6),
                        TotalPrice = reader.GetInt32(7),
                        PaidAmount = reader.GetInt32(8),
                        FinalPaymentDate = new DateTime(reader.GetInt64(9))
                    };
                }
            }

            return r;
        }

        public static List<Reservation> FindAllBy(int clientId)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM RESERVATION WHERE clientid=@clientid";
            cmd.Parameters.AddWithValue("@clientid", clientId);

            List<Reservation> r = new List<Reservation>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.NextResult())
                {
                    r.Add(new Reservation
                    {
                        Id = reader.GetInt32(0),
                        ClientId = reader.GetInt32(1),
                        UserId = reader.GetInt32(2),
                        Destination = reader.GetString(3),
                        HotelName = reader.GetString(4),
                        PersonCount = reader.GetInt32(5),
                        Details = reader.GetString(6),
                        TotalPrice = reader.GetInt32(7),
                        PaidAmount = reader.GetInt32(8),
                        FinalPaymentDate = new DateTime(reader.GetInt64(9))
                    });
                }
            }

            return r;
        }

        public static List<Reservation> FindAllSubmittedBy_between(int userId, long from, long until)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM RESERVATION WHERE userid=@userid AND finalpaymentdate>=@from AND finalpaymentdate<=@until";
            cmd.Parameters.AddWithValue("@userid", userId);
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@until", until);

            List<Reservation> r = new List<Reservation>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.NextResult())
                {
                    r.Add(new Reservation
                    {
                        Id = reader.GetInt32(0),
                        ClientId = reader.GetInt32(1),
                        UserId = reader.GetInt32(2),
                        Destination = reader.GetString(3),
                        HotelName = reader.GetString(4),
                        PersonCount = reader.GetInt32(5),
                        Details = reader.GetString(6),
                        TotalPrice = reader.GetInt32(7),
                        PaidAmount = reader.GetInt32(8),
                        FinalPaymentDate = new DateTime(reader.GetInt64(9))
                    });
                }
            }

            return r;
        }

        public static List<Reservation> GetAllBefore(long date)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM RESERVATION WHERE finalpaymentdate<=@finalpaymentdate";
            cmd.Parameters.AddWithValue("@finalpaymentdate", date);

            List<Reservation> r = new List<Reservation>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.NextResult())
                {
                    r.Add(new Reservation
                    {
                        Id = reader.GetInt32(0),
                        ClientId = reader.GetInt32(1),
                        UserId = reader.GetInt32(2),
                        Destination = reader.GetString(3),
                        HotelName = reader.GetString(4),
                        PersonCount = reader.GetInt32(5),
                        Details = reader.GetString(6),
                        TotalPrice = reader.GetInt32(7),
                        PaidAmount = reader.GetInt32(8),
                        FinalPaymentDate = new DateTime(reader.GetInt64(9))
                    });
                }
            }

            return r;
        }

        public static void Update(Reservation r)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "UPDATE RESERVATION SET clientid=@clientid,destination=@destination,hotelname=@hotelname,personcount=@personcount,details=@details,totalprice=@totalprice,paidamount=@paidamount,finalpaymentdate=@finalpaymentdate WHERE id=@id";
            cmd.Parameters.AddWithValue("@clientid", r.ClientId);
            cmd.Parameters.AddWithValue("@destination", r.Destination);
            cmd.Parameters.AddWithValue("@hotelname", r.HotelName);
            cmd.Parameters.AddWithValue("@personcount", r.PersonCount);
            cmd.Parameters.AddWithValue("@details", r.Details);
            cmd.Parameters.AddWithValue("@totalprice", r.TotalPrice);
            cmd.Parameters.AddWithValue("@paidamount", r.PaidAmount);
            cmd.Parameters.AddWithValue("@finalpaymentdate", r.FinalPaymentDate.Ticks);
            cmd.Parameters.AddWithValue("@id", r.Id);

            cmd.ExecuteNonQuery();
        }

        public static void Delete(int id)
        {
            SqlConnection connection = Connector.GetConnection();

            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "DELETE FROM RESERVATION WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
