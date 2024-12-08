using HospitalAPI.Models;
using Microsoft.Extensions.FileProviders;

namespace HospitalAPI.Repos
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Booking> GetAllBookings()
        {
            return _context.Bookings.ToList();
        }

        public int Add(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return booking.SlotNumber;
        }

        public List<Booking> GetBookingsByDate(DateOnly Date)
        {
            return _context.Bookings.Where(b => b.Date == Date).ToList();
        }

        public List<Booking> GetBookingsBySlot(int Slot)
        {
            return _context.Bookings.Where(b => b.SlotNumber == Slot).ToList();
        }

    }
}
