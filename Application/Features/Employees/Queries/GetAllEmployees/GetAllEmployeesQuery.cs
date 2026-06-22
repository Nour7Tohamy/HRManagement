using Application.DTOs;
using MediatR;

namespace Application.Features.Employees.Queries.GetAllEmployees;

public sealed record GetAllEmployeesQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? Search = null,
    int? DepartmentId = null
) : IRequest<PagedResult<EmployeeDto>>;