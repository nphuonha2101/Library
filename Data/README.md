## Đây là chú thích cho Repository

- Trong thư mục <b>Interfaces</b> đã có 1 interface <code>IRepository</code> được định nghĩa sẵn các phương thức CRUD cơ
  bản như:
    - <code>GetAll()</code>
    - <code>GetById()</code>
    - <code>Add()</code>
    - <code>Update()</code>
    - <code>Delete()</code>
- Trong thư mục <b>Implements<b> đã có 1 class <code>Repository</code> implement interface <code>IRepository</code> và
  thực hiện các phương thức CRUD cơ bản.

## Quan trọng:

- Nếu repository của bạn cần thêm các phương thức khác, bạn cần tạo 1 interface mới kế thừa từ <code>IRepository</code>
  và thêm các phương thức đó vào interface mới đó.
- Khi tạo mới 1 repository, bạn cần tạo 1 interface mới kế thừa từ <code>IRepository</code> và 1 class implement
  interface đó.
- Sau đó ở class implement interface mới, bạn cần thực hiện các phương thức đã định nghĩa trong interface mới đó và quan
  trọng là
  class đó phải implement interface mới vừa tạo và extend từ <code>Repository</code> để sử dụng các phương thức CRUD cơ
  bản đã được định nghĩa sẵn.
- Chi tiết xem IBookRepository (<b>Interfaces</b>) và BookRepository (<b>Implements</b>) để hiểu rõ hơn.
- Nếu tạo thêm 1 repository, bạn cần phải thêm nó vào <code>Program.cs</code> để .NET tự động Dependence Injection

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