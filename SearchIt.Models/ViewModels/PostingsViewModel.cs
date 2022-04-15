using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.Models.ViewModels
{
    public class PostingsViewModel
    {
        public IEnumerable<Postings> posts { get; set; }
        public string? SearchState { get; set; }
        public string? SearchCity { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? StateList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? CityList { get; set; }
    }
}
