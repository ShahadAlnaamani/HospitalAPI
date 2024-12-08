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

        public int Add(string PatientName, string ClinicSpecialization, DateOnly Date)
        {
            PatientRepository patientRepo = new PatientRepository(_context);
            ClinicRepository clinicRepo = new ClinicRepository(_context);


            bool patientExists = patientRepo.PatientExists(PatientName);
            if (patientExists) //found patient
            {
                int PatientID = patientRepo.GetPatientID(PatientName);
                int TotalSlots = clinicRepo.GetNextSlot(ClinicSpecialization);

                if (TotalSlots != 0) //found clinic
                {
                    //Calculating slot number 
                    int clinicID = clinicRepo.GetClinicID(ClinicSpecialization);
                    int TakenSlots = _context.Bookings.Count(b => b.CID == clinicID && b.Date == Date);
                    int SlotNumber = TotalSlots - TakenSlots;

                    //Creating new booking 
                    var booking = new Booking { Date = Date, SlotNumber = SlotNumber, CID = clinicID, PID = PatientID };
                    _context.Bookings.Add(booking);
                    _context.SaveChanges();
                    return 1; //Created successfully 
                }

                else
                {
                    return 2; //Invalid clinic  
                }
            }

            else
            {
                return 0; //Invalid patient
            }
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
