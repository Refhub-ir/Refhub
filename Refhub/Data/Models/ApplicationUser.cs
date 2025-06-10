using Microsoft.AspNetCore.Identity;

namespace Refhub.Data.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Book> Books { get; set; }
}
