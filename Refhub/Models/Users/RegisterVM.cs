using Refhub.Resources;
using System.ComponentModel.DataAnnotations;

namespace Refhub.Models.Users;

public class RegisterVM
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
       @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$",
       ErrorMessageResourceType = typeof(Messages),
       ErrorMessageResourceName = nameof(Messages.Password_Regex_Invalid))]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required(
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.ConfirmPassword_Required)]
    [DataType(DataType.Password)]
    [Compare("Password",
        ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.ConfirmPassword_Compare_Invalid)]
    public required string ConfirmPassword { get; set; }
}
