using HospitalAPI.Models;
using LinqToDB;

namespace HospitalAPI.Repos
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly ApplicationDbContext _context;
        public ClinicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Clinic> GetAllClinics()
        {
            return _context.Clinics.ToList();
        }

        public bool ClinicExists(string Specialization)
        {
            var clinic = _context.Clinics.Where(c => c.Specialization == Specialization).FirstOrDefault();

            if (clinic == null) return false;
            else return true;

        }

        public int AddClinc(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            _context.SaveChanges();
            return clinic.CID;
        }

        public int GetNextSlot(string Specialization)
        {
            Clinic clinic = _context.Clinics.FirstOrDefault(c => c.Specialization == Specialization);

            if (clinic == null) return 0;
            else
            {
                return clinic.NumberofSlots;
            };
        }

        public int GetClinicID(string Specialization)
        {
            Clinic clinic = _context.Clinics.FirstOrDefault(c => c.Specialization == Specialization);

            if (clinic == null) return 0;
            else
            {
                int ID = clinic.CID;
                return ID;
            };

        }

        public Clinic GetClinicByID(int ID)
        {
            Clinic clinic = _context.Clinics.FirstOrDefault(c => c.CID == ID);

            return clinic; 

        }
    }
}
