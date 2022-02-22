using System.Collections.Generic;
using LackingWebSecurityCore.Data;
using LackingWebSecurityCore.Models.DataLayer;
using LackingWebSecurityCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace LackingWebSecurityCore.Controllers.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService ps = null;

        public PatientsController(IContext _context)
        {
            ps = new PatientService(_context);
        }

        #region InjectionMethods
        [HttpGet]
        [Route("BadGet")]
        public IEnumerable<Patient> BadGet(string mrn)
        {
            return ps.BadGet(mrn);
        }

        [HttpGet]
        [Route("BetterGet")]
        public IEnumerable<Patient> BetterGet(string mrn)
        {
            return ps.BetterGet(mrn);
        }

        [HttpGet]
        [Route("BestGet")]
        public IEnumerable<Patient> BestGet(string mrn)
        {
            return ps.BestGet(mrn);
        }
        #endregion

        #region NonInjectionMethods
        [HttpGet]
        [Route("GetPatients")]
        public IEnumerable<Patient> Get()
        {
            return ps.GetPatients();
        }

        [HttpPost]
        [Route("Reseed")]
        public void Reseed()
        {
            ps.SeedPatients();
        }
        #endregion
    }
}