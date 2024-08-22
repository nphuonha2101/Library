using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Utils.Validations;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements;

public class UserRepository(ApplicationDbContext appDbContext) : Repository<User>(appDbContext), IUserRepository
{
    public async Task<User?> LoginAsync(string usernameOrEmail, string password)
    {
        
        
        var user = await AppDbContext.Users
            .Where(u => (u.Username == usernameOrEmail || u.Email == usernameOrEmail) && u.Password == password)
            .SingleOrDefaultAsync();

        if (user == null)
        {
            throw new InvalidOperationException("Invalid username or password.");
        }

        return user;
    }
}