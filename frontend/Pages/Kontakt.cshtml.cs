using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend.Pages
{
    public class KontaktModel : PageModel
    {
        private readonly ILogger<KontaktModel> _logger;

        public KontaktModel(ILogger<KontaktModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
