using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.Models.ViewModels
{
    public class ApplyUserViewModel
    {
        public ApplicationUser User { get; set; }
        public int ApplyForId { get; set; }
        public string ApplyStatus { get; set; }
    }
}
