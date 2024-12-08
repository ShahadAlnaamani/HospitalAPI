using HospitalAPI.Models;

namespace HospitalAPI.Repos
{
    public interface IBookingRepository
    {
        int Add(string PatientName, string ClinicSpecialization, DateOnly Date);
        List<Booking> GetAllBookings();
        List<Booking> GetBookingsByDate(DateOnly Date);
        List<Booking> GetBookingsBySlot(int Slot);
    }
}