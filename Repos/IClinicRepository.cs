using HospitalAPI.Models;

namespace HospitalAPI.Repos
{
    public interface IClinicRepository
    {
        bool AddClinc(string Specialization, int slots);
        List<Clinic> GetAllClinics();
        bool GetClinic(string Specialization);
        int GetClinicID(string Specialization);
        int GetNextSlot(string Specialization);
    }
}