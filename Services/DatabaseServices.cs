using I3332Proj.Models;
using I3332Proj.Models.DataModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

        public async Task<CV> AddCVAsync(CvBindingModel cvBinding)
        {
            string imageRelPath = CreateImage(cvBinding.Photo);

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
                PhotoPath = imageRelPath,
            };

            await _context.AddAsync(dbCV);
            await _context.SaveChangesAsync();

            return dbCV;
        }

        public async Task EditCVAsync(CvEditBindingModel cv)
        {
            if (cv is null)
                return;

            var dbCV = await GetCVAsync(cv.Id);

            if (dbCV is null)
                return;

            string imageRelPath = CreateImage(cv.Photo);

            dbCV.BirthDate = cv.BirthDate;
            dbCV.Email = cv.Email;
            dbCV.FirstName = cv.FirstName;
            dbCV.LastName = cv.LastName;
            dbCV.Gender = cv.Gender;
            dbCV.Nationality = cv.Nationality;
            dbCV.ProgSkills = string.Join(";", cv.ProgSkills);
            dbCV.Grade = _grading.CalculateGrade(cv);

            if (!string.IsNullOrWhiteSpace(imageRelPath))
                dbCV.PhotoPath = imageRelPath;

            await _context.SaveChangesAsync();
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

            var photoFullPath = Path.Combine(webHostEnvironment.WebRootPath, cv.PhotoPath);
            if (File.Exists(photoFullPath))
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                File.Delete(photoFullPath);
            }

            _context.CVs.Remove(cv);
            await _context.SaveChangesAsync();
        }

        private string CreateImage(IFormFile photo)
        {
            if (photo is null)
                return "";

            var ImagesFolder = "Images";
            var ImagesFullPath = Path.Combine(webHostEnvironment.WebRootPath, ImagesFolder);
            Directory.CreateDirectory(ImagesFullPath);

            var fileExt = Path.GetExtension(photo.FileName);
            var fileName = Guid.NewGuid().ToString() + fileExt;
            var imageFullPath = Path.Combine(ImagesFullPath, fileName);
            var imageRelPath = Path.Combine(ImagesFolder, fileName);
            using (var filestream = new FileStream(imageFullPath, FileMode.Create))
            {
                photo.CopyTo(filestream);
            }

            return imageRelPath;
        }

    }
}
