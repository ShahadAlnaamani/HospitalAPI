using HospitalAPI.Models;

namespace HospitalAPI.Services
{
    public interface IBookingService
    {
        int AddBooking(DateTime date, string patientName, string clinicSpecialization);
        List<Booking> GetAllBookings();
        bool PatientExists(string patientName);
        public int Validation(DateTime date, string patientName, string clinicSpecialization);
    }
}