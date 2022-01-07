using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenViewModel> Add(RefreshTokenViewModel model);
        RefreshTokenViewModel GetRefreshToken(string refreshToken);
    }
}
