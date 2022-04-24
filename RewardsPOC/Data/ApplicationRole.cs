using Microsoft.AspNetCore.Identity;

namespace RewardsPOC.Data
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}