using HospitalAPI.Models;

namespace HospitalAPI.Repos
{
    public interface IBookingRepository
    {
        int Add(Booking booking);
        List<Booking> GetAllBookings();
        List<Booking> GetBookingsByDate(DateTime Date);
        List<Booking> GetBookingsBySlot(int Slot);
        public int GetTakenSlots(DateTime date, int clinicID);
    }
}