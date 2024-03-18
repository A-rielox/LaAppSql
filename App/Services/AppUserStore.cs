using App.Data;
using App.Entities;
using App.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Services.Services;

public class AppUserStore : IUserStore<AppUser>,
                            IUserEmailStore<AppUser>,
                            IUserPasswordStore<AppUser>
{
    private readonly IUserRepository _userRepository;

    public AppUserStore(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
    {
        user.Id = await _userRepository.CreateUser(user);

        return IdentityResult.Success;
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public void Dispose()
    {
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public async Task<AppUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserByUserNameStoreAsync(normalizedEmail); // es el mismo UserName
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public async Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {// lo va a encontrar = a pesar de no estar normalizado en la DB
        return await _userRepository.GetUserByUserNameAsync(normalizedUserName);
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task<string> GetEmailAsync(AppUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
        //throw new NotImplementedException();
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task<bool> GetEmailConfirmedAsync(AppUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task<string> GetNormalizedEmailAsync(AppUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash);
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id.ToString());
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
    {
        //return Task.FromResult(user.Email);
        return Task.FromResult(user.UserName);
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task SetEmailAsync(AppUser user, string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task SetEmailConfirmedAsync(AppUser user, bool confirmed, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task SetNormalizedEmailAsync(AppUser user, string normalizedEmail, CancellationToken cancellationToken)
    {
        user.NormalizedEmail = normalizedEmail;

        return Task.CompletedTask;
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;

        return Task.CompletedTask;
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
