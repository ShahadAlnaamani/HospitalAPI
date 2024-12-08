using HospitalAPI.Models;
using HospitalAPI.Repos;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private ApplicationDbContext _context;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }


        [HttpPost("AddBooking")]
        public IActionResult AddBooking(DateOnly date, string patientName, string clinicSpecialization )
        {
            try
            {
                PatientRepository patientRepo = new PatientRepository(_context);
                ClinicRepository clinicRepo = new ClinicRepository(_context);


                bool patientExists = patientRepo.PatientExists(patientName);
                if (patientExists) //found patient
                {
                    int PatientID = patientRepo.GetPatientID(patientName);
                    int TotalSlots = clinicRepo.GetNextSlot(clinicSpecialization);

                    if (TotalSlots != 0) //found clinic
                    {
                        //Calculating slot number 
                        int clinicID = clinicRepo.GetClinicID(clinicSpecialization);
                        int TakenSlots = _context.Bookings.Count(b => b.CID == clinicID && b.Date == date);
                        if (TakenSlots < TotalSlots)
                        {
                            int SlotNumber = TakenSlots + 1;

                            int newBookingId = _bookingService.AddBooking(new Booking
                            {
                                Date = date,
                                SlotNumber = SlotNumber,
                                PID = PatientID,
                                CID = clinicID,
                            });
                            return Created(string.Empty, newBookingId);
                        }
                        else { return BadRequest("<!>No available slots for this date<!>"); }

                    }
                    else { return BadRequest("<!>No available slots for this date<!>"); }
                }
                
                else { return BadRequest("<!>Invalid patient<!>"); }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllBookings")]
        public IActionResult GetAllBookings()
        {
            try
            {
                var admins = _bookingService.GetAllBookings();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
