using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ParticipatingProjectRepository : RepositoryBase<ParticipatingProject>
    {
        public List<ParticipatingProject> GetParticipatingProjectsByCompanyProjectID(int companyProjectID)
        {
            return _dbSet.Where(id => id.CompanyProjectID == companyProjectID).ToList();
        }
    }
}
