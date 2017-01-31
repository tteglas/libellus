using System.Security.Claims;
using System.Threading.Tasks;
using Libellus.DataAccess.Domain;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Libellus.Managers
{
    public class SignInCustomManager : SignInManager<User, string>
    {
        public SignInCustomManager(UserCustomManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UserCustomManager)UserManager);
        }

        public static SignInCustomManager Create(IdentityFactoryOptions<SignInCustomManager> options, IOwinContext context)
        {
            return new SignInCustomManager(context.GetUserManager<UserCustomManager>(), context.Authentication);
        }
    }
}