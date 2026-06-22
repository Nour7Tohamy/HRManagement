using Application.DTOs;
using MediatR;

namespace Application.Features.Departments.Queries.GetAllDepartments;

public sealed record GetAllDepartmentsQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<PagedResult<DepartmentDto>>;