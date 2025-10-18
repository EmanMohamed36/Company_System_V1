using Demo.DAL.Data;
using Demo.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Classes
{
    public class UnitOfWork:IUnitOfWork/*,IDisposable*/
    {
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext) 
        {
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dbContext));
            _dbContext = dbContext;
        }

        public IEmployeeRepository  EmployeeRepository { get => _employeeRepository.Value;  }
        public IDepartmentRepository DepartmentRepository { get => _departmentRepository.Value;}

        //public void Dispose()
        //{
        //    //Action before close 
        //    _dbContext.Dispose();
        //}

        public int SaveChanges()
        {
           return _dbContext.SaveChanges();
        }
    }
}
