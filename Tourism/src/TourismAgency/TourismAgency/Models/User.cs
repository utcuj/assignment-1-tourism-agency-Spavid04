using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismAgency.Models
{
    public enum UserLevel
    {
        Normal = 0,
        Administrator = 1
    }

    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public UserLevel UserLevel { get; set; }

        public override string ToString()
        {
            return $"{this.Username}";
        }
    }
}
