using SearchIt.DataAccess.Data;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser user)
        {
           var objfromdb = _db.ApplicationUsers.FirstOrDefault(user => user.Id == user.Id);
            if(objfromdb != null)
            {
                objfromdb.Dob = user.Dob;
                objfromdb.Gender = user.Gender;
                objfromdb.Highest_Qualification = user.Highest_Qualification;
                objfromdb.Experience = user.Experience;
                objfromdb.LastPosition = user.LastPosition;
                objfromdb.StreetAddress = user.StreetAddress;
                objfromdb.City = user.City;
                objfromdb.State = user.State;
                objfromdb.PostalCode = user.PostalCode;
                objfromdb.AreaOfInterest = user.AreaOfInterest;
                objfromdb.PreferredLocation = user.PreferredLocation;
                if(user.ImageUrl != null)
                {
                    objfromdb.ImageUrl = user.ImageUrl;
                }
            }
        }
    }
}
