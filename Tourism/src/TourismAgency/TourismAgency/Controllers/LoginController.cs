using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismAgency.Models;
using TourismAgency.Persistence;
using TourismAgency.Views;

namespace TourismAgency.Controllers
{
    public static class LoginController
    {
        public static int TryLogin(string username, string password, out UserLevel level)
        {
            User u = UserDao.Find(username);
            level = UserLevel.Normal;

            if (u == null)
            {
                return -1;
            }
            else
            {
                if (u.Password == password)
                {
                    level = u.UserLevel;
                    return u.Id;
                }
                else
                {
                    return -1;
                }
            }
        }

        public static void Login(UserLevel level, int userId)
        {
            switch (level)
            {
                case UserLevel.Normal:
                    RegularUserForm reg = new RegularUserForm(userId);
                    reg.ShowDialog();
                    break;
                case UserLevel.Administrator:
                    AdminForm adm = new AdminForm();
                    adm.ShowDialog();
                    break;
            }

            Environment.Exit(0);
        }
    }
}
