using Data;
using Data.Entitys;
using Services.Interface;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    { 
        private readonly VpsDbContext dbcontext;
        public RefreshTokenRepository(VpsDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public async Task<RefreshTokenViewModel> Add(RefreshTokenViewModel model)
        {
            var entity = await dbcontext.RefreshTokens.AddAsync(new RefreshToken { 
                Id = model.Id,
                ExpiredAt = model.ExpiredAt,
                IsRevoked = model.IsRevoked,
                IssuedAt = model.IssuedAt,
                IsUsed = model.IsUsed,
                JwtId = model.JwtId,
                Token = model.Token,
                UserId= model.UserId
            });
            await dbcontext.SaveChangesAsync();

            return model;
        }

        public RefreshTokenViewModel GetRefreshToken(string refreshToken)
        {
            var entity = dbcontext.RefreshTokens.FirstOrDefault(a => a.Token.Contains(refreshToken));
            if (entity != null)
            {
                return new RefreshTokenViewModel
                {
                    Id = entity.Id,
                    ExpiredAt = entity.ExpiredAt,
                    IsRevoked = entity.IsRevoked,
                    IssuedAt = entity.IssuedAt,
                    IsUsed = entity.IsUsed,
                    JwtId = entity.JwtId,
                    Token = entity.Token,
                    UserId = entity.UserId
                };
            }
            return null;
        }
    }
}
