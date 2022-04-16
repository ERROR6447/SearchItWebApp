using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;

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

        public async Task<IActionResult> ViewProfile(string id)
        {
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == id);
            user.PhoneNumber =  await _userManager.GetPhoneNumberAsync(user);
            user.UserName = await _userManager.GetUserNameAsync(user);
            return View(user);
        }
    }
}
