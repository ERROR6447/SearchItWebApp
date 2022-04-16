using Microsoft.AspNetCore.Mvc;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;
using System.Security.Claims;

namespace SearchItApp.Models
{
    public class AppliedForController : Controller
    {
        private readonly IUnitOfWork _context;
        public AppliedForController(IUnitOfWork context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity; // fecthes current identity User
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier); // fetch user Id by claim.Id
            string UserId = claim.Value;
            
            IEnumerable<ApplyFor> AllApplications = _context.Apply.GetAll(u => u.User.Id == UserId, includeProperties: "Postings");
            AllApplications = from post in AllApplications orderby post.AppliedAt descending select post;
            return View(AllApplications);
        }

        public IActionResult ApplyToPost(int PostId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity; // fecthes current identity User
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier); // fetch user Id by claim.Id

            ApplyFor For = new()
            {
                UserId = claim.Value,
                PostId = PostId
            };
            _context.Apply.Add(For);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
