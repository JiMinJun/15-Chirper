using System;
using Chirper.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace Chirper.API.Infrastructure
{
    public class AuthorizationRepository : IDisposable
    {
        private readonly UserManager<ChirperUser> _userManager;
        private readonly DataContext _dataContext;

        public AuthorizationRepository()
        {
            _dataContext = new DataContext();
            _userManager = new UserManager<ChirperUser>(new UserStore<ChirperUser>(_dataContext));
        }

        public async Task<IdentityResult> RegisterUser(UserRegistrationModel userModel)
        {
            ChirperUser user = new ChirperUser
            {
                UserName = userModel.UserName,
                Email = userModel.EmailAddress
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<ChirperUser> FindUser(string userName, string password)
        {
            ChirperUser user = await _userManager.FindAsync(userName, password);
            return user;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
            _userManager.Dispose();
        }
    }
}