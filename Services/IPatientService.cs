using HospitalAPI.Models;

namespace HospitalAPI.Services
{
    public interface IPatientService
    {
        int AddPatient(Patient patient);
        List<Patient> GetAllPatients();
    }
}