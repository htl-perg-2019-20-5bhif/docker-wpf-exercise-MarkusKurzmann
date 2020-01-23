using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class Car
    {
        [JsonProperty("carID")]
        public int CarID { get; set; }
        [JsonProperty("make")]
        public String Make { get; set; }
        [JsonProperty("model")]
        public String Model { get; set; }
        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("bookings")] 
        public List<Booking> Bookings { get; set; }
    }

    public class Booking
    {

        public int BookingID { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

    }

    public class BookingDate
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }
}
