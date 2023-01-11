using AspWebApi.Repositories;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspWebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase 
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeesController(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try {
                return Ok(await _employeeRepo.GetEmployees());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error in Retriving Data From DataBase");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var res = await _employeeRepo.GetEmployee(id);
                if(res == null)
                {
                    return NotFound();
                }
                return res;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data From DataBase");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee emp)
        {
            try
            {
                if(emp == null)
                {
                    return BadRequest();
                }

                var CreatedEmp = await _employeeRepo.AddEmployee(emp);
                return CreatedAtAction(nameof(GetEmployees), new { id = CreatedEmp.Id }, CreatedEmp);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data From DataBase");
            }
        }
    }
}
