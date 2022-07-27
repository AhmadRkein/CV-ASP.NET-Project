using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace I3332Proj.Models.DataModels
{
    public class CV
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string ProgSkills { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public int Grade { get; set; }
    }
}
