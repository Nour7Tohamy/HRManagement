using Application.Interfaces;
using MediatR;

namespace Application.Features.Employees.Commands.UpdateEmployee;

public sealed class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public UpdateEmployeeCommandHandler(
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Employee with ID {request.Id} was not found.");

        _ = await _departmentRepository.GetByIdAsync(request.DepartmentId, cancellationToken)
            ?? throw new KeyNotFoundException($"Department with ID {request.DepartmentId} was not found.");

        employee.Name = request.Name;
        employee.DepartmentId = request.DepartmentId;

        await _employeeRepository.UpdateAsync(employee, cancellationToken);

        return Unit.Value;
    }
}
