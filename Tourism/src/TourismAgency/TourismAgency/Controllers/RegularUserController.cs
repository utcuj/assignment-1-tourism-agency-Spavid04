using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismAgency.Models;
using TourismAgency.Persistence;

namespace TourismAgency.Controllers
{
    public static class RegularUserController
    {
        public static List<Client> SearchClients(string text)
        {
            return ClientDao.GetAll().Where(x => x.Name.Contains(text)).ToList();
        }

        public static Client GetClient(int id)
        {
            if (id >= 0) //existing user
            {
                return ClientDao.Find(id);
            }
            else if (id == -1) //new user or no user
            {
                return new Client();
            }

            return null;
        }

        public static int SaveClient(int clientId, string name, string icn, string pnc, string address)
        {
            if (clientId >= 0) //existing user
            {
                var c = ClientDao.Find(clientId);

                c.Name = name;
                c.ICN = icn;
                c.PNC = pnc;
                c.Address = address;

                ClientDao.Update(c);

                return c.Id;
            }
            else if (clientId == -1) //new user
            {
                var c = new Client();

                c.Name = name;
                c.ICN = icn;
                c.PNC = pnc;
                c.Address = address;

                c = ClientDao.InsertClient(c);

                return c.Id;
            }

            return -1;
        }

        public static List<Reservation> GetReservationsFor(int clientId)
        {
            return ReservationDao.FindAllBy(clientId);
        }

        public static Reservation GetReservation(int id)
        {
            if (id >= 0) //existing user
            {
                return ReservationDao.Find(id);
            }
            else if (id == -1) //new user or no user
            {
                return new Reservation();
            }

            return null;
        }

        public static int SaveReservation(int reservationId, int clientId, int userId, string destination, string hotel, int people, string details, int totalPrice, int paid, DateTime deadline)
        {
            if (reservationId >= 0) //existing reservation
            {
                var r = ReservationDao.Find(reservationId);

                r.ClientId = clientId;
                r.UserId = userId;
                r.Destination = destination;
                r.HotelName = hotel;
                r.PersonCount = people;
                r.Details = details;
                r.TotalPrice = totalPrice;
                r.PaidAmount = paid;
                r.FinalPaymentDate = deadline;

                ReservationDao.Update(r);

                return r.Id;
            }
            else if (reservationId == -1) //new reservation
            {
                var r = new Reservation();

                r.ClientId = clientId;
                r.UserId = userId;
                r.Destination = destination;
                r.HotelName = hotel;
                r.PersonCount = people;
                r.Details = details;
                r.TotalPrice = totalPrice;
                r.PaidAmount = paid;
                r.FinalPaymentDate = deadline;

                r = ReservationDao.InsertReservation(r);

                return r.Id;
            }

            return -1;
        }

        public static void DeleteReservation(int reservationId)
        {
            ReservationDao.Delete(reservationId);
        }

        public static List<string> GetAllMissedReservations()
        {
            return ReservationDao.GetAllBefore(DateTime.Now.Ticks).Select(x=>$"{x.Id}-{x.ClientId} - {x.Destination} - {x.HotelName}").ToList();
        }
    }
}
