using BlogApiDemo.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult EmployeeList()
        {
            using var c = new Context();
            var values = c.Employees.ToList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult EmployeeAdd(Employee employee)
        {
            using var c = new Context();
            c.Add(employee);
            c.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            using var c = new Context();
            var employee = c.Employees.Find(id);
            if (employee==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(employee);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            using var c = new Context();
            var employee = c.Employees.Find(id);
            if (employee==null)
            {
                return NotFound($"{id}'li çalışan bulunamadı!");
            }
            else
            {
                c.Remove(employee);
                c.SaveChanges();
                return Ok($"{employee.Name} başarıyla silindi.");
            }
        }
        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            using var c = new Context();
            var emp = c.Find<Employee>(employee.Id);
            if (emp is null)
            {
                return NotFound();
            }
            else
            {
                emp.Name = employee.Name;
                c.Update(emp);
                c.SaveChanges();
                return Ok();
            }
        }
    }
}
