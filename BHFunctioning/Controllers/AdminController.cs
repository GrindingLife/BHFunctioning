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

        //Main page of role management, displays all roles
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return View(users);
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
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            EditUserModel model = new();
            model.Id = user.Id;
            model.Name = user.UserName;
            var inRoles = await _userManager.GetRolesAsync(user);
            foreach (var roles in inRoles)
            {
                model.Users.Add(roles);
            }
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditRoleModel obj)
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
                }
                else
                {
                    ModelState.AddModelError("Name", "Error editing Role");
                }
            }

            return View(newRole);
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
        public async Task<IActionResult> EditRole(EditRoleModel obj)
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
        public async Task<IActionResult> DeleteUser(string id)
        {
            var getUser = await _userManager.FindByIdAsync(id);

            User Tempmodel = new();
            Tempmodel.Id = getUser.Id.ToString();
            Tempmodel.UserName = getUser.UserName;            
            
            return View(Tempmodel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(User obj)
        {
            IdentityUser getUser = await _userManager.FindByIdAsync(obj.Id);
            if (getUser == null)
            {
                ViewData["ErrorMessage"] = $"No User with Id '{obj.Id}' was found";
                return View("Error");
            }
            else
            {
                //Check if there are any user is assigned in any role and unassign them from role before deleting
                var inRoles = await _userManager.GetRolesAsync(getUser);

                if(inRoles != null) 
                { 
                    var resu = await _userManager.RemoveFromRolesAsync(getUser, inRoles);
                    if (!resu.Succeeded)
                    {
                        ModelState.AddModelError("","Error removing roles from user before deleting");
                        return View(obj);
                    }
                }

                //deleteing the user 
                var res = await _userManager.DeleteAsync(getUser);
                if (res.Succeeded)
                {
                    return RedirectToAction("ListUser");
                }
                else
                {
                    ModelState.AddModelError("Name", "Error deleting User");
                }
            }

            return View(getUser);
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

        //Role management for setting users into specific role
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

        //User roles management 
        [HttpGet]
        public async Task<IActionResult> EditUserRole(string id)
        {
            
            var user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = _roleManager.Roles.ToList();
            List<RolesModel> listOfUsersInRole = new();
            ViewData["rID"] = id;
            //Goes through each user and adds them into the list 
            foreach (var role in roles)
            {
                RolesModel temp = new();
                temp.Id = user.Id;
                temp.Name = user.UserName;
                temp.rName = role.Name;
                temp.rId = role.Id;
                //Checks if the user has the role and if it does, it will check the checkbox
                if(await _userManager.IsInRoleAsync(user, role.Name))
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
        public async Task<IActionResult> EditUserRole(List<RolesModel> obj)
        {

            var user = await _userManager.FindByIdAsync(obj[0].Id);
            if (user == null)
            {
                ModelState.AddModelError("", "Role was null");
                return View();
            }
            //goes through each UserRoleModel in the list
            foreach (RolesModel role in obj)
            {
                //creates a 
                if (role.IsSelected)
                {
                    if (!await _userManager.IsInRoleAsync(user, role.rName))
                    {
                        var res = await _userManager.AddToRoleAsync(user, role.rName);
                        if (!res.Succeeded)
                        {
                            ModelState.AddModelError("", "Error adding role from user");
                        }
                    }
                }
                else
                {
                    if (await _userManager.IsInRoleAsync(user, role.rName))
                    {
                        var res = await _userManager.RemoveFromRoleAsync(user, role.rName);
                        if (!res.Succeeded)
                        {
                            ModelState.AddModelError("","Error removing role from user");
                        }
                    }
                }
            }

            return RedirectToAction("ListUsers", new { id = obj[0].rId });
        }

    }
}
