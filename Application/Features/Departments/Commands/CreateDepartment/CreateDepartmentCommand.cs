namespace Application.Features.Departments.Commands.CreateDepartment;

public record CreateDepartmentCommand(string Name) : IRequest<DepartmentDto>;
