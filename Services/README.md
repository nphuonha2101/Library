## Chú thích cho Service

- Mỗi khi tạo 1 Service, bạn cần tạo 1 interface cho nó extends interface <code>IService</code>.
- Nếu Service của bạn có các phương thức đặc biệt ngoài CRUD thì bạn cần khai báo phương thức đó trong Interface của
  Service đó:

```csharp
// In interface IBookService
using Library.Entities.Implements;

namespace Library.Services.Interfaces;

/**
 * Interface for Book service
 * If this service needs to be extended, add methods here
 */
public interface IBookService : IService<Book>
{
 List<Book> GetAllByAuthor(int authorId);

}
```

- Nếu Service của bạn không có phương thức đặc biệt nào ngoài CRUD bạn cũng phải tạo 1 interface rỗng vào trong thư
  mục <code>Interfaces</code> (mục đích là để Dependency Injection)
- Trong thư mục <code>Implements</code> bạn cần phải implements interface của Service bạn vừa khai báo và hiện thực các
  phương thức trong interface đó. Xem ví dụ về <code>BookService.cs</code>

```csharp
using Library.Data.Repositories.Interfaces;
using Library.Entities.Implements;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

/**
 * Book service
 * Implement IBookRepository
 */
public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public List<Book> GetAllByAuthor(int authorId)
    {
        return _bookRepository.GetBooksByAuthorAsync(authorId).Result;
    }
    
    public List<Book> GetAll()
    {
        return  _bookRepository.GetAllAsync().Result;
    }

    public Book GetById(int id)
    {
        return _bookRepository.GetByIdAsync(id).Result;
    }

    // ...
}
```

- Sau khi đã tạo interface và implement của service thì bạn cần vào Program.cs để khai báo cho service đó để .NET thực
  hiện Dependency Injection

```csharp
  // In Program.cs

/*
* This following code snippet configures the application to use all services.
* These services will be injected into the API endpoints automatically.
* You can add more services here.
* Scoped services are created once per request, it is suitable for services that work with the database.
  */
  builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

  builder.Services.AddScoped<IBookService, BookService>();
  builder.Services.AddScoped<IBookRepository, BookRepository>();
  // builder.Services.AddScoped<IAuthorService, AuthorService>();
  // ...

```