using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DTOs;
using Api.SRVs;
using Api.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/visita")]
    [ApiExplorerSettings(GroupName = "Service - Visitas")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WebVisitController : Controller
    {
        private readonly ServiceVisit _serviceVisit;
        private readonly ServiceLogs _serviceLogs;

        public WebVisitController(ServiceVisit serviceVisit,ServiceLogs serviceLogs)
        {
            _serviceVisit = serviceVisit;
            _serviceLogs = serviceLogs;
        }
        
        [HttpPost]
        public async Task<ActionResult> postVisit([FromHeader] DtoWebVisit visit)
        {
            if (visit != null)
            {
                try
                {
                    await _serviceVisit.addVisit(visit);
                    return Ok(visit);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return BadRequest(e.Message);
                }
                
            }

            return BadRequest(Errors.UnknownError);
        }

        [HttpGet("ipAddr")]
        public async Task<ActionResult<List<DtoWebVisit>>> getVisitsByIpAddr(string ipAddr)
        {
            if (!string.IsNullOrWhiteSpace(ipAddr))
            {
                try
                {
                    List<DtoWebVisit> visits= await _serviceVisit.getVisitsByIpAddr(ipAddr);
                    return visits;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return BadRequest(e.Message);
                }
            }
            return BadRequest(Errors.UnknownError);
        }

        [HttpGet]
        public async Task<ActionResult<List<DtoWebVisit>>> getVisitsByDate(DateTime date)
        {
            if (!(date > DateTime.Today))
            {
                List<DtoWebVisit> visits = await _serviceVisit.getDtoWebVisitsByDate(date);
            }

            return BadRequest(Errors.DateIncorrect);
        }


    }
}