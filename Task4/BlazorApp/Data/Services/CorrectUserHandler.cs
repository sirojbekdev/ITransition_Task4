using BlazorApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlazorApp.Data.Services
{
    public class CorrectUserHandler : AuthorizationHandler<CorrectUserRequirement>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public CorrectUserHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CorrectUserRequirement requirement)
        {
            var userName = context.User;
            var user = await _userManager.GetUserAsync(userName);
            if (user is null)
            {
                return;
            }
            if ( user.Status == Enums.Status.Blocked)
            {
                context.Fail();
                await _signInManager.SignOutAsync();
            }
            if (user.Status == Enums.Status.Active)
            {
                context.Succeed(requirement);
            }
        }
    }
}
