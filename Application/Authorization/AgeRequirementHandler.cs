using Microsoft.AspNetCore.Authorization;

namespace Application.Authorization
{
    public class AgeRequirementHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
        {
            var isAuth = context.User.Identity.IsAuthenticated;
            if (!isAuth)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            var dateOfBirth = DateTime.Parse(context.User.FindFirst(x => x.Type == "DateOfBirth").Value);

            if (dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
