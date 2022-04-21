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
    public class KeySkillsRepository : Repository<KeySkills>, IKeySkillsRepository
    {
        private ApplicationDbContext _db;
        public KeySkillsRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(KeySkills post)
        {
            _db.Update(post);
        }
    }
}
