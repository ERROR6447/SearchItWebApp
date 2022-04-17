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
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        private ApplicationDbContext _db;
        public OfferRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Offer post)
        {
            _db.Update(post);
        }
    }
}
