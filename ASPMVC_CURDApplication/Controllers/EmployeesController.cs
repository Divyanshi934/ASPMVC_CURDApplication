using ASPMVC_CURDApplication.Data;
using ASPMVC_CURDApplication.Models;
using ASPMVC_CURDApplication.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.XPath;

namespace ASPMVC_CURDApplication.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDBClass mVCDemoDBClass;

        public EmployeesController(MVCDemoDBClass mVCDemoDBClass)
        {
            this.mVCDemoDBClass = mVCDemoDBClass;
        }

        //*** now we have single entry in DB, we can create an index method to show the list

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mVCDemoDBClass.Employees.ToListAsync();
            return View(employees);
        }

        //*** this will be the GET method, so writing HTTPGet
        [HttpGet]
        public IActionResult Add()
        {
            //*** added razor view by clicking on View method

            return View();
        }
        //*** first adding 1 employee in list to show. After creating Add Employee page, we'll create POST method.
        [HttpPost]
        //*** convert the method to asynchronous method by adding async
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            //*** doing convertion from AddEmployeeViewModel model to Employee model.
            //*** this is the Entity Domain model
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department
            };

            //*** using the above readonly field, we'll be talking to DB
            await mVCDemoDBClass.Employees.AddAsync(employee);
            await mVCDemoDBClass.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //*** add the view method
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            //*** retrive the single employee
            //*** FirstOrDefaultAsync will give null value too, so wrting if else condition.
            var employee = await mVCDemoDBClass.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth
                };

                //*** not confirmed
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mVCDemoDBClass.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mVCDemoDBClass.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
        //*** for deleting
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mVCDemoDBClass.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                mVCDemoDBClass.Employees.Remove(employee);
                await mVCDemoDBClass.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
