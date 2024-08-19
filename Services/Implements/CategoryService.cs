using Library.Data.Repositories.Interfaces;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public List<Category>? GetAll()
    {
        return _categoryRepository.GetAllAsync().Result;
    }

    public Category? GetById(long id)
    {
        return _categoryRepository.GetByIdAsync(id).Result;
    }

    public Category? Add(Category entity)
    {
        return _categoryRepository.AddAsync(entity).Result;
    }

    public Category? Update(long id, Category entity)
    {
        return _categoryRepository.UpdateAsync(id, entity).Result;
    }

    public bool Delete(long id)
    {
        return _categoryRepository.DeleteAsync(id).Result;
    }
}