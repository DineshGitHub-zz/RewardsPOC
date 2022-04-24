using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RewardsPOC.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }
        public int? ManagerId { get; set; }
        public int RewardPoints { get; set; }
        public virtual ApplicationUser Parent { get; set; }
        public virtual ICollection<ApplicationUser> Children { get; set; }
    }
}
