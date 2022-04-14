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
        void Save();
    }
}
