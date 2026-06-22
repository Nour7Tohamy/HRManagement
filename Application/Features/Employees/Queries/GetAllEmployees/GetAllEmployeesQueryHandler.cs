using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Employees.Queries.GetAllEmployees;

public sealed class GetAllEmployeesQueryHandler
    : IRequestHandler<GetAllEmployeesQuery, PagedResult<EmployeeDto>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<PagedResult<EmployeeDto>> Handle(
        GetAllEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _employeeRepository
            .Query()
            .AsNoTracking();

        // 🔎 Search by name
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(e => e.Name.Contains(request.Search));
        }

        // 🎯 Filter by Department
        if (request.DepartmentId.HasValue)
        {
            query = query.Where(e => e.DepartmentId == request.DepartmentId.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(e => e.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department.Name
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<EmployeeDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}