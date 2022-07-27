using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using I3332Proj.Models;
using I3332Proj.Models.DataModels;
using I3332Proj.Services;

namespace I3332Proj.Pages
{
    public class SendCvModel : PageModel
    {
        private readonly DatabaseContext _context;
        private readonly DatabaseServices _dbService;

        public SendCvModel(DatabaseContext context, DatabaseServices dbService)
        {
            _context = context;
            _dbService = dbService;

            Nationalities = new SelectList(_dbService.GetNationalities());

            ProgrammingSkills = new SelectList(_dbService.GetProgrammingSkills());
        }

        public SelectList Nationalities { get; set; }
        public SelectList ProgrammingSkills { get; set; }
        public string[] Genders = new[] { "Male", "Female" };

        public IActionResult OnGet()
        {
            return Page();
        }


        [BindProperty]
        public CvBindingModel CvBindingModel { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if ((CvBindingModel.X + CvBindingModel.Y) != CvBindingModel.Sum)
            {
                ModelState.AddModelError("CvBindingModel.Sum", "X + Y should be equal to Sum");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _dbService.AddCV(CvBindingModel);

            return RedirectToPage("./Index");
        }
    }
}
