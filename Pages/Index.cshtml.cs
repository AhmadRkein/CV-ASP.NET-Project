using I3332Proj.Models.DataModels;
using I3332Proj.Services;
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
        private readonly ExportingServices _exportingServices;
        public IndexModel(ILogger<IndexModel> logger, DatabaseContext context, ExportingServices exportingServices)
        {
            _logger = logger;
            _context = context;
            _exportingServices = exportingServices;
        }

        public void OnGet()
        {
            _exportingServices.CreatePDF(_context.CVs.Find(1));
        }
    }
}
