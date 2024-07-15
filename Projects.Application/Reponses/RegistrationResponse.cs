using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Application.Reponses
{
    public class RegistrationResponse
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string? Othername { get; set; }
        public string Lastname { get; set; }
        public string? Email { get; set; }
    }
}
