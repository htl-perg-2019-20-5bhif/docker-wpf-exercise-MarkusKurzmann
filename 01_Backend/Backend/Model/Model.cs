using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Car
    {
        [JsonProperty("CarID")]
        public int CarID { get; set; }
        [JsonProperty("Make")]
        public String Make { get; set; }
        [JsonProperty("Model")]
        public String Model { get; set; }
        [JsonProperty("Year")]
        public int Year { get; set; }

        [JsonProperty("Bookings")]
        public List<Booking> Bookings { get; set; }
    }


    public class Booking
    {

        public int BookingID { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

    }
}
