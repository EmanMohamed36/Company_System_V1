using Demo.BLL.DTOs.DepartmentDTOs;
using Demo.BLL.Services.Interfaces;
using Demo.PL.ViewModels.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentsController(IDepartmentServices _departmentService, ILogger<HomeController> _logger, IWebHostEnvironment _enviroment) : Controller
    {
        //Get BaseUrl /Departments/Index
        //check flag is deleted

        [HttpGet]
        public IActionResult Index()
        {
            //ViewData["Message"] = "Hello from View Data";
            //ViewBag.Msg = "Hello from View Bag"; // Dynamic type
            ViewData["Message"] = new DepartmentDTO { Name = "Test View Data" };
            ViewBag.Msg = new DepartmentDTO { Name = "Test View Bag" };
            var departments = _departmentService.GetAllDepartment();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Filter Action 
        public IActionResult Create(DepartmentViewModel departmentView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var departmentDTO = departmentView.FromViewModelToCreatedDTO();
                    int res = _departmentService.AddDepartment(departmentDTO);
                    string msg;
                    if (res > 0)
                    {
                        msg = $"Department {departmentDTO.Name} is Created Successfully";
                    }
                    else
                    {
                        msg = $"Department{departmentDTO.Name} Creation Faild";
                    }
                    TempData["Message"] = msg;
                    return RedirectToAction(nameof(Index));

                    // return View(nameof(Index));
                }
                catch (Exception ex)
                {
                    //Log Exception 
                    //1)Development => Log Error in console and Ant Return The Same Value With The Error Message
                    if (_enviroment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);

                    }
                    //2)Deployment =>  Log Error in File and Ant Return The Same Value With The Error Message
                    else
                    {
                        _logger.LogError(ex.Message);

                    }
                    return View(departmentView);


                }

            }
            else
            {

                return View(departmentView);
            }

        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest();
            if (id < 0) return NotFound();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();

            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();
            if (id < 0) return NotFound();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var departmentView = department.FromDetailsDTOsTOEditViewModel();
            departmentView.Id = id.Value;
            return View(departmentView);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel viewDepartment)
        {


            if (ModelState.IsValid)
            {

                var updateDeptDTO = viewDepartment.FromEditViewModelToUpdateDTO();
                updateDeptDTO.Id = id;
                try
                {
                    int res = _departmentService.UpdateDepartment(updateDeptDTO);
                    if (res > 0)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        return View(viewDepartment);
                    }

                }
                catch (Exception ex)
                {
                    //Log Exception 
                    //1)Development => Log Error in console and Ant Return The Same Value With The Error Message
                    if (_enviroment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);

                    }
                    //2)Deployment =>  Log Error in File and Ant Return The Same Value With The Error Message
                    else
                    {
                        _logger.LogError(ex.Message);

                    }
                    return View(viewDepartment);


                }


            }
            else
            {

                return View(viewDepartment);
            }
        }
        //[HttpGet]
        //public IActionResult Delete(int ? id)
        //{
        //    if (id is null) return BadRequest();
        //    if (id < 0) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();
            try
            {
                bool isDeleted = _departmentService.DeleteDepartment(id);
                if (isDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Can't be Deleted");
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
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);

                }


            }
        }
        //Eny button without submit goes to Get 
        //Submit button -> Post Action
        //Another Buttons -> Get Action


    }
}
