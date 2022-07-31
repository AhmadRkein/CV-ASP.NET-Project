using I3332Proj.Models;
using I3332Proj.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace I3332Proj.Pages
{
    public class BrowseCVsModel : PageModel
    {
        private readonly DatabaseServices databaseServices;

        public BrowseCVsModel(DatabaseServices databaseServices)
        {
            this.databaseServices = databaseServices;
        }

        public List<BrowseCvViewModel> AllCVs { get; set; } = new();
        public async Task OnGetAsync()
        {
            var all = await databaseServices.GetAllCVsAsync();

            AllCVs = all.Select(x => new BrowseCvViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Gender = x.Gender,
                Grade = x.Grade,
            }).ToList();
        }

        [BindProperty]
        public int? Id { get; set; }

        public async Task<IActionResult> OnPostDeleteCV()
        {
            if (Id is null)
                return NotFound();

            await databaseServices.DeleteCVAsync(Id.Value);

            return new OkResult();
        }
    }
}
