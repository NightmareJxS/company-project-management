using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Data;

namespace PRN231_PE_Trial_API.Controllers
{
    [Authorize(Roles = "Administator")]
    public class ParticipatingProjectController : ODataController
    {
        private readonly ParticipatingProjectRepository _participatingProjectRepository;

        public ParticipatingProjectController(ParticipatingProjectRepository participatingProjectRepository)
        {
            _participatingProjectRepository = participatingProjectRepository;
        }

        [EnableQuery]
        public ActionResult<IQueryable<ParticipatingProject>> Get()
        {
            return Ok(_participatingProjectRepository.GetAll());
        }

        [EnableQuery]
        public ActionResult<ParticipatingProject> GetQuery([FromRoute] string key)
        {
            return Ok(_participatingProjectRepository.Get(x => x.CompanyProjectID.ToString().Contains(key)));
        }
    }
}
