## Đây là chú thích cho Entities
- Các Entity sẽ sử dụng Entity Framework Core để mapping với Database.
- Các Entity sẽ được tạo trong thư mục <b>Implements</b> và implement interface <code>IEntity</code>.
- Sau đây là 1 ví dụ mẫu về cách tạo 1 Entity:
```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities.Implements;

[Table("books")]
public class Book : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    public long AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public Author author { get; set; }
    [Required]
    [StringLength(50)]
    public string isbn { get; set; }
}
```
- Trong ví dụ trên, Entity <code>Book</code> sẽ mapping với bảng <code>books</code> trong Database.
- Annotation <code>[Table("books")]</code> sẽ định nghĩa tên bảng trong Database.
- Annotation <code>[Key]</code> sẽ định nghĩa trường <code>Id</code> là trường <code>Primary Key</code> của bảng.
- Annotation <code>[DatabaseGenerated(DatabaseGeneratedOption.Identity)]</code> sẽ định nghĩa trường <code>Id</code> sẽ tự động tăng.
- Annotation <code>[Required]</code> sẽ định nghĩa trường <code>Name</code> và <code>isbn</code> không được để trống.
- Annotation <code>[StringLength(255)]</code> sẽ định nghĩa trường <code>Name</code> chỉ chứa tối đa 255 ký tự.
- Annotation <code>[ForeignKey("AuthorId")]</code> sẽ định nghĩa trường <code>AuthorId</code> sẽ là trường <code>Foreign Key</code> liên kết với trường <code>Id</code> của Entity <code>Author</code>.
Với AuthorId là tên trường 
```csharp  
public long AuthorId { get; set; }
``` 
chứa Id của Author.
- Riêng với Foreign Key, bạn cần thêm 1 trường chứa id cuả Entity mà bạn muốn liên kết và thêm Annotation <code>[ForeignKey("Tên trường Id")]</code> để định nghĩa Foreign Key.
