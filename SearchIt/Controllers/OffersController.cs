using Microsoft.AspNetCore.Mvc;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;
using SearchIt.Utility;
using System.Security.Claims;

namespace SearchItApp.Controllers
{
    public class OffersController : Controller
    {
        private readonly IUnitOfWork _context;
        public OffersController(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendOffer(int id)
        {
            var ExistingOffer = _context.Offers.GetFirstOrDefault(o => o.ApplyId == id,includeProperties:"Apply");
            
            if ( ExistingOffer != null)
            {
                ExistingOffer.Apply.Postings = _context.Postings.GetFirstOrDefault(u => u.Id == ExistingOffer.Apply.PostId);
                ExistingOffer.Apply.User = _context.ApplicationUser.GetFirstOrDefault(u => u.Id == ExistingOffer.Apply.UserId);
                return View(ExistingOffer);
            }
            ApplyFor offer= _context.Apply.GetFirstOrDefault(u=>u.ApplyId ==id,includeProperties:"User,Postings");
            Offer newoffer = new Offer()
            {
                ApplyId = offer.ApplyId,
                Apply = _context.Apply.GetFirstOrDefault(u => u.ApplyId == id, includeProperties: "User,Postings"),
            };
           // Offer offer = new Offer();
           // offer.ApplyId = _context.Apply.GetFirstOrDefault(u => u.ApplyId == id, includeProperties: "Postings,User").ApplyId;
           // offer.Apply = _context.Apply.GetFirstOrDefault(u => u.ApplyId == id, includeProperties: "Postings,User");
            return View(newoffer);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SendOffer([Bind("Id,ApplyId,Amt,OfferDescrip")] Offer offer)
         {
            if (offer.Id != 0)
            {
                _context.Offers.Update(offer);
            }
            else
            {
                _context.Offers.Add(offer);
                var post = _context.Apply.GetFirstOrDefault(u => u.ApplyId == offer.ApplyId);
                post.ApplyStatus = Utility.App_JobOffer;
                _context.Apply.Update(post);


            }
            _context.Save();
            return RedirectToAction("ViewProfile","ApplicationUser",new { id=offer.ApplyId });
        }


        
        public IActionResult ViewAllOffer()
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Offer> AllOffer = _context.Offers.GetAll(u=>u.Apply.UserId == UserId,includeProperties:"Apply").ToList();
            for(int i = 0; i < AllOffer.Count(); i++)
            {
                AllOffer[i].Apply.Postings = _context.Postings.GetFirstOrDefault(u=>u.Id == AllOffer[i].Apply.PostId);
                AllOffer[i].Apply.User = _context.ApplicationUser.GetFirstOrDefault(u=>u.Id ==AllOffer[i].Apply.UserId);

            }

            return View(AllOffer);
        }

        public IActionResult ViewOffer(int id)
        {
            Offer offer = _context.Offers.GetFirstOrDefault(u=>u.Id == id,includeProperties:"Apply");
            offer.Apply.Postings = _context.Postings.GetFirstOrDefault(u => u.Id == offer.Apply.PostId);
            offer.Apply.Postings.Company = _context.Company.GetFirstOrDefault(u=>u.Id == offer.Apply.Postings.CompanyId);
            return View(offer);
        }


        #region API Calls

        

        #endregion



    }
}
