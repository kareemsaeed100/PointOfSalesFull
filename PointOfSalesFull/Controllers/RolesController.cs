using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PointOfSalesFull.Models;
using System.Collections.Generic;
using System.IO;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;
using PointOfSalesFull.Models;

namespace PointOfSalesFull.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public RolesController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {

            return View(db.Roles.ToList());
        }


        // GET: Roles/Details/5
        public ActionResult Details(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Create(ApplicationRole role)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));


            if (!roleManager.RoleExists(role.Name))
            {

                roleManager.Create(role);

            }

            return View("Index", db.Roles);


        }


        // POST: Roles/Create
        //[HttpPost]
        //public ActionResult Create(IdentityRole role)
        //{
        //    // TODO: Add insert logic here
        //    if (ModelState.IsValid)
        //    {
        //        db.Roles.Add(role);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(role);


        //}

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name")]IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {

            var myrole = db.Roles.Find(role.Id);
            db.Roles.Remove(myrole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult UsersWithRoles()
        {

            var usersWithRoles = (from user in db.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in db.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new Users_in_Role_ViewModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  }).ToList();


            return View(usersWithRoles);
        }

        [HttpGet]
        public ActionResult AddRolesToUser()
        {
            ViewBag.RoleId = new SelectList(db.Roles.Where(a => !a.Name.Contains("Admin")).ToList(), "Name", "RolDescrption");
            ViewBag.RolDescrption = new SelectList(db.Roles.Where(a => !a.Name.Contains("Admin")).ToList(), "RolDescrption", "RolDescrption");
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddRolesToUser(UserRoles u, params string[] selectedRoles)
        {
            foreach (var role in selectedRoles)
            {
                var isInRole = await UserManager.IsInRoleAsync(u.UserId, role);
                if (!isInRole)
                {
                    await UserManager.AddToRoleAsync(u.UserId, role);
                }
            }
            ViewBag.RoleId = new SelectList(db.Roles.Where(a => !a.Name.Contains("Admin")).ToList(), "Name", "RolDescrption");
            ViewBag.RolDescrption = new SelectList(db.Roles.Where(a => !a.Name.Contains("Admin")).ToList(), "RolDescrption", "RolDescrption");
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");

            var usersWithRoles = (from user in db.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in db.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new Users_in_Role_ViewModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });


            return View("UsersWithRoles", usersWithRoles);
        }


        //[HttpGet]
        //public ActionResult AssignRolesToUsers()
        //{
        //    AssignRole _addignroles = new AssignRole();
        //    _addignroles.UserRolesList = GetAll_UserRoles();
        //    _addignroles.Userlist = GetAll_Users();
        //    return View(_addignroles);
        //}


        //[HttpGet]
        //public ActionResult AssignRolesToUsers()
        //{
        //    AssignRole _addignroles = new AssignRole();
        //    _addignroles.UserRolesList = GetAll_UserRoles();
        //    _addignroles.Userlist = GetAll_Users();
        //    return View(_addignroles);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AssignRolesToUsers(AssignRole _assignRole)
        //{
        //    if (_assignRole.UserRoleName == "0")
        //    {
        //        ModelState.AddModelError("RoleName", " select UserRoleName");
        //    }
        //    if (_assignRole.UserID == "0")
        //    {
        //        ModelState.AddModelError("UserName", " select Username");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        if (Get_CheckUserRoles(Convert.ToString(_assignRole.UserID)) == true)
        //        {
        //            ViewBag.ResultMessage = "Current user is already has the role";
        //        }
        //        else
        //        {
        //            var UserName = GetUserName_BY_UserID(Convert.ToString(_assignRole.UserID));
        //            var UserRoleName = GetRoleNameByRoleID(Convert.ToString(_assignRole.UserRoleName));
        //            Roles.AddUserToRole(UserName, UserRoleName);
        //            ViewBag.ResultMessage = "Username added to role successfully !";
        //        }
        //        _assignRole.UserRolesList = GetAll_UserRoles();
        //        _assignRole.Userlist = GetAll_Users();
        //        return View(_assignRole);
        //    }
        //    else
        //    {
        //        _assignRole.UserRolesList = GetAll_UserRoles();
        //        _assignRole.Userlist = GetAll_Users();
        //    }
        //    return View(_assignRole);
        //}


        public string GetUserName_BY_UserID(string UserId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var UserName = (from UP in context.Users where UP.Id == UserId select UP.UserName).SingleOrDefault();
                return UserName;
            }
        }

        public string GetRoleNameByRoleID(string RoleId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var roleName = (from UP in context.Roles where UP.Id == RoleId select UP.Name).SingleOrDefault();
                return roleName;
            }
        }

        public bool Get_CheckUserRoles(string UserId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var data = (from WR in context.Roles
                            join R in context.Roles on WR.Id equals R.Id
                            where WR.Id == UserId
                            orderby R.Id
                            select new
                            {
                                WR.Id
                            }).Count();
                if (data > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }




        [NonAction]
        public List<SelectListItem> GetAll_UserRoles()
        {
            List<SelectListItem> listrole = new List<SelectListItem>();
            listrole.Add(new SelectListItem
            {
                Text = "select",
                Value = "0"
            });
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (var item in db.Roles)
                {
                    listrole.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = Convert.ToString(item.Id)
                    });
                }
            }
            return listrole;
        }
        [NonAction]
        public List<SelectListItem> GetAll_Users()
        {
            List<SelectListItem> listuser = new List<SelectListItem>();
            listuser.Add(new SelectListItem
            {
                Text = "Select",
                Value = "0"
            });
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (var item in db.Users)
                {
                    listuser.Add(new SelectListItem
                    {
                        Text = item.UserName,
                        Value = Convert.ToString(item.Id)
                    });
                }
            }
            return listuser;
        }
    }
}