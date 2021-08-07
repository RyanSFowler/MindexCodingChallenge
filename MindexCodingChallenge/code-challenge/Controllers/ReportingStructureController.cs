using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;

namespace challenge.Controllers
{
    [Route("api/reportingStructure")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }


        [HttpGet("{id}", Name = "GetReportingStructureById")]
        public IActionResult GetReportingStructureById(String id)
        {
            var reportingStructure = _employeeService.GetReportingStructureById(id);
            if (reportingStructure == null)
                return NotFound();

            return Ok(reportingStructure);
        }
    }
}
