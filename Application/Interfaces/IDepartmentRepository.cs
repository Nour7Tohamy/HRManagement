

namespace Application.Interfaces;

public interface IDepartmentRepository
{
    IQueryable<Department> Query();

    Task<Department?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Department> AddAsync(Department department, CancellationToken cancellationToken = default);
    Task UpdateAsync(Department department, CancellationToken cancellationToken = default);
    Task DeleteAsync(Department department, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}