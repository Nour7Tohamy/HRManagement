using Application.Interfaces;

namespace Application.Features.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public CreateEmployeeCommandHandler(
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(request.DepartmentId, cancellationToken)
            ?? throw new KeyNotFoundException($"Department with ID {request.DepartmentId} was not found.");

        var employee = new Employee
        {
            Name = request.Name,
            DepartmentId = request.DepartmentId
        };

        var created = await _employeeRepository.AddAsync(employee, cancellationToken);

        return new EmployeeDto
        {
            Id = created.Id,
            Name = created.Name,
            DepartmentId = created.DepartmentId,
            DepartmentName = department.Name
        };
    }
}
