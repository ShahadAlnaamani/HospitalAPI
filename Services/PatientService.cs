using HospitalAPI.Models;
using HospitalAPI.Repos;

namespace HospitalAPI.Services
{
    public class PatientService
    {
        private readonly IPatientRepository _patientRepository;
        private ApplicationDbContext _context;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public int AddPatient(Patient patient)
        {
            if (string.IsNullOrWhiteSpace(patient.Name))
            {
                throw new ArgumentException("<!>Patient name required<!>");
            }

            if (int.IsNegative(patient.Age))
            {
                throw new ArgumentException("<!>Age must be a whole number<!>");
            }

            var patientRepo = new PatientRepository(_context);

            bool exists = patientRepo.PatientExists(patient.Name);
            if (exists) 
            {
                throw new ArgumentException("<!>This patient already exists<!>");
            }

            return _patientRepository.AddPatient(patient);
        }

        public List<Patient> GetAllPatients()
        {
            var patients = _patientRepository.GetAllPatients()
                .OrderBy(p=>p.Name)
                .ToList();
            if (patients == null || patients.Count == 0)
            {
                throw new InvalidOperationException("No patients found.");
            }
            return patients;
        }
    }
}
