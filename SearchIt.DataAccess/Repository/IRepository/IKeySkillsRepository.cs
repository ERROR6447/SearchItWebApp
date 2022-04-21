using SearchIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.DataAccess.Repository.IRepository
{
    public interface IKeySkillsRepository: IRepository<KeySkills> 
    {
        void Update(KeySkills obj);
    }
}
