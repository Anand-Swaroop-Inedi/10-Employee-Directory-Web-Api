using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRoleDepLocLinkRepository _roleDepLocLinkRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly EmployeeDirectoryContext _employeeDirectoryContext;
        public UnitOfWork(IStatusRepository statusRepository,IEmployeeRepository employeeRepository,ILocationRepository locationRepository,IRoleRepository roleRepository,IRoleDepLocLinkRepository roleDepLocLinkRepository,IDepartmentRepository departmentRepository,EmployeeDirectoryContext employeeDirectoryContext) {
            _departmentRepository = departmentRepository;
            _statusRepository = statusRepository;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _statusRepository = statusRepository;
            _employeeDirectoryContext = employeeDirectoryContext;
            _roleDepLocLinkRepository = roleDepLocLinkRepository;
            _locationRepository = locationRepository;
        }

        public void BeginTransaction()
        {
            _employeeDirectoryContext.Database.BeginTransaction();
        }

       public void CommitTransaction()
        {
            _employeeDirectoryContext.Database.CommitTransaction();
        }
        public void RollBack()
        {
            _employeeDirectoryContext.Database.RollbackTransaction();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _employeeDirectoryContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void SaveChanges()
        {
            _employeeDirectoryContext.SaveChanges();
        }
       
    }
}
