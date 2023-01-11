using AspWebApi.DataContext;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspWebApi.Repositories
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var Result = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync(); 
            return Result.Entity;
        }

        public async Task<Employee> DeleteEmployee(int Id)
        {
            var Result = await _context.Employees.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if(Result != null)
            {
                _context.Employees.Remove(Result);
                await _context.SaveChangesAsync();
                return Result;
            }
            return null;
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var Result = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
            if(Result != null)
            {
                Result.Name = employee.Name;
                Result.City = employee.City;
                await _context.SaveChangesAsync();
                return Result;
            }
            return null;
            
        }
    }
}
