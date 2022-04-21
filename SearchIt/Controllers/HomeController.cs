using Microsoft.AspNetCore.Mvc;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;
using System.Diagnostics;

namespace SearchIt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _context;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            //used the below commented code to bulk insert the data into the keyskills data
            //using (StreamReader file = new StreamReader("V:/Projects/SearchItWebApp/SearchItWebApp/SearchIt/wwwroot/SkillsList/list_of_skills.txt"))
            //{
            //    string line;
            //    while ((line = file.ReadLine()) != null)
            //    {
            //        _context.KeySkills.Add(new SearchIt.Models.KeySkills() { Name = line });

            //    }
            //    file.Close();
            //}
            //_context.Save();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}