using challenge.Models;
using System;

namespace challenge.Services
{
    public interface IEmployeeService
    {
        Employee GetById(String id);
        Employee GetByIdWithDirectReports(String id);
        Employee Create(Employee employee);
        Employee Replace(Employee originalEmployee, Employee newEmployee);

        ReportingStructure GetReportingStructureById(String id);
    }
}
