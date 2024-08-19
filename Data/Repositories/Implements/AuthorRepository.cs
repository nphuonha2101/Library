using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Entities.Implements;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements;

public class AuthorRepository(ApplicationDbContext appDbContext) : Repository<Author>(appDbContext), IAuthorRepository;