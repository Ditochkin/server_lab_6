using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesGeneral.Pages
{
    public class featuresModel : PageModel
    {
        private IFeatureService _service;
        public IEnumerable<Feature> Features { get; set; }
        public featuresModel(IFeatureService service)
        {
            this._service = service;
            Features = service.getAll();
        }

        public void OnGet()
        {
        }
    }
}
