namespace Application.Features.Departments.Commands.CreateDepartment;

public sealed class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
{
    private readonly IDepartmentRepository _departmentRepository;

    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department { Name = request.Name };
        var created = await _departmentRepository.AddAsync(department, cancellationToken);

        return new DepartmentDto
        {
            Id = created.Id,
            Name = created.Name,
            Employees = []
        };
    }
}
