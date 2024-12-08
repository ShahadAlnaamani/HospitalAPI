using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAPI.Models
{
    [PrimaryKey(nameof(Date), nameof(SlotNumber))]
    public class Booking
    {
        public DateOnly Date {  get; set; }

        public int SlotNumber { get; set; }


        [ForeignKey(nameof(Patient))]
        public int PID { get; set; }
        public virtual Patient Patient { get; set; }


        [ForeignKey(nameof(CID))]
        public int CID { get; set; }
        public virtual Clinic Clinic { get; set; }
    }
}
