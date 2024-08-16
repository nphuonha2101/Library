using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Utils.Validations;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<User> LoginAsync(string usernameOrEmail, string password)
    {
        var isEmail = new EmailValidation().IsValid(usernameOrEmail);

        if (isEmail)
            return await AppDbContext.Users.FirstAsync(u => u.Email == usernameOrEmail && u.Password == password);

        return await AppDbContext.Users.FirstAsync(u => u.Username == usernameOrEmail && u.Password == password);
    }
}