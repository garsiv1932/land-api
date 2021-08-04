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
        private readonly Service_Web_Visit _serviceWebVisit;

        public WebVisitController(Service_Web_Visit serviceWebVisit)
        {
            _serviceWebVisit = serviceWebVisit;
        }
        
        [HttpPost]
        public async Task<ActionResult> postVisit([FromHeader] DTO_Web_Visit visit)
        {
            if (visit != null)
            {
                try
                {
                    await _serviceWebVisit.addVisit(visit);
                    return Ok(visit);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return BadRequest(e.Message);
                }
                
            }

            return BadRequest(Errors.unknown_error);
        }

        [HttpGet("ipAddr")]
        public async Task<ActionResult<List<DTO_Web_Visit>>> getVisitsByIpAddr(string ipAddr)
        {
            if (!string.IsNullOrWhiteSpace(ipAddr))
            {
                try
                {
                    List<DTO_Web_Visit> visits= await _serviceWebVisit.getVisitsByIpAddr(ipAddr);
                    return visits;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return BadRequest(e.Message);
                }
            }
            return BadRequest(Errors.unknown_error);
        }

        [HttpGet]
        public async Task<ActionResult<List<DTO_Web_Visit>>> getVisitsByDate(DateTime date)
        {
            if (!(date > DateTime.Today))
            {
                List<DTO_Web_Visit> visits = await _serviceWebVisit.getDtoWebVisitsByDate(date);
            }

            return BadRequest(Errors.date_incorrect);
        }


    }
}