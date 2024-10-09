using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.Views.Role
{
    public class delete : PageModel
    {
        private readonly ILogger<delete> _logger;

        public delete(ILogger<delete> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}