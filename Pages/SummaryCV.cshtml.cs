using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using I3332Proj.Models.DataModels;
using I3332Proj.Models;
using I3332Proj.Services;

namespace I3332Proj.Pages
{
    public class SummaryCVModel : PageModel
    {
        private readonly ExportingServices _exportingServices;
        private readonly DatabaseServices _dbServices;

        public SummaryCVModel(ExportingServices exportingServices, DatabaseServices dbServices)
        {
            _exportingServices = exportingServices;
            _dbServices = dbServices;
        }
        [BindProperty(SupportsGet =true)]
        public int? Id { get; set; }

        public SummaryCvViewModel CvViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == null)
            {
                return NotFound();
            }

            var cv = await _dbServices.GetCV(Id.Value);

            if (cv == null)
            {
                return NotFound();
            }

            CvViewModel = new SummaryCvViewModel()
            {
                BirthDate = cv.BirthDate,
                Email = cv.Email,
                FirstName = cv.FirstName,
                LastName = cv.LastName,
                Gender = cv.Gender,
                Grade = cv.Grade,
                Nationality = cv.Nationality,
                ProgSkills = cv.ProgSkills.Replace(";", ", "),
                PhotoPath = cv.PhotoPath,
            };


            return Page();
        }

        public async Task<IActionResult> OnGetPDF()
        {
            if (Id == null)
            {
                return NotFound();
            }

            var content = await _exportingServices.CreatePDF(Id.Value);

            if (content == null)
            {
                return NotFound();
            }

            return new FileContentResult(content, "application/pdf");
        }
    }
}
