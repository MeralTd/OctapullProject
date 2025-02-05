using Domain.Entities;

namespace Application.Interfaces.Repository;

public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken>, IRepository<RefreshToken>
{
}