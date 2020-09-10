using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Data;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Call API
        [HttpGet]
        public IActionResult GETAll()
        {
            var userList = _context.ApplicationUsers.Include(u => u.Company).ToList();
            var userRole = _context.UserRoles.ToList();
            var roles = _context.Roles.ToList();
            foreach (var user in userList)
            {
                var roleId = _context.UserRoles.SingleOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = _context.Roles.SingleOrDefault(u => u.Id == roleId).Name;
                if (user.Company==null)
                {
                    user.Company = new Models.Company()
                    {
                        Name = ""
                    };
                }
            }
            return Json(new { data = userList });
        }
        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking / UnLocking" });
            }
            if (objFromDb.LockoutEnd!=null && objFromDb.LockoutEnd>DateTime.Now )
            {
                //currently locked , we will unlocked 
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else 
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _context.SaveChanges();
            return Json(new { success = true, message = "Operation Successful." });
         
        }


        #endregion
    }
}
