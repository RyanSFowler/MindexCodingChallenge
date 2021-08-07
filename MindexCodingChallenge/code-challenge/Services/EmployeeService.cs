using System;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee GetByIdWithDirectReports(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetByIdWithDirectReports(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }

         public ReportingStructure GetReportingStructureById(string id)
         {
            _logger.LogDebug($"Geting Reporting Structure for '{id}'");

            // added endpoint to return employee with the direct report employees filled out
            var employee = GetByIdWithDirectReports(id);
            if (employee != null)
            {
                return new ReportingStructure
                {
                    Employee = GetById(id), // pushing original employee dataset into ReportingStructure
                    NumberOfReports = getReportingCount(employee)
                };
            }
            return null;
        }

        //a recursive function to loop through all direct reports and get a count
        int getReportingCount(Employee employee)
        {
            _logger.LogDebug($"Geting Direct Report Count for '{employee.FirstName} {employee.LastName}'");
            var count = 0;
            //some employees have no direct reports
            if (employee.DirectReports != null)
            {
                foreach (Employee directReport in employee.DirectReports)
                {
                    count++;
                    count += getReportingCount(directReport);

                }
            }
            return count;
        }
    }
}
