using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace I3332Proj.Models
{
    public class SummaryCvViewModel
    {
        public int Id { get; set; }
        
        [DisplayName("First Name")] 
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DisplayName("Nationality")]
        public string Nationality { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayName("Programming Skills")]
        public string ProgSkills { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }
        
        [DisplayName("Photo")]
        public string PhotoPath { get; set; }

        [DisplayName("Grade")]
        public int Grade { get; set; }
    }
}
