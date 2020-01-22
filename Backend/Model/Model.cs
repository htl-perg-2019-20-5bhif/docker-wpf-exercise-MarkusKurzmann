using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Car
    {
        public int CarID { get; set; }
        public String Make { get; set; }
        public String Model { get; set; }
        public int Year { get; set; }

        public List<Booking> Bookings { get; set; }
    }

    public class Booking
    {

        public int BookingID { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

    }
}
