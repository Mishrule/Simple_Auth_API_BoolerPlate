using System.ComponentModel.DataAnnotations;

namespace Projects.Application.Model.Identity
{
    public class RegistrationRequest
    {
        [Required] public string FirstName { get; set; }
        
        [Required] public string LastName { get; set; }
        [Required] public string Othername { get; set; }

        [Required][EmailAddress] public string Email { get; set; }

        [Required][MinLength(6)] public string UserName { get; set; }
        //[Required]  public string? PhoneNumber { get; set; }

        [Required][MinLength(6)] public string Password { get; set; }
        [Required] public string PhoneNumber { get; set; }
    }
}
