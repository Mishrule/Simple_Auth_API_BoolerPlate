using Projects.Identity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Identity.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BuildingType BuildingType { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}
