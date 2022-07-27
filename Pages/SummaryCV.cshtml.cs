using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using I3332Proj.Models.DataModels;
using I3332Proj.Models;

namespace I3332Proj.Pages
{
    public class SummaryCVModel : PageModel
    {
        private readonly I3332Proj.Models.DataModels.DatabaseContext _context;

        public SummaryCVModel(I3332Proj.Models.DataModels.DatabaseContext context)
        {
            _context = context;
        }

        public SummaryCvViewModel CvViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cv = await _context.CVs.FirstOrDefaultAsync(m => m.Id == id);

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
                ProgSkills = cv.ProgSkills.Replace(";",", "),
                PhotoPath = cv.PhotoPath,
            };


            return Page();
        }
    }
}
