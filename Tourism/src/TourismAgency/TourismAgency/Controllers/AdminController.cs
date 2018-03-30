using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismAgency.Models;
using TourismAgency.Persistence;

namespace TourismAgency.Controllers
{
    public static class AdminController
    {
        public static List<User> Search(string text)
        {
            return UserDao.GetAll(UserLevel.Normal).Where(x => x.Username.Contains(text)).ToList();
        }

        public static User GetUser(int id)
        {
            if (id >= 0) //existing user
            {
                return UserDao.Find(id);
            }
            else if (id == -1) //new user or no user
            {
                return new User();
            }

            return null;
        }

        public static int Save(int userId, string username, string password)
        {
            if (userId >= 0) //existing user
            {
                var u = UserDao.Find(userId);

                u.Username = username;
                u.Password = password;

                UserDao.Update(u);

                return u.Id;
            }
            else if (userId == -1) //new user
            {
                var u = new User();

                u.Username = username;
                u.Password = password;

                u = UserDao.InsertUser(u);

                return u.Id;
            }

            return -1;
        }

        public static void Delete(int userId)
        {
            UserDao.Delete(userId);
        }

        public static void GenerateReport(int id, DateTime from, DateTime until)
        {
            var reservatios = ReservationDao.FindAllSubmittedBy_between(id, from.Ticks, until.Ticks);

            if (reservatios.Count > 0)
            {
                File.WriteAllText("report.txt",
                    String.Join(Environment.NewLine, reservatios.Select(x => x.ToString())));
                Process.Start("report.txt");
            }
        }
    }
}
