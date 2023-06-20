using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CompanyProjectRepository : RepositoryBase<CompanyProject>
    {
        public List<CompanyProject> GetCompanyProjects()
        {
            return _dbSet.Include(pp => pp.ParticipatingProjects).ThenInclude(e => e.Employee).ThenInclude(d => d.Department).ToList();
        }

        public CompanyProject GetCompanyProject(Expression<Func<CompanyProject, bool>> predicate)
        {
            return _dbSet.Where(predicate).Include(pp => pp.ParticipatingProjects).ThenInclude(e => e.Employee).ThenInclude(d => d.Department).FirstOrDefault();
        }

        public List<CompanyProject> GetCompanyProjectsByProjectName(string target)
        {
            return _dbSet.Where(cp => cp.ProjectName.Contains(target)).Include(pp => pp.ParticipatingProjects).ThenInclude(e => e.Employee).ThenInclude(d => d.Department).ToList();
        }

        public List<CompanyProject> GetCompanyProjectsByEstimatedStartDate(DateTime target)
        {
            return _dbSet.Where(cp => cp.EstimatedStartDate == target).Include(pp => pp.ParticipatingProjects).ThenInclude(e => e.Employee).ThenInclude(d => d.Department).ToList();
        }
    }
}
