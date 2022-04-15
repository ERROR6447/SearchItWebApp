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
    public class PostingsRepository : Repository<Postings>, IPostingsRepository
    {
        private ApplicationDbContext _db;
        public PostingsRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Postings post)
        {
            _db.Update(post);
        }
        public int DecrementLike(Postings post, int count)
        {
            post.PostLike -= count;
            return post.PostLike;
        }

        public int IncrementLike(Postings post, int count)
        {
            post.PostLike += count;
            return post.PostLike;
        }
        public int DecrementDisLike(Postings post, int count)
        {
            post.PostDisLike -= count;
            return post.PostDisLike;
        }

        public int IncrementDisLike(Postings post, int count)
        {
            post.PostDisLike += count;
            return post.PostDisLike;
        }
    }
}
