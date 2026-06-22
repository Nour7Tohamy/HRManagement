using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Departments.Queries.GetAllDepartments;

public sealed class GetAllDepartmentsQueryHandler
    : IRequestHandler<GetAllDepartmentsQuery, PagedResult<DepartmentDto>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<PagedResult<DepartmentDto>> Handle(
        GetAllDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _departmentRepository
            .Query()
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(d => d.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                Employees = d.Employees.Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = d.Name
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<DepartmentDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}