using Projects.Identity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Identity.Models
{
    public class ResidentialBooking
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public Building Building { get; set; }
        public int ApplicationUserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AcceptanceState AcceptanceState { get; set; }
    }
}
