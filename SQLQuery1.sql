SELECT e.Id , e.Name AS EmployeeName , d.Name AS DepartmentName
FROM Employees e INNER JOIN Departments d
ON e.DepartmentId = d.Id
ORDER BY d.Id, e.Name;