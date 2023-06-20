using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Data;

namespace PRN231_PE_Trial_API.Controllers
{
    [Authorize(Roles = "ProjectManager")]
    public class EmployeeController : ODataController
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeController(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [EnableQuery]
        public ActionResult<IQueryable<Employee>> Get()
        {
            return Ok(_employeeRepository.GetEmployees());
        }
    }
}
