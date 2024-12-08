using HospitalAPI.Repos;
using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace HospitalAPI.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private ApplicationDbContext _context;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
       
        public int AddBooking (Booking booking) 
        {
            PatientRepository patientRepo = new PatientRepository(_context);
            ClinicRepository clinicRepo = new ClinicRepository(_context);


            bool patientExists = patientRepo.PatientExists(booking.Patient.Name);
            if (patientExists) //found patient
            {
                int PatientID = patientRepo.GetPatientID(booking.Patient.Name);
                int TotalSlots = clinicRepo.GetNextSlot(booking.Clinic.Specialization);

                if (TotalSlots != 0) //found clinic
                {
                    //Calculating slot number 
                    int clinicID = clinicRepo.GetClinicID(booking.Clinic.Specialization);
                    int TakenSlots = _context.Bookings.Count(b => b.CID == clinicID && b.Date == Date);
                    if (TakenSlots < TotalSlots)
                    {
                        int SlotNumber = TakenSlots + 1;
                        booking.SlotNumber = SlotNumber;

                        //Creating new booking 
                        return _bookingRepository.Add(booking); 
                    }
                    else
                    {
                        throw new ArgumentException("<!>No slots available on this day<!>");
                    }
                }

                else
                {
                    throw new ArgumentException("<!>Invalid clinic<!>");
                }
            }

            else
            {
                throw new ArgumentException("<!>Invalid patient<!>");
            }
        }


        public List<Booking> GetAllBookings()
        {
            var bookings = _bookingRepository.GetAllBookings()
                .OrderBy(b=> b.Date)
                .ToList();
            if (bookings == null || bookings.Count == 0)
            {
                throw new InvalidOperationException("No bookings found.");
            }
            return bookings;
        }

    }
}
