using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace PRN231_PE_Trial_API.Controllers
{
    [Authorize(Roles = "Administator")]
    public class HrStaffController : ODataController
    {
        private readonly HrStaffRepository _hrStaffRepository;

        public HrStaffController(HrStaffRepository hrStaffRepository)
        {
            _hrStaffRepository = hrStaffRepository;
        }

        [EnableQuery]
        public ActionResult<IQueryable<HRStaff>> Get()
        {
            //// Get the user's claims
            //var userIdClaim = User.FindFirst(ClaimTypes.Name);
            //var roleClaim = User.FindFirst(ClaimTypes.Role);

            return Ok(_hrStaffRepository.GetAll());
        }

        [EnableQuery]
        public ActionResult<HRStaff> Get([FromRoute] string key)
        {
            //// Get the user's claims
            //var userIdClaim = User.FindFirst(ClaimTypes.Name);
            //var roleClaim = User.FindFirst(ClaimTypes.Role);

            return Ok(_hrStaffRepository.Get(x => x.Email.Contains(key)));
        }




    }
}
