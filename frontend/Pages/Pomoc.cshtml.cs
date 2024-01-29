using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend.Pages
{
    public class PomocModel : PageModel
    {
        private readonly ILogger<PomocModel> _logger;

        public PomocModel(ILogger<PomocModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
