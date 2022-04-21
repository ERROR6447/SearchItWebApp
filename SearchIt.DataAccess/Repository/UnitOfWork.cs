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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IPostingsRepository Postings { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IApplyForRepository Apply { get; private set; }
        public IOfferRepository Offers { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IKeySkillsRepository KeySkills { get; private set; }
        public IBookMarkRepository BookMarks { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db=db;
            ApplicationUser= new ApplicationUserRepository(_db);
            Postings= new PostingsRepository(_db);
            Company= new CompanyRepository(_db);
            Apply=new ApplyForRepository(_db);
            Offers=new OfferRepository(_db);
            Category=new CategoryRepository(_db);
            KeySkills = new KeySkillsRepository(_db);
            BookMarks = new BookMarkRepository(_db);

        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
