using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TCGCF.API.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string LastName { get; set; }
    }
}
