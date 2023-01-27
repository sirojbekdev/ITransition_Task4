using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Task4.Models.Enums;

namespace Task4.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
    [Required]
    public string Name { get; set; }
    public DateTime RegisterationDate { get; set; }
    public DateTime LastLogin { get; set; }
    public Status Status { get; set; }
}