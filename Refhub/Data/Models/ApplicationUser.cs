using Microsoft.AspNetCore.Identity;

namespace Refhub_Ir.Data.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Book> Books { get; set; }
}
