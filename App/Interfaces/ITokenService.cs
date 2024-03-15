using App.Entities;

namespace App.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}
