using HospitalAPI.Models;
using HospitalAPI.Repos;

namespace HospitalAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

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


            bool exists = _patientRepository.PatientExists(patient.Name);
            if (exists)
            {
                throw new ArgumentException("<!>This patient already exists<!>");
            }

            return _patientRepository.AddPatient(patient);
        }

        public List<Patient> GetAllPatients()
        {
            var patients = _patientRepository.GetAllPatients()
                .OrderBy(p => p.Name)
                .ToList();
            if (patients == null || patients.Count == 0)
            {
                throw new InvalidOperationException("No patients found.");
            }
            return patients;
        }
    }
}
