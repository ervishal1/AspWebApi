using AspWebApi.Repositories;
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

        [HttpGet]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try {
                return Ok(await _employeeRepo.GetEmployee(id));
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data From DataBase");
            }
        }
    }
}
