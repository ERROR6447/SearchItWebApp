using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;
using SearchIt.Models.ViewModels;

namespace SearchItApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IUnitOfWork _context;
        public SearchController(IUnitOfWork context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{

        //    PostingsViewModel PostsList = new()
        //    {
        //        posts = _context.Postings.GetAll(includeProperties: "Company"),
        //        StateList = _context.Company.GetAll().Select(i => new SelectListItem
        //        {
        //            Text = i.State,
        //            Value = i.State
        //        }),
        //        CityList = _context.Company.GetAll().Select(i=> new SelectListItem
        //        {
        //            Text=i.City,
        //            Value=i.City
        //        })
                
        //    };
        //    return View(PostsList);
        //}

        public IActionResult Index(string? SearchState, string? SearchCity, string? search)
        {

            IEnumerable<Postings> temp = _context.Postings.GetAll(includeProperties: "Company");

            if (search != null && search !="")
            {
               temp = from post in temp where post.PostName.Contains(search) select post;

            }
            
            if (SearchState != null && SearchState != "")
            {
                temp = from post in temp where post.State == SearchState select post; 
            }
            if (SearchCity != null && SearchCity != "")
            {
                temp = from post in temp where post.City == SearchCity select post;
            }

            List<String?> tempState = _context.Company.GetAll().Select(x => x.State).Distinct().ToList();
            List<String?> tempCity = _context.Company.GetAll().Select(x=>x.City).Distinct().ToList();

            PostingsViewModel PostsList = new()
            {
                posts=temp,
                //StateList = _context.Company.GetAll().Select(i=> new SelectListItem
                //{
                //    Text = i.State,
                //    Value = i.State,
                //}).Distinct(),
                StateList = tempState.Select(i=> new SelectListItem
                {
                    Text= i,
                    Value= i,
                }),
                CityList = tempCity.Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i,
                }).Distinct()

            };
            return View(PostsList);
        }

       


    }
}
