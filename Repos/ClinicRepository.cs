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

        public bool GetClinic(string Specialization)
        {
            var clinic = _context.Clinics.Where(c => c.Specialization == Specialization);

            if (clinic == null) return false;
            else return true;

        }

        public bool AddClinc(string Specialization, int slots)
        {
            bool exists = GetClinic(Specialization);
            if (!exists)
            {
                var clinic = new Clinic { Specialization = Specialization, NumberofSlots = slots };
                _context.Clinics.Add(clinic);
                _context.SaveChanges();
                return true;
            }

            else { return false; }
        }

        public int GetNextSlot(string Specialization)
        {
            Clinic clinic = _context.Clinics.FirstOrDefault(c => c.Specialization == Specialization);

            if (clinic == null) return 0;
            else
            {
                int TotalSlots = clinic.NumberofSlots;
                return TotalSlots;
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
    }
}
