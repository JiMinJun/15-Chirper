using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace Chirper.API.Infrastructure
{
    public class ChirperAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (var authorizationRepository = new AuthorizationRepository())
            {
                var user = await authorizationRepository.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "username or password is incorrect");
                }
                else
                {
                    var token = new ClaimsIdentity(context.Options.AuthenticationType);
                    token.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    token.AddClaim(new Claim(ClaimTypes.Role, "user"));

                    context.Validated(token);
                }
            }
        }
    }
 }