using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork  
    {
        //here we declare the interface of all the interface implementation we want in our IUnitOfWork
        IApplicationUserRepository ApplicationUser { get; }
        IPostingsRepository Postings { get; }
        ICompanyRepository Company { get; }
        IApplyForRepository Apply { get; }
        IOfferRepository Offers { get; }
        ICategoryRepository Category { get; }
        IKeySkillsRepository KeySkills { get; }
        IBookMarkRepository BookMarks { get; }
        void Save();
    }
}
