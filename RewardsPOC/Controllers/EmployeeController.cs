using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RewardsPOC.Data;
using RewardsPOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RewardsPOC.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public EmployeeController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<ActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            List<ApplicationUser> result = GetChildren(users, userId);
            return View(result);
        }

        public async Task<JsonResult> GetEmpChartData()
        {
            List<ApplicationUser> empChartList = new List<ApplicationUser>();
            
            var users = await _userManager.Users.ToListAsync();
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            List<ApplicationUser> result = GetChildren(users,userId);
            result.Add(users.Where(x => x.Id == userId).FirstOrDefault());
            foreach (var user in result)
            {
                // Adding new Employee object to List
                empChartList.Add(new ApplicationUser()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    ManagerId = user.ManagerId != null ? user.ManagerId : 0,
                });
            }


            return Json(empChartList);
        }

        public List<ApplicationUser> GetChildren(List<ApplicationUser> users, int parentId)
        {
            return users
                    .Where(c => c.ManagerId == parentId)
                    .Select(c => new ApplicationUser
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        Email = c.Email,
                        ManagerId = c.ManagerId,
                        RewardPoints = c.RewardPoints,
                        Children = GetChildren(users, c.Id)
                    })
                    .ToList();
        }
    }
}
