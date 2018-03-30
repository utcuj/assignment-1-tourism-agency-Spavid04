using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismAgency.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public int UserId { get; set; }
        public string Destination { get; set; }
        public string HotelName { get; set; }
        public int PersonCount { get; set; }
        public string Details { get; set; }
        public int TotalPrice { get; set; }
        public int PaidAmount { get; set; }
        public DateTime FinalPaymentDate { get; set; }

        public override string ToString()
        {
            return $"{this.Destination} - {this.HotelName}";
        }
    }
}
