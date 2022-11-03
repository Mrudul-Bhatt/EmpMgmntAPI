using DemoAPI.Data;
using DemoAPI.Models.DTO;
using DemoAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DemoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly DemoDbContext demoDbContext;

        public EmployeesController(DemoDbContext demoDbContext)
        {
            this.demoDbContext = demoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await demoDbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await demoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee != null)
            {
                return Ok(employee);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeRequest request)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                Salary = request.Salary,
                Department = request.Department,
            };

            await demoDbContext.Employees.AddAsync(employee);
            await demoDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, UpdateEmployeeRequest request)
        {
            var existingEmployee = await demoDbContext.Employees.FindAsync(id);

            if(existingEmployee!=null)
            {
                existingEmployee.Name = request.Name;
                existingEmployee.Email = request.Email;
                existingEmployee.DateOfBirth = request.DateOfBirth;
                existingEmployee.Salary = request.Salary;
                existingEmployee.Department = request.Department;
                await demoDbContext.SaveChangesAsync();
                return Ok(existingEmployee);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var existingEmployee = await demoDbContext.Employees.FindAsync(id);

            if (existingEmployee != null)
            {
                demoDbContext.Employees.Remove(existingEmployee);
                await demoDbContext.SaveChangesAsync();
                return Ok(existingEmployee);
            }

            return NotFound();
        }
    }

}
