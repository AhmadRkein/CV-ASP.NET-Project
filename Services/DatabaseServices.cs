using I3332Proj.Models;
using I3332Proj.Models.DataModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace I3332Proj.Services
{
    public class DatabaseServices
    {
        private readonly DatabaseContext _context;
        private readonly GradingService _grading;
        private readonly IWebHostEnvironment webHostEnvironment;
        public DatabaseServices(DatabaseContext context, GradingService grading, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _grading = grading;
            this.webHostEnvironment = webHostEnvironment;
        }

        public List<string> GetNationalities()
        {
            return _context.Nationalities.Select(x => x.Name).ToList();
        }
        
        public List<string> GetProgrammingSkills()
        {
            return _context.ProgrammingSkills.Select(x => x.Name).ToList();
        }

        public CV AddCV(CvBindingModel cvBinding)
        {
            var ImagesFolder = "Images";
            var ImagesFullPath = Path.Combine(webHostEnvironment.WebRootPath, ImagesFolder);
            Directory.CreateDirectory(ImagesFullPath);

            var fileExt = Path.GetExtension(cvBinding.Photo.FileName);
            var fileName = Guid.NewGuid().ToString() + fileExt;
            var imageFullPath = Path.Combine(ImagesFullPath, fileName);
            var imageRelPath = Path.Combine(ImagesFolder, fileName);
            using (var filestream = new FileStream(imageFullPath, FileMode.Create)) 
            {
                cvBinding.Photo.CopyTo(filestream);
            }
            
            var dbCV = new CV()
            {
                BirthDate = cvBinding.BirthDate,
                Email = cvBinding.Email,
                FirstName = cvBinding.FirstName,
                LastName = cvBinding.LastName,
                Gender = cvBinding.Gender,
                Nationality = cvBinding.Nationality,
                ProgSkills = string.Join(";", cvBinding.ProgSkills),
                Grade = _grading.CalculateGrade(cvBinding),
                PhotoPath= imageRelPath,
            };

            _context.Add(dbCV);
            _context.SaveChanges();

            return dbCV;
        }
    
        public async Task<CV> GetCVAsync(int id)
        {
            return await _context.CVs.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<CV>> GetAllCVsAsync()
        {
            return await _context.CVs.ToListAsync();
        }

        public async Task DeleteCVAsync(int id)
        {
            var cv = await _context.CVs.FindAsync(id);
            if (cv is null)
            {
                return;
            }

            _context.CVs.Remove(cv);
            await _context.SaveChangesAsync();
        }
    }
}
