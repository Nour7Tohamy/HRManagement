namespace Application.Features.Employees.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDto>;
