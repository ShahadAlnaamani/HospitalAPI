using HospitalAPI.Models;

namespace HospitalAPI.Repos
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }

        public bool PatientExists(string name) //checks if patient exists
        {
           var  patient = _context.Patients.Where(c => c.Name == name);

            if (name == null) return false;
            else return true;

        }

        public int AddPatient(Patient patient)
        {
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return patient.PID;
        }

        public int GetPatientID(string name)
        {
            Patient patient = _context.Patients.FirstOrDefault(c => c.Name == name);

            if (name == null) return 0;
            else return patient.PID;

        }
    }
}
