using HospitalAPI.Models;

namespace HospitalAPI.Repos
{
    public interface IBookingRepository
    {
        int Add(Booking booking);
        List<Booking> GetAllBookings();
        List<Booking> GetBookingsByDate(DateOnly Date);
        List<Booking> GetBookingsBySlot(int Slot);
    }
}