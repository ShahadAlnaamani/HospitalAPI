using LinqToDB.Mapping;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace HospitalAPI.Models
{
    public class Clinic
    {
        [Key]
        public int CID { get; set; }

        [Identity]
        public string Specialization { get; set; }

        [DefaultValue(0)]
        [Range(0, 20)]
        public int NumberofSlots { get; set; }


        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}
