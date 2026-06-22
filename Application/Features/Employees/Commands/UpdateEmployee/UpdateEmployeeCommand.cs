using MediatR;

namespace Application.Features.Employees.Commands.UpdateEmployee;

public record UpdateEmployeeCommand(int Id, string Name, int DepartmentId) : IRequest<Unit>;
