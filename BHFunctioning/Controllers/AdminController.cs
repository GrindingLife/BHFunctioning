using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BHFunctioning.Data;
using BHFunctioning.Models;

namespace BHFunctioning.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        public readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
            

        }

        public void CheckAdmin()
        {
            if (AdminExistsAsync() == false)
            {
                CreateAdmin();
            }
        }

        private bool CreateAdmin()
        {

            IdentityRole identityRole = new();
            identityRole.Name = "Administrator";
            identityRole.NormalizedName = "Administrator";
            var res = _roleManager.CreateAsync(identityRole);
            if(res != null)
            {
                return true;
            }
            return false;
        }
        private bool AdminExistsAsync()
        {
            bool AdminExist;
            Role admin = new();
            admin.Name = "Administrator";
            var role = _roleManager.FindByNameAsync(admin.Name);

            if(role == null)
            {
                AdminExist = false;
            }
            else
            {
                AdminExist = true;
            }

            return AdminExist;

        }

        //Main page of role management, displays all roles
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult AddRoles()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoles(Role roleObj)
        {
            //System.Diagnostics.Debug.WriteLine("Rolename: " + roleObj.Name);
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByNameAsync(roleObj.Name);

                if (role != null)
                {
                    ModelState.AddModelError("Name", "This role already exists!");

                }

                IdentityRole identityRole = new();
                identityRole.Name = roleObj.Name;
                identityRole.NormalizedName = roleObj.Name;

                var result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                

            }
            
                return View(roleObj);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var roleDb = await _roleManager.FindByIdAsync(id);
            
            EditRoleModel model = new();
            model.Id = roleDb.Id;
            model.Name = roleDb.Name;
            
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, model.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoleAsync(EditRoleModel obj)
        {
            //id.Id is null for some reason no idea
            var newRole = await _roleManager.FindByIdAsync(obj.Id);
            if (newRole == null)
            {
                ViewData["ErrorMessage"] = $"No role with Id '{obj.Id}' was found";
                return View("Error");
            }
            else
            {
                newRole.Name = obj.Name;
                var res = await _roleManager.UpdateAsync(newRole);
                if (res.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }else
                {  
                    ModelState.AddModelError("Name", "Error editing Role");
                }
            }

            return View(newRole);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var getRole = await _roleManager.FindByIdAsync(id);

            EditRoleModel Tempmodel = new();
            Tempmodel.Id = getRole.Id.ToString();
            Tempmodel.Name = getRole.Name;
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, Tempmodel.Name))
                {
                    Tempmodel.Users.Add(user.UserName);
                }
            }
            return View(Tempmodel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(EditRoleModel obj)
        {
            var newRole = await _roleManager.FindByIdAsync(obj.Id);
            if (newRole == null)
            {
                ViewData["ErrorMessage"] = $"No role with Id '{obj.Id}' was found";
                return View("Error");
            }
            else
            {
                //Check if there are any users in the role before deleting
                foreach (var user in _userManager.Users)
                {
                    if (await _userManager.IsInRoleAsync(user, newRole.NormalizedName))
                    {
                        ModelState.AddModelError("Name", "There exists users with this role");
                        return View(obj);
                    }
                }

                //deleteing the role 
                var res = await _roleManager.DeleteAsync(newRole);
                if (res.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    ModelState.AddModelError("Name", "Error deleting Role");
                }
            }

            return View(newRole);
        }

        [HttpGet]
        public async Task<IActionResult> EditRoleUser(string id)
        {
            var roleDb = await _roleManager.FindByIdAsync(id);
            
            List<UserRoleModel> listOfUsersInRole = new();
            ViewData["rID"] = id;
            //Goes through each user and adds them into the list 
            foreach (var user in _userManager.Users)
            {
                UserRoleModel temp = new();
                temp.Id = user.Id;
                temp.Name = user.UserName;
                temp.rName = roleDb.Name;
                temp.rId = roleDb.Id;
                //Checks if the user has the role and if it does, it will check the checkbox
                if(await _userManager.IsInRoleAsync(user, roleDb.Name))
                {
                    temp.IsSelected = true;
                }
                else
                {
                    temp.IsSelected = false;
                }
                listOfUsersInRole.Add(temp);
                
            }
            return View(listOfUsersInRole);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoleUser(List<UserRoleModel> obj)
        {

            var role = await _roleManager.FindByIdAsync(obj[0].rId);
            if(role == null)
            {
                ModelState.AddModelError("", "Role was null");
                return View();
            }
            //goes through each UserRoleModel in the list
            foreach (UserRoleModel user in obj)
            {
                //creates a 
                var _user = await _userManager.FindByIdAsync(user.Id);
                if (user.IsSelected)
                {
                    if (!await _userManager.IsInRoleAsync(_user, role.NormalizedName))
                    {
                        var res = await _userManager.AddToRoleAsync(_user, role.NormalizedName);

                    }
                }
                else
                {
                    if (await _userManager.IsInRoleAsync(_user, user.rName))
                    {
                        var res = await _userManager.RemoveFromRoleAsync(_user, user.rName);

                    }
                }
            }

            return RedirectToAction("ListRoles", new {id = obj[0].rId });
        }

    }
}
