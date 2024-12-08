using HospitalAPI.Models;

namespace HospitalAPI.Repos
{
    public interface IPatientRepository
    {
        bool AddPatient(string name, int age, Gender gender);
        List<Patient> GetAllPatients();
        int GetPatientID(string name);
        bool PatientExists(string name);
    }
}