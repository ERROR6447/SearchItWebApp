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
    public class ApplyForRepository : Repository<ApplyFor>, IApplyForRepository
    {
        private ApplicationDbContext _db;
        public ApplyForRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(ApplyFor post)
        {
            _db.Update(post);
        }
    }
}
