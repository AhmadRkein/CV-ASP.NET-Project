using I3332Proj.Models;
using I3332Proj.Models.DataModels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace I3332Proj.Services
{
    public class DatabaseServices
    {
        private readonly DatabaseContext _context;
        private readonly GradingService _grading;
        public DatabaseServices(DatabaseContext context, GradingService grading)
        {
            _context = context;
            _grading = grading;
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
            var ImagesFolder = "/Images";
            Directory.CreateDirectory(ImagesFolder);

            var fileExt = Path.GetExtension(cvBinding.Photo.FileName);
            var imagePath = Path.Combine(ImagesFolder, Guid.NewGuid().ToString() + fileExt);
            using (var filestream = new FileStream(imagePath, FileMode.Create)) 
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
                PhotoPath= imagePath,
            };

            _context.Add(dbCV);
            _context.SaveChanges();

            return dbCV;
        }
    }
}
