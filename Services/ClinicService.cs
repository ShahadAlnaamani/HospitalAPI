using HospitalAPI.Models;
using HospitalAPI.Repos;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HospitalAPI.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepository;
        private ApplicationDbContext _context;

        public ClinicService(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        public int AddClinic(Clinic clinic)
        {
            if (string.IsNullOrWhiteSpace(clinic.Specialization))
            {
                throw new ArgumentException("Clinic specialization required.");
            }

            if (int.IsNegative(clinic.NumberofSlots))
            {
                throw new ArgumentException("Number of slots must be a whole number.");
            }

            bool exists = _clinicRepository.ClinicExists(clinic.Specialization);
            if (exists)
            {
                throw new ArgumentException("<!>This clinic already exists<!>");
            }

            return _clinicRepository.AddClinc(clinic);
        }

        public List<Clinic> GetAllClinics()
        {
            var clinics = _clinicRepository.GetAllClinics()
                .OrderBy(c => c.Specialization)
                .ToList();
            if (clinics == null || clinics.Count == 0)
            {
                throw new InvalidOperationException("No clinics found.");
            }
            return clinics;
        }

    }
}
