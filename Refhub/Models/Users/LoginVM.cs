using Refhub.Resources;
using System.ComponentModel.DataAnnotations;

namespace Refhub.Models.Users;

public class LoginVM
{
    [Required(
       ErrorMessageResourceType = typeof(Messages),
       ErrorMessageResourceName = nameof(Messages.Email_Required))]
    [EmailAddress(
       ErrorMessageResourceType = typeof(Messages),
       ErrorMessageResourceName = nameof(Messages.Email_Format_Invalid))]
    public required string Email { get; set; }

    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Password_Required))]
    [RegularExpression(
       // At least one letter, one digit, 6-64 chars, allow specials.
       @"^(?=.*[A-Za-z])(?=.*\d)[\S]{6,64}$",
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Password_Regex_Invalid))]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Display(
         Name = nameof(Messages.RememberMe_Display),
         ResourceType = typeof(Messages))]
    public bool RememberMe { get; set; }
}
