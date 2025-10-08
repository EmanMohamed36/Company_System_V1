using Demo.BLL.DTOs.EmployeeDTOs;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.PL.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, IWebHostEnvironment _enviroment) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployee();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedEmployeeDTO employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int res = _employeeService.AddEmployee(employee);
                    if (res > 0)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        return View(nameof(Index));
                    }

                }
                catch (Exception ex)
                {
                    //Log Exception 
                    //1)Development => Log Error in console and Ant Return The Same Value With The Error Message
                    if (_enviroment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(employee);


                    }
                    //2)Deployment =>  Log Error in File and Ant Return The Same Value With The Error Message
                    else
                    {
                        // _logger.LogError(ex.Message);
                        return View(employee);

                    }


                }
            }
            else
                return View(employee);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest();
            if (id < 0) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();

            return View(employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();
            if (id < 0) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            var dto = new UpdatedEmployeeDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                HiringDate = employee.HiringDate,
                IsActive = employee.IsActive,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)
            };
          
            return View(dto);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id,UpdatedEmployeeDTO dto)
        {
            if(id != dto.Id) return BadRequest();

            if (ModelState.IsValid)
            {

               
                try
                {
                    int res = _employeeService.UpdateEmployee(dto);
                    if (res > 0)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        return View(dto);
                    }

                }
                catch (Exception ex)
                {
                    //Log Exception 
                    //1)Development => Log Error in console and Ant Return The Same Value With The Error Message
                    if (_enviroment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(dto);


                    }
                    //2)Deployment =>  Log Error in File and Ant Return The Same Value With The Error Message
                    else
                    {
                      //  _logger.LogError(ex.Message);
                        return View(dto);

                    }


                }


            }
            else
            {

                return View(dto);
            }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();
            try
            {
                bool isDeleted = _employeeService.DeleteEmployee(id);
                if (isDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Can't be Deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }

            }
            catch (Exception ex)
            {
                //Log Exception 
                //1)Development => Log Error in console and Ant Return The Same Value With The Error Message
                if (_enviroment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));

                }
                //2)Deployment =>  Log Error in File and Ant Return The Same Value With The Error Message
                else
                {
                   // _logger.LogError(ex.Message);
                    return View("ErrorView", ex);

                }


            }
        }

    }
}
