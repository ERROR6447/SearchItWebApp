
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
        private readonly IWebHostEnvironment _WebHostEnv;
        public ApplicationUserController(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager, IWebHostEnvironment WebHost)
        {
            _context = unitOfWork;
            _userManager = userManager;
            _WebHostEnv = WebHost;
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

        public IActionResult UploadCv()
        {
            return View();
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult UploadCv(IFormFile? CV)
        {
            string UserId = _userManager.GetUserId(User);
            ApplicationUser CurUser = _context.ApplicationUser.GetFirstOrDefault(u=>u.Id==UserId);
            string Uploads;
            if(CV != null)
            {
                string rootpath = _WebHostEnv.WebRootPath;
                string fileName= Guid.NewGuid().ToString();
                Uploads = Path.Combine(rootpath, @"CVs\");
                var extension = Path.GetExtension(CV.FileName);
                if(CurUser.CvUrl != null)
                {
                    var OldCVPath = Path.Combine(rootpath, CurUser.CvUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(OldCVPath))
                    {
                        System.IO.File.Delete(OldCVPath);
                    }

                
                }

                using(var fileStream = new FileStream(Path.Combine(Uploads,fileName+extension), FileMode.Create))
                {
                    CV.CopyTo(fileStream);
                }
                CurUser.CvUrl = @"\CVs\" + fileName + extension;
                _context.Save();
            }
            
            return View();
        }


        #region API Calls

       
        public  IActionResult DownloadCV(string url)
        {
            string? UserName = _context.ApplicationUser.GetFirstOrDefault(u => u.Id == _userManager.GetUserId(User)).FullName;
            url.Replace(@"\",@"\\");
            var CVPath= Path.Combine(_WebHostEnv.WebRootPath, url.TrimStart('\\'));
            var memory = new MemoryStream();
            string newurl = @"C:\Users\vivek\Desktop\SearchItWebApp\SearchIt\wwwroot\CVs\8e11ec89-e47c-4399-98bd-1aef6e86b54c.pdf";
            using (var stream = new FileStream(newurl, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            if (UserName == null)
            {
                return File(memory, "application/pdf", "SearchItUser_CV.pdf");

            }
            return File(memory, "application/pdf", UserName+"_CV.pdf");
        }


        #endregion


    }
}
