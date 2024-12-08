using HospitalAPI.Models;

namespace HospitalAPI.Repos
{
    public interface IClinicRepository
    {
        int AddClinc(Clinic clinic);
        List<Clinic> GetAllClinics();
        bool ClinicExists(string Specialization);
        int GetClinicID(string Specialization);
        int GetNextSlot(string Specialization);
    }
}