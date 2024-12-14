using Microsoft.AspNetCore.Identity;

namespace Notes_Manager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Notes> Notes { get; set; }
        public virtual ICollection<Category> Category { get; set; }
    }
}
