using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>
    {
        public List<Employee> GetEmployees()
        {
            return _dbSet.Include(d => d.Department).ToList();
        }
    }
}
