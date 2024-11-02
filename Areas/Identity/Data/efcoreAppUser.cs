using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using efcoreApp.Data;
using Microsoft.AspNetCore.Identity;

namespace efcoreApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the efcoreAppUser class
public class efcoreAppUser : IdentityUser
{
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    [NotMapped]
    public IFormFile? ImageURL { get; set; }
}

