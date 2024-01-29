using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend.Pages
{
    public class ONasModel : PageModel
    {
        private readonly ILogger<ONasModel> _logger;

        public ONasModel(ILogger<ONasModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
