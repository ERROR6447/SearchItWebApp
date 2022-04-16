using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;
using SearchIt.Models.ViewModels;

namespace SearchItApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager)
        {
            _context = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllUsers()
        {
            return View();
        }

        public IActionResult AllCompany()
        {
            return View();
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<ApplicationUser> AllUsers = _context.ApplicationUser.GetAll().ToList();
            
            for(int i = 0; i < AllUsers.Count(); i++)
            {
                AllUsers[i].PhoneNumber = await _userManager.GetPhoneNumberAsync(AllUsers[i]);
                AllUsers[i].UserName = await _userManager.GetUserNameAsync(AllUsers[i]);
            }

            return Json(new { data = AllUsers });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompany()
        {

            List<AllCompaniesAdminViewModel> AllCompany = _context.ApplicationUser.GetAll(u => u.CompanyId != null, includeProperties: "Company").Select(i => new AllCompaniesAdminViewModel()
            {
                User =i,
                TotalPosts = _context.Postings.GetAll(p=>p.CompanyId==i.CompanyId).Count(),
            }).ToList();

            for (int i = 0; i < AllCompany.Count(); i++)
            {
                AllCompany[i].User.PhoneNumber = await _userManager.GetPhoneNumberAsync(AllCompany[i].User);
                AllCompany[i].User.UserName = await _userManager.GetUserNameAsync(AllCompany[i].User);

            }


            return Json(new { data = AllCompany });
        }

        [HttpPost]
        public async Task<IActionResult> LockUnlockUser([FromBody] string id)
        {
            var AppUser= _context.ApplicationUser.GetFirstOrDefault(u=>u.Id==id);
            if (AppUser == null)
            {
                return Json(new { success=false,message="No User Found" });
            }
            if(AppUser.LockoutEnd != null && AppUser.LockoutEnd > DateTime.Now)
            {
                AppUser.LockoutEnd = DateTime.Now;
                _context.Save();
                return Json(new { success = true, message = "User UnLocked" });
            }
            else
            {
                AppUser.LockoutEnd= DateTime.Now.AddYears(2000);
                _context.Save();
                return Json(new { success = "true", message = "User Locked" });
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> LockUnlockCompany([FromBody] string id)
        {
            var AppUserComp = _context.ApplicationUser.GetFirstOrDefault(u => u.Id == id,includeProperties:"Company");
            if (AppUserComp == null)
            {
                return Json(new { success = false, message = "No Company Found" });
            }
            if (AppUserComp.LockoutEnd != null && AppUserComp.LockoutEnd > DateTime.Now)
            {
                AppUserComp.LockoutEnd = DateTime.Now;
                _context.Save();
                return Json(new { success = true, message = "Company UnLocked" });
            }
            else
            {
                AppUserComp.LockoutEnd = DateTime.Now.AddYears(2000);
                _context.Save();
                return Json(new { success = "true", message = "Copmany Locked" });
            }

        }

        #endregion
    }
}
