using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;
using SearchIt.Models.ViewModels;
using System.Collections.Generic;

namespace SearchItApp.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserController(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager)
        {
            _context = unitOfWork;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewProfile(int id)
        {
            ApplyFor apply = _context.Apply.GetFirstOrDefault(p=>p.ApplyId == id);
            ApplicationUser user = _context.ApplicationUser.GetFirstOrDefault(x => x.Id == apply.UserId);
            user.PhoneNumber =  await _userManager.GetPhoneNumberAsync(user);
            user.UserName = await _userManager.GetUserNameAsync(user);
            ApplyUserViewModel UserModel = new()
            {
                User = user,
                ApplyForId = id,
                ApplyStatus = apply.ApplyStatus,
            };
            return View(UserModel);
        }

        public IActionResult BookMarks()
        {
            string UserId = _userManager.GetUserId(User);
            List< BookMark > AllBookMarkPosts = _context.BookMarks.GetAll(u => u.UserId == UserId,includeProperties:"Postings").ToList();
            for(int i = 0; i < AllBookMarkPosts.Count; i++)
            {
                AllBookMarkPosts[i].Postings.Company = _context.Company.GetFirstOrDefault(u=>u.Id == AllBookMarkPosts[i].Postings.CompanyId);
            }

            return View(AllBookMarkPosts);
        }
    }
}
