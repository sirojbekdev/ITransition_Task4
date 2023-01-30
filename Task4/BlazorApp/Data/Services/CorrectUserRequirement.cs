using BlazorApp.Data.Enums;
using Microsoft.AspNetCore.Authorization;

namespace BlazorApp.Data.Services
{
    public class CorrectUserRequirement : IAuthorizationRequirement
    {
    }
}
