using HospitalAPI.Repos;
using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace HospitalAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IClinicRepository _clinicRepository;
        private ApplicationDbContext _context;

        public BookingService(IBookingRepository bookingRepository, IPatientRepository patientRepository, IClinicRepository clinicRepository)
        {
            _bookingRepository = bookingRepository;
            _patientRepository = patientRepository;
            _clinicRepository = clinicRepository;
        }

        public int AddBooking(DateTime date, string patientName, string clinicSpecialization)
        {
            int PatientID = _patientRepository.GetPatientID(patientName);

            //Calculating slot number 
            int clinicID = _clinicRepository.GetClinicID(clinicSpecialization);
            int TakenSlots = _bookingRepository.GetTakenSlots(date, clinicID);
            Clinic Clinic = _clinicRepository.GetClinicByID(clinicID);
            Patient patient = _patientRepository.GetPatientByName(patientName);

            int SlotNumber = TakenSlots + 1;
            
            var booking = new Booking
            {
                Date = date,
                SlotNumber = SlotNumber,
                PID = PatientID,
                CID = clinicID,
            };

            return _bookingRepository.Add(booking);

           

            //}
            //        try
            //{
            //    //    PatientRepository patientRepo = new PatientRepository(_context);
            //    //    ClinicRepository clinicRepo = new ClinicRepository(_context);

            //    if (date >= DateTime.Now) //checks that date is in the future
            //    {

            //    bool patientExists = _patientRepository.PatientExists(patientName);
            //    if (patientExists) //found patient
            //    {
            //        int PatientID = _patientRepository.GetPatientID(patientName);
            //        int TotalSlots = _clinicRepository.GetNextSlot(clinicSpecialization);

            //        if (TotalSlots != 0) //found clinic
            //        {
            //            //Calculating slot number 
            //            int clinicID = _clinicRepository.GetClinicID(clinicSpecialization);
            //            int TakenSlots = _context.Bookings.Count(b => b.CID == clinicID && b.Date == date);
            //            if (TakenSlots < TotalSlots)
            //            {
            //                int SlotNumber = TakenSlots + 1;
                            
            //                    var booking = new Booking {
            //                        Date = date, SlotNumber = SlotNumber, PID = PatientID, CID = clinicID
            //                    };

            //                    return _bookingRepository.Add(booking);
            //                }
            //            else { return BadRequest("<!>No available slots for this date<!>"); }

            //        }
            //        else { return BadRequest("<!>No available slots for this date<!>"); }
            //    }

            //    else { return BadRequest("<!>Invalid patient<!>"); }
            //    }
            //    else { return BadRequest("<!>Date must be in the future<!>"); };

            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            
        }

        public bool PatientExists(string patientName)
        {
            bool patientExists = _patientRepository.PatientExists(patientName);
            if (patientExists) //found patient
            {
                return true;
            }
            else return false;
        }


        public int Validation(DateTime date, string patientName, string clinicSpecialization)
        {
            if (date > DateTime.Now) //checks that date is in the future
            {
                bool patientExists = PatientExists(patientName);
                if (patientExists) //found patient
                {
                    int PatientID = _patientRepository.GetPatientID(patientName);
                    int TotalSlots = _clinicRepository.GetNextSlot(clinicSpecialization);

                    if (TotalSlots != 0) //found clinic
                    {
                        //Calculating slot number 
                        int clinicID = _clinicRepository.GetClinicID(clinicSpecialization);

                        int TakenSlots = _bookingRepository.GetTakenSlots(date, clinicID);
                        
                        //int TakenSlots = _context.Bookings.Count(b => b.CID == clinicID && b.Date == date);

                        if (TakenSlots < TotalSlots)
                        {
                            return 0; //no issues 

                        }
                        else { return 3; } //no slots available 

                    }
                    else { return 3; } //no slots availabe 
                }

                else { return 2; } //patient does not exist
            }
            else { return 1; };//bad date 

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
