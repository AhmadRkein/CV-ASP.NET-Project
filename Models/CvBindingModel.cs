using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace I3332Proj.Models
{
    public class CvBindingModel
    {

        public int Id { get; set; }
        
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        
        [Required]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        
        [Required]
        [DisplayName("Nationality")]
        public string Nationality { get; set; }
        
        [Required]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        
        [DisplayName("Programming Skills")]
        public List<string> ProgSkills { get; set; } = new List<string>();
        
        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }
        
        [Required]
        [EmailAddress]
        [Compare(nameof(Email))]
        [DisplayName("Confirm Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [Range(1,20)]
        public int X { get; set; }
        
        [Required]
        [Range(20, 50)]
        public int Y { get; set; }
        
        [Required]
        public int Sum { get; set; }

        [Required]
        [DisplayName("Photo")]
        public IFormFile Photo { get; set; }
    }
}
