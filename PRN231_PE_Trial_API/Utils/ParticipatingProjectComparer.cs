using DAL.Entities;
using System.Diagnostics.CodeAnalysis;

namespace PRN231_PE_Trial_API.Utils
{
    public class ParticipatingProjectComparer : IEqualityComparer<ParticipatingProject>
    {
        public bool Equals(ParticipatingProject? x, ParticipatingProject? y)
        {
            // 2 keys: CompanyProjectID, EmployeeID
            return x.CompanyProjectID == y.CompanyProjectID && x.EmployeeID == y.EmployeeID;
        }

        public int GetHashCode([DisallowNull] ParticipatingProject obj)
        {
            int hCode = obj.CompanyProjectID.GetHashCode() ^ obj.EmployeeID.GetHashCode();
            return hCode.GetHashCode();
        }
    }
}
