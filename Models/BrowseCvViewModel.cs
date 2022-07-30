using System.ComponentModel;

namespace I3332Proj.Models
{
    public class BrowseCvViewModel
    {
        public int Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }
        
        [DisplayName("Gender")]
        public string Gender { get; set; }
        
        [DisplayName("Grade")]
        public int Grade { get; set; }


    }
}
