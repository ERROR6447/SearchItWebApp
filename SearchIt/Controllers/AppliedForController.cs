using Microsoft.AspNetCore.Mvc;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;
using System.Security.Claims;
using SearchIt.Utility;
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
                PostId = PostId,
                ApplyStatus = SearchIt.Utility.Utility.App_Applied
            };
            _context.Apply.Add(For);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }


        #region API Calls
        [HttpPost]
        public IActionResult ViewedProfile(int id)
        {
            var post = _context.Apply.GetFirstOrDefault(u=>u.ApplyId == id);
            if (post.ApplyStatus == Utility.App_Applied)
            {
                post.ApplyStatus = SearchIt.Utility.Utility.App_Viewed;
                _context.Apply.Update(post);
                _context.Save();
            }
            

            return Json(new { success = "true", message="Status Updated Successfully" });
        }

        [HttpPost]
        public IActionResult SortedList(int id)
        {
            var post = _context.Apply.GetFirstOrDefault(u => u.ApplyId == id);
            if(post.ApplyStatus == Utility.App_Viewed)
            {
                post.ApplyStatus = SearchIt.Utility.Utility.App_SortListed;
                _context.Apply.Update(post);
                _context.Save();

            }
            return Json(new { success = "true", message = "Status Updated Successfully" });
        }

        [HttpPost]
        public IActionResult SendOffer(int id)
        {
            var post = _context.Apply.GetFirstOrDefault(u => u.ApplyId == id);
            if(post.ApplyStatus == Utility.App_SortListed || post.ApplyStatus == Utility.App_Viewed || post.ApplyStatus == null)
            {
                post.ApplyStatus = Utility.App_JobOffer;
                _context.Apply.Update(post);
                _context.Save();
               
            }
            return Json(new { success = "true", message = "Offer Sent" });
        }


       public IActionResult AcceptOffer(int id)
        {
            var post=_context.Apply.GetFirstOrDefault(u=>u.ApplyId == id);
            if(post != null)
            {
                var Posting = post.Postings;
                Posting.TotalVacancies -= 1;
                post.ApplyStatus = Utility.App_JobAccept;
                _context.Apply.Update(post);
                _context.Postings.Update(Posting);
                _context.Save();
            }
            
            return Json(new { success = "true", message = "Job Accepted" });
        }

        [HttpPost]
        public IActionResult SortListed(int id)
        {
            ApplyFor post = _context.Apply.GetFirstOrDefault(u=>u.ApplyId==id);
            if(post != null)
            {
                
                post.ApplyStatus = Utility.App_SortListed;
                _context.Apply.Update(post);
                _context.Save();
                return Json(new { success = "true", message = "SortListed" });

            }
            return Json(new { succes = "false", message = "Cannot Sort List Candidate" });
        }



        #endregion

    }
}
