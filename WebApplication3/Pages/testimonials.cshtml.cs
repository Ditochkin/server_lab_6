using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesGeneral.Pages
{
    public class testimonialsModel : PageModel
    {
        private ITestimonialService _service;
        public IEnumerable<Testimonial> Testimonials { get; set; }
        public testimonialsModel(ITestimonialService service)
        {
            this._service = service;
            Testimonials = service.getAll();
        }
        public void OnGet()
        {
        }
    }
}
