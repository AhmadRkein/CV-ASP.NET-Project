using I3332Proj.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace I3332Proj.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DatabaseContext _context;

        public IndexModel(ILogger<IndexModel> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {

        }
    }
}
