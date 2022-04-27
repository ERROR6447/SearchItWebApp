#nullable disable
#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;
using SearchIt.Models.ViewModels;
using SearchIt.Utility;

namespace SearchItApp.Controllers
{
    public class PostingsController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<ApplicationUser> _UserManager;

        public PostingsController(IUnitOfWork context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _UserManager = userManager;

        }

        // GET: Postings
        public async Task<IActionResult> Index()
        {
            var searchItAppContext= _context.Postings.GetAll(includeProperties:"Company").ToList();
            
            return View(searchItAppContext);
        }

        // GET: Postings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var postings = _context.Postings.GetFirstOrDefault(u=>u.Id == id,includeProperties:"Company");
            
            //var postings = await _context.Postings
            //    .Include(p => p.Company)
           //     .FirstOrDefaultAsync(m => m.Id == id);
            if (postings == null)
            {
                return NotFound();
            }

            return View(postings);
        }

        // GET: Postings/Create
        public IActionResult Create()
        {
            
            
            string userid = _UserManager.GetUserId(User);
            ViewData["Company"] = _context.ApplicationUser.GetFirstOrDefault(u => u.Id == userid).Company;
            Postings post = new Postings()
            {
                ReqSkillsList = _context.KeySkills.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name,
                }).ToList(),
            };
            //ViewData["CompanyName"] = _context.ApplicationUser.GetFirstOrDefault(u => u.Id == userid).Company.CompName;
            
            return View(post);
        }

        // POST: Postings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostName,PostDescription,MinExp,TotalVacancies,PostMinSal,PostMaxSal,PostLike,PostDisLike,CompanyId,TypeOfJob,StreetAddress,City,State,PostalCode,PhoneNumber,CreatedAt,ListKeySkills,ListCategories,SelectedReqSkillList")] Postings postings)
        {
            string user = _UserManager.GetUserId(User);
            postings.CompanyId = _context.ApplicationUser.GetFirstOrDefault(u => u.Id == user).CompanyId;
            postings.ReqSkills = String.Join(',', postings.SelectedReqSkillList);
            if (ModelState.IsValid)
            {
                _context.Postings.Add(postings);
                _context.Save();
                return RedirectToAction("AllPosts");
            }
           
            return NotFound();
        }

        // GET: Postings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postings = _context.Postings.GetFirstOrDefault(u=> u.Id == id);
            if (postings == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = _context.Company.GetAll().Select(u => new SelectListItem
            {
                Text = u.CompName,
                Value = u.Id.ToString(),
            });
            postings.ReqSkillsList = _context.KeySkills.GetAll().Select(i => new SelectListItem
            {
                Value = i.Name,
                Text = i.Name,
            }).ToList();
            postings.SelectedReqSkillList = postings.ReqSkills != null?postings.ReqSkills.Split(',').ToList():new List<string>();
            return View(postings);
        }

        // POST: Postings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostName,PostDescription,MinExp,TotalVacancies,PostMinSal,PostMaxSal,PostLike,PostDisLike,CompanyId,TypeOfJob,StreetAddress,City,State,PostalCode,PhoneNumber,CreatedAt,ListKeySkills,ListCategories,SelectedReqSkillList")] Postings postings)
        {
            if (id != postings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    postings.CompanyId = _context.ApplicationUser.GetFirstOrDefault(u => u.Id == _UserManager.GetUserId(User)).CompanyId;
                    postings.ReqSkills = string.Join(',', postings.SelectedReqSkillList);
                    _context.Postings.Update(postings);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostingsExists(postings.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("AllPosts");
            }
            
            return View(postings);
        }

        // GET: Postings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var postings = _context.Postings.GetFirstOrDefault(u => u.Id == id, includeProperties: "Company");
            //var postings = await _context.Postings
            //    .Include(p => p.Company)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (postings == null)
            {
                return NotFound();
            }

            return View(postings);
        }

        // POST: Postings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postings = _context.Postings.GetFirstOrDefault(u => u.Id == id);    
            //var postings = await _context.Postings.FindAsync(id);
            _context.Postings.Remove(postings);
             _context.Save();
            return RedirectToAction("AllPosts");
        }

        private bool PostingsExists(int id)
        {
            if(_context.Postings.GetFirstOrDefault(e => e.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public IActionResult Like(int PostId)
        {
            Postings post = _context.Postings.GetFirstOrDefault(u => u.Id == PostId);
            _context.Postings.IncrementLike(post, 1);
            _context.Save();
            return Json(new { success= "true", message="Post Liked" });

        }
        public IActionResult DisLike(int PostId)
        {
            Postings post = _context.Postings.GetFirstOrDefault(u => u.Id == PostId);
            _context.Postings.IncrementDisLike(post, 1);
            _context.Save();
            return Json(new { success = "true", message = "Post DisLiked" });

        }
        public IActionResult DecLike(int PostId)
        {
            Postings post = _context.Postings.GetFirstOrDefault(u => u.Id == PostId);
            _context.Postings.DecrementLike(post, 1);
            _context.Save();
            return Json(new { success = "true", message = "Post Un Liked" });

        }
        public IActionResult DecDisLike(int PostId)
        {
            Postings post = _context.Postings.GetFirstOrDefault(u => u.Id == PostId);
            _context.Postings.DecrementDisLike(post, 1);
            _context.Save();
            return Json(new { success = "true", message = "Post DisLike Removed" });

        }

        public IActionResult PostDetails(int id)
        {
            string UserId = _UserManager.GetUserId(User);
            Postings temp = _context.Postings.GetFirstOrDefault(i => i.Id == id, includeProperties: "Company");
            temp.SelectedReqSkillList = temp.ReqSkills.Split(",").ToList();
            PostDetailsViewModel post = new()
            {
                Postings = temp,
                IsApplied = _context.Apply.GetFirstOrDefault(u => u.UserId == UserId && u.PostId == temp.Id) != null ? true : false,
            };

        

            if(post.Postings != null && post.Postings.Company != null)
            {
                return View(post);
            }

            return NotFound();
        }

        public IActionResult AllPosts()
        {
            var UserId = _UserManager.GetUserId(User);
            var UserInfo= _context.ApplicationUser.GetFirstOrDefault(x=>x.Id == UserId);
            ApplicationUser CurUser = _context.ApplicationUser.GetFirstOrDefault(x => x.Id == UserInfo.Id);
            var AllPosts = _context.Postings.GetAll(x => x.CompanyId == CurUser.CompanyId, includeProperties: "Company").ToList();
            for(int i=0; i < AllPosts.Count; i++)
            {
                if (AllPosts[i].ReqSkills != null)
                {
                    AllPosts[i].SelectedReqSkillList = AllPosts[i].ReqSkills.Split(',').ToList();
                }
            }
            IEnumerable<AllPostingsVIewModel> AllCompPosts = from posts in AllPosts
                                                             where posts.CompanyId == CurUser.CompanyId
                                                             select(new AllPostingsVIewModel()
                                                             {
                                                                 Post = posts,
                                                                 Responses = _context.Apply.GetAll(x => x.PostId == posts.Id).Count(),
                                                             });

            return View(AllCompPosts);


        }

        public IActionResult ViewResponses(int id)
        {
            
            return View(id);
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetResponses(int id)
        {
            IEnumerable<ApplyFor> AllResponses = _context.Apply.GetAll(u=>u.PostId == id && u.ApplyStatus != Utility.App_Rejected,includeProperties: "Postings,User") ;

            return Json(new { data=AllResponses });
        }

       

        [HttpDelete]
        public IActionResult RejectApplicant(int id)
        {
            ApplyFor post = _context.Apply.GetFirstOrDefault(u => u.ApplyId == id);
            
            post.ApplyStatus = "Rejected";
            _context.Apply.Update(post);
            _context.Save();
            return Json(new { success = true, message = "Rejected Successfully" });
        }

        [HttpPost]
        public IActionResult BookMarkPost(int id)
        {
            string UserId = _UserManager.GetUserId(User);
            BookMark exists= _context.BookMarks.GetFirstOrDefault(u=>u.UserId == UserId && u.PostId == id);
            if(id != 0 && exists == null)
            {
                BookMark Post = new()
                {
                    UserId = UserId,
                    PostId = id
                };
                _context.BookMarks.Add(Post);
                _context.Save();

                return Json(new { success = "true", message = "BookMarked Post" });
            }
            return Json(new { success = "false", message = "Cannot BookMark Post" });
        }

        [HttpDelete]
        public IActionResult RemoveBookMark(int id)
        {
            string UserId = _UserManager.GetUserId(User);
            BookMark exists = _context.BookMarks.GetFirstOrDefault(u => u.UserId == UserId && u.PostId == id);
            if (id != 0 && exists != null)
            {
                
                _context.BookMarks.Remove(exists);
                _context.Save();

                return Json(new { success = "true", message = "BookMark Removed From Post" });
            }
            return Json(new { success = "false", message = "Cannot Remove BookMark Post" });
        }

        [HttpDelete]
        public IActionResult RemoveBookMarkById(int id)
        {
            string UserId = _UserManager.GetUserId(User);
            BookMark exists = _context.BookMarks.GetFirstOrDefault(u => u.Id == id);
            if (id != 0 && exists != null)
            {

                _context.BookMarks.Remove(exists);
                _context.Save();

                return Json(new { success = "true", message = "BookMark Removed From Post" });
            }
            return Json(new { success = "false", message = "Cannot Remove BookMark Post" });
        }



        #endregion

    }
}
