using Application.Interfaces;

namespace Application.Features.Departments.Commands.DeleteDepartment;

public sealed class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Unit>
{
    private readonly IDepartmentRepository _departmentRepository;

    public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Unit> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Department with ID {request.Id} was not found.");

        await _departmentRepository.DeleteAsync(department, cancellationToken);

        return Unit.Value;
    }
}
