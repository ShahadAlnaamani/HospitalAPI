using HospitalAPI.Models;

namespace HospitalAPI.Services
{
    public interface IClinicService
    {
        int AddClinic(Clinic clinic);
        List<Clinic> GetAllClinics();
    }
}