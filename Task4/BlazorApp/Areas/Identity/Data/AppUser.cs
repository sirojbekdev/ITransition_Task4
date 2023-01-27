using System.ComponentModel.DataAnnotations;
using BlazorApp.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace BlazorApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
    [Required]
    [PersonalData]
    public string FullName { get; set; }
    public DateTime RegisterationDate { get; set; }
    public DateTime LastLogin { get; set; }
    public Status Status { get; set; }
}
