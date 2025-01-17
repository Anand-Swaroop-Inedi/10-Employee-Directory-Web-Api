﻿using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Models;


namespace BusinessLogicLayer.Managers
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleDepLocLinkRepository _roleDepLocLinkRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeManager(IRoleDepLocLinkRepository roleDepLocLinkRepository,IEmployeeRepository employeeRepository,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _roleDepLocLinkRepository = roleDepLocLinkRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Create(Employee employee)
        {
            try
            {
                _unitOfWork.BeginTransaction();
/*                int result=_unitOfWork.Role
*/            }
            catch (Exception ex) { }
            if (employee.JobTitleId != null && employee.DepartmentId != null && employee.LocationId != null)

            {
                int result = await _roleDepLocLinkRepository.GetRoleDepLocLinkId(employee.JobTitleId, employee.DepartmentId, employee.LocationId);
                if (result > 0)
                {
                    employee.RoleDeptLocId = result;
                    return await _employeeRepository.AddEmployeeToDb(_mapper.Map<EmployeeEntity>(employee));
                }
            }
            else
            {
                return await _employeeRepository.AddEmployeeToDb(_mapper.Map<EmployeeEntity>(employee));
            }
            return false;
        }
        public async Task<bool> Update(Employee employee)
        {
            if (employee.JobTitleId != null && employee.DepartmentId!=null && employee.LocationId!=null)
            {
                int result = await _roleDepLocLinkRepository.GetRoleDepLocLinkId(employee.JobTitleId, employee.DepartmentId, employee.LocationId);
                if (result > 0)
                {
                    employee.RoleDeptLocId = result;
                    return await _employeeRepository.UpdateEmployee(_mapper.Map<EmployeeEntity>(employee));
                }
            }
            else
            {
                return await _employeeRepository.UpdateEmployee(_mapper.Map<EmployeeEntity>(employee));
            }
            return false;
        }
        public async Task<bool> DeleteEmployees(string [] ids)
        {
                if(await _employeeRepository.DeleteEmployees(ids))
                {
                    return true;
                }
                return false;  
        }
        public async Task<List<Employee>> GetAllEmployees(int pageNumber, int recordsPerPage)
        {
            return _mapper.Map<List<Employee>>( await _employeeRepository.GetEmployees(pageNumber,recordsPerPage));
        }
        public async Task<List<Employee>> GetEmployeesWithNotAssignedRole(string name)
        {
            return _mapper.Map<List<Employee>>(await _employeeRepository.GetEmployeesWithNotAssignedRole(name));
        }
        public async Task<Employee> GetEmployeeById(string  id)
        {
            return _mapper.Map<Employee>(await _employeeRepository.GetEmployeeById(id));
        }

        public async Task<List<string>> GetAllEmployeeIds()
        {
            return await _employeeRepository.GetAllEmployeeIds();
        }
        public async Task<List<Employee>> ApplyFilter(Filter filterEmployee, int pageNumber, int recordsPerPage)
        {
            return _mapper.Map<List<Employee>>(await _employeeRepository.ApplyFilters(filterEmployee,pageNumber,recordsPerPage));
        }
        public async Task<IEnumerable<Employee>> ApplySorting(string property, string order, int pageNumber, int recordsPerPage)
        {
            return  _mapper.Map<List<Employee>>(await _employeeRepository.ApplySorting(property, order,pageNumber,recordsPerPage));
        }
        public async Task<List<Employee>> GetEmployeesByRoleId(int id)
        {
            return _mapper.Map<List<Employee>>(await _employeeRepository.GetEmployeesByRoleId(id));
        }
        public async Task<int> GetEmployeesCount()
        {
            return await  _employeeRepository.GetEmployeesCount();
        }


    }

}
