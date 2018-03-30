using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismAgency.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ICN { get; set; }
        public string PNC { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}
