using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
/*        public IEmployeeRepository EmployeeRepository { get; }
        public ILocationRepository LocationRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
        public IStatusRepository StatusRepository { get; }
        public IRoleDepLocLinkRepository RoleDepLocLinkRepository { get; }
        public ILocationRepository locationRepository { get; }*/
        public void BeginTransaction();
        public void CommitTransaction();
        public void  RollBack();
        public void SaveChanges();


    }
}
