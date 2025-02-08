using Microsoft.EntityFrameworkCore;
using SizeFintech.Domain.Entities;
using SizeFintech.Domain.Security.Tokens;
using SizeFintech.Domain.Services.LoggedUser;
using SizeFintech.Infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SizeFintech.Infrastructure.Services.LoggedUser;
internal class LoggedUser : ILoggedUser
{
    private readonly SizeFintechDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(SizeFintechDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
    }
}
