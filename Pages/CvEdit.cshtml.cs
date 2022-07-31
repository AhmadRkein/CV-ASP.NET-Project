using I3332Proj.Models;
using I3332Proj.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace I3332Proj.Pages
{
    public class CvEditModel : PageModel
    {
        private readonly DatabaseServices databaseServices;

        public CvEditModel(DatabaseServices databaseServices)
        {
            this.databaseServices = databaseServices;

            Nationalities = new SelectList(databaseServices.GetNationalities());

            ProgrammingSkills = new SelectList(databaseServices.GetProgrammingSkills());
        }

        public SelectList Nationalities { get; set; }
        public SelectList ProgrammingSkills { get; set; }
        public string[] Genders = new[] { "Male", "Female" };

        [BindProperty]
        public CvEditBindingModel CV { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var cv = await databaseServices.GetCVAsync(id.Value);

            if (cv is null)
                return NotFound();

            CV = new CvEditBindingModel()
            {
                Id = cv.Id,
                FirstName = cv.FirstName,
                LastName = cv.LastName,
                Gender = cv.Gender,
                Email = cv.Email,
                BirthDate = cv.BirthDate,
                Nationality = cv.Nationality,
                Grade = cv.Grade,
                ProgSkills = cv.ProgSkills.Split(';').ToList(),
                PhotoPath = cv.PhotoPath
            };


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./CvEdit", new { id });
            }

            await databaseServices.EditCVAsync(CV);

            return RedirectToPage("./BrowseCVs");
        }
    }
}
