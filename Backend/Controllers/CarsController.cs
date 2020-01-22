using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Model;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly BookingContext _context;

        public CarsController(BookingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.CarID)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.CarID }, car);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarID == id);
        }

        [HttpGet("available/{date}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetAvailableCarsForDay(string date)
        {
            //Parse date
            DateTime curDate = DateTime.Parse(date);

            var cars = await _context.Cars.Include(car => car.Bookings).ToListAsync();

            //Get cars via linq-query for current day
            var availableCars = cars.Where(c => !c.Bookings.Any(b => b.RentalDate.Year == curDate.Year && b.RentalDate.Month == curDate.Month && b.RentalDate.Day == curDate.Day)).ToList();

            return Ok(availableCars);
        }

        [HttpPut("{id}/book")]
        public async Task<IActionResult> BookCar(int id, [FromBody] Booking date)
        {
            if (!CarExists(id))
            {
                return NotFound();
            }

            //Get the requested car
            Car carForBooking = await _context.Cars.Include(c => c.Bookings).FirstAsync(c => c.CarID == id);

            DateTime curDate = date.RentalDate;
            if (carForBooking.Bookings.Any(b => b.RentalDate.Year == curDate.Year && b.RentalDate.Month == curDate.Month && b.RentalDate.Day == curDate.Day))
            {
                return BadRequest("Car not available for date " + date.RentalDate.ToShortTimeString());
            }

            carForBooking.Bookings.Add(new Booking { RentalDate = curDate });
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
