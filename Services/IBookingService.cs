using HospitalAPI.Models;

namespace HospitalAPI.Services
{
    public interface IBookingService
    {
        int AddBooking(Booking booking);
        List<Booking> GetAllBookings();
    }
}