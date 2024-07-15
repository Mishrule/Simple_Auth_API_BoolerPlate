using System.ComponentModel.DataAnnotations;

namespace Projects.Application.Model.Identity
{
    public class AppUser
  {
    public string Id { get; set; }
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string? Othername { get; set; }
    public string Lastname { get; set; }
    public string? Email { get; set; }
    public string Security { get; set; }
    public string Role { get; set; }
    public string Contact { get; set; }
    public int DepartmentId { get; set; }
    public string? ImageUrl { get; set; }
  }

  public class ForgotPasswordViewModel
  {
    [Required]
    public string Username { get; set; }
    public string Email { get; set; }
    [DataType(DataType.Password)]
    [Display(Name = "New Password")]
    public string NewPassword { get; set; }
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
    [Display(Name = "Confirm password")]
    public string ConfirmPassword { get; set; }
  }
  public class ResetPasswordViewModel
  {
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public string? Code { get; set; }
  }

  public class AppUserRole
  {
    public string UserId { get; set; }
    public string Username { get; set; }
    public IEnumerable<string> Roles { get; set; }
  }

  public class UpdateRole
  {
    public string UserId { get; set; }
    public string RoleId { get; set; }
  }

  public class LockAccount
  {
    public string UserId { get; set; }
    public bool Lock { get; set; }
  }
}
