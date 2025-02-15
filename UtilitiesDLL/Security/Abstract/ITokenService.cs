using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesDLL.Entities.Concrete;

namespace UtilitiesDLL.Security.Abstract
{
    public interface ITokenService
    {
        Task<Token> CreateAccessToken(AppUser appUser, List<string> roles);
        string CreateRefreshToken();
    }
}
