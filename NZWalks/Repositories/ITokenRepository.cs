using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.Repositories
{
    public interface ITokenRepository
    {

        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
