namespace Application.Features.Departments.Queries.GetDepartmentById;

public record GetDepartmentByIdQuery(int Id) : IRequest<DepartmentDto>;
