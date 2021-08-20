using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Context;
using Api.DTOs;
using Api.Model;
using Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.SRVs
{
    public class ServiceVisit:Service
    {
        public ServiceVisit(ApiContext context, IConfiguration configuration):base(context, configuration)
        {
            
        }

        public ServiceVisit()
        {
            
        }
        public async Task<bool> addVisit(DtoWebVisit pVisit)
        {
            if (pVisit != null)
            {
                try
                {
                    WebVisit newVisit = Utls.mapper.Map<WebVisit>(pVisit);
                    _context.Db_Web_Visit.Add(newVisit);
                    int result = await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw new Exception(e.Message);
                }
            }

            throw new Exception(Errors.UnknownError);
        }

        public async Task<List<DtoWebVisit>> getVisitsByIpAddr(string ipAddr)
        {
            List<DtoWebVisit> visits = null;
            if (!string.IsNullOrWhiteSpace(ipAddr))
            {
                try
                {
                    List<WebVisit> visitsSearched = 
                        await _context.Db_Web_Visit.Where(e => e.IpAddress == ipAddr).ToListAsync();

                    visits = Utls.mapper.Map<List<WebVisit>, List<DtoWebVisit>>(visitsSearched);
                    return visits;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw new Exception(e.Message);
                }
            }
            throw new Exception(Errors.UnknownError);
        }
        
        public async Task<List<DtoWebVisit>> getVisitsByIpAddr(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DtoWebVisit>> getDtoWebVisitsByDate(DateTime date)
        {
            if (!(date > DateTime.Today))
            {
                try
                {
                    List<WebVisit> visits = await getVisitsByDate(date);
                    return Utls.mapper.Map<List<WebVisit> , List<DtoWebVisit>>(visits);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            throw new Exception(Errors.DateIncorrect);
        }
        
        public async Task<List<WebVisit>> getVisitsByDate(DateTime date)
        {
            List<WebVisit> visits = 
                await _context.Db_Web_Visit.Where(e => (e.VisitDate.DayOfYear == date.DayOfYear)).ToListAsync();
            return visits;
        }
        

    }
    

}