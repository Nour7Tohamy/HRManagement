namespace Application.Features.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand(string Name, int DepartmentId) : IRequest<EmployeeDto>;
