using Microsoft.AspNetCore.Identity;
using Refhub_Ir.Models;

namespace Refhub_Ir.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Book>Books { get; set; }
    }
}
