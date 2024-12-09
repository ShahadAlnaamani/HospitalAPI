using HospitalAPI.Models;
using Microsoft.Extensions.FileProviders;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public List<Booking> GetBookingsByDate(DateTime Date)
        {
            return _context.Bookings.Where(b => b.Date == Date).ToList();
        }

        public List<Booking> GetBookingsBySlot(int Slot)
        {
            return _context.Bookings.Where(b => b.SlotNumber == Slot).ToList();
        }

        public int GetTakenSlots(DateTime date, int clinicID)
        {
            return _context.Bookings.Count(b => b.CID == clinicID && b.Date == date);
        }

    }
}
