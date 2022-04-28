using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RewardsPOC.Entity;
using RewardsPOC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RewardsPOC.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new ApplicationRole(Enums.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(Enums.Roles.Employee.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(Enums.Roles.Manager.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(Enums.Roles.HR.ToString()));
        }

        public static async Task SeedPointsAsync(ApplicationDbContext dbContext)
        {
            var rewardTypes = await dbContext.RewardTypes.ToListAsync();

            if (rewardTypes.Any())
                return;
            RewardTypes Regularization = new RewardTypes
            {
                Name = "Regularization"
            };
            await dbContext.RewardTypes.AddAsync(Regularization);
            RewardTypes Productivity = new RewardTypes
            {
                Name = "Productivity"
            };
            await dbContext.RewardTypes.AddAsync(Productivity);
            RewardTypes InnovativeIdea = new RewardTypes
            {
                Name = "Innovative Idea"
            };
            await dbContext.RewardTypes.AddAsync(InnovativeIdea);
            RewardTypes Response = new RewardTypes
            {
                Name = "Response"
            };
            await dbContext.RewardTypes.AddAsync(Response);
            RewardTypes Inspiring = new RewardTypes
            {
                Name = "Inspiring"
            };
            await dbContext.RewardTypes.AddAsync(Inspiring);
            await dbContext.SaveChangesAsync();
        }

        public static async Task SeedRewards(ApplicationDbContext dbContext)
        {
            var Rewards = await dbContext.Rewards.ToListAsync();

            if(Rewards.Any())
                return ;

            var rewardsData = await System.IO.File.ReadAllTextAsync("Data/Rewards.json");

            var rewards = JsonSerializer.Deserialize<List<Rewards>>(rewardsData).ToList();

            foreach (var reward in rewards)
            {
                await dbContext.Rewards.AddAsync(reward);
            }
            await dbContext.SaveChangesAsync();
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Admin",
                LastName = "A",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    var result = await userManager.CreateAsync(defaultUser, "password");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(defaultUser, Enums.Roles.HR.ToString());
                        await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Manager.ToString());
                        await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Employee.ToString());
                        await userManager.AddToRoleAsync(defaultUser, Enums.Roles.SuperAdmin.ToString());
                    }

                }

            }

            var UsersData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");

            //System.Text.Json -> Available from .net 3
            var Users = JsonSerializer.Deserialize<List<ApplicationUser>>(UsersData).ToList();

            if (Users == null) return;


            foreach (var user in Users)
            {
                var otheruser = await userManager.FindByEmailAsync(user.Email);
                if (otheruser == null)
                {
                    var result = await userManager.CreateAsync(user, "password");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, Enums.Roles.Employee.ToString());
                    }
                }
                else return;

            }


        }
    }
}
