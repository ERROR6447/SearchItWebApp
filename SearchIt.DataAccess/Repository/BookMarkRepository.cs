﻿using SearchIt.DataAccess.Data;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.DataAccess.Repository
{
    public class BookMarkRepository : Repository<BookMark>, IBookMarkRepository
    {
        private ApplicationDbContext _db;
        public BookMarkRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(BookMark post)
        {
            _db.Update(post);
        }
    }
}
