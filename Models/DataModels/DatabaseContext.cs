using Microsoft.EntityFrameworkCore;
using I3332Proj.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace I3332Proj.Models.DataModels
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CV> CVs { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<ProgrammingSkill> ProgrammingSkills { get; set; }

        public void InitializeDatabase()
        {
            this.Database.EnsureCreated();

            if (!Nationalities.Any())
            {
                var list = Properties.Resource.Nationalities.Split('\n').Select(x => new Nationality() { Name = x.Trim() });

                Nationalities.AddRange(list);
            }

            if (!ProgrammingSkills.Any())
            {
                var list = new[] { "Java", "C#", "Python", "C++", "R", "SQL", "Go" };

                ProgrammingSkills.AddRange(list.Select(x => new ProgrammingSkill() { Name = x }));
            }
            this.SaveChanges();
        }
    }
}
