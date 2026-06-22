using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public sealed class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Employee> Query()
        {
            return _context.Employees
                .Include(e => e.Department)
                .AsNoTracking();
        }

        public async Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        public async Task<Employee> AddAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return employee;
        }

        public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
            => await _context.Employees.AnyAsync(e => e.Id == id, cancellationToken);
    }
}