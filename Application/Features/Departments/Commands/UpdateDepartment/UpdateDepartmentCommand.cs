namespace Application.Features.Departments.Commands.UpdateDepartment;

public record UpdateDepartmentCommand(int Id, string Name) : IRequest<Unit>;
