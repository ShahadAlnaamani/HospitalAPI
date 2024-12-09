using HospitalAPI.Models;

namespace HospitalAPI.Repos
{
    public interface IPatientRepository
    {
        int AddPatient(Patient patient);
        List<Patient> GetAllPatients();
        int GetPatientID(string name);
        bool PatientExists(string name);
        public Patient GetPatientByName(string name);
    }
}