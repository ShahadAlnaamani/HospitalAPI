using HospitalAPI.Repos;
using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace HospitalAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private ApplicationDbContext _context;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public int AddBooking(Booking booking)
        {
            return _bookingRepository.Add(booking);
        }


        public List<Booking> GetAllBookings()
        {
            var bookings = _bookingRepository.GetAllBookings()
                .OrderBy(b => b.Date)
                .ToList();
            if (bookings == null || bookings.Count == 0)
            {
                throw new InvalidOperationException("No bookings found.");
            }
            return bookings;
        }

    }
}
