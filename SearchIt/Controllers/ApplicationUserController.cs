using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;
using SearchIt.Models.ViewModels;

namespace SearchItApp.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserController(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewProfile(int id)
        {
            ApplyFor apply = _unitOfWork.Apply.GetFirstOrDefault(p=>p.ApplyId == id);
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == apply.UserId);
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
    }
}
