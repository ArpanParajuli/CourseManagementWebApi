
namespace CourseManagement.Repositories;


public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
