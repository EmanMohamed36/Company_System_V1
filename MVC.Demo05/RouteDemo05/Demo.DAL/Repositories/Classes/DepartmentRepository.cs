using Demo.DAL.Data;
using Demo.DAL.Models.DepartmentModel;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Classes
{
    public class DepartmentRepository(ApplicationDbContext _context)
        : GenericRepository<Department>(_context), IDepartmentRepository
    {

    }
}
