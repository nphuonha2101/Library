using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;

namespace Library.Data.Repositories.Implements;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
        
    }
}