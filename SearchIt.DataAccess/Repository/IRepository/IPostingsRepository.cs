using SearchIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.DataAccess.Repository.IRepository
{
    public interface IPostingsRepository: IRepository<Postings>
    {
        void Update(Postings user);
        int IncrementLike(Postings post, int count);
        int DecrementLike(Postings post, int count);
        int IncrementDisLike(Postings post, int count);
        int DecrementDisLike(Postings post, int count);


    }
}
