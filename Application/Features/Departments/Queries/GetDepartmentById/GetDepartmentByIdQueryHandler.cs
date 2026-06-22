namespace Application.Features.Departments.Queries.GetDepartmentById;

public sealed class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>
{
    private readonly IDepartmentRepository _departmentRepository;

    public GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<DepartmentDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Department with ID {request.Id} was not found.");

        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Employees = department.Employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                DepartmentId = e.DepartmentId,
                DepartmentName = department.Name
            }).ToList()
        };
    }
}
