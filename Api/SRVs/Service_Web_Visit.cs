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
using Services.SRVs;

namespace Api.SRVs
{
    public class Service_Web_Visit:Service
    {
        public Service_Web_Visit(ApiContext context, IConfiguration configuration):base(context, configuration)
        {
            
        }

        public Service_Web_Visit()
        {
            
        }
        public async Task<bool> addVisit(DTO_Web_Visit pVisit)
        {
            if (pVisit != null)
            {
                try
                {
                    Web_Visit newVisit = Utls.mapper.Map<Web_Visit>(pVisit);
                    _context.Db_Web_Visit.Add(newVisit);
                    int result = await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw new Exception(e.Message);
                }
            }

            throw new Exception(Errors.unknown_error);
        }

        public async Task<List<DTO_Web_Visit>> getVisitsByIpAddr(string ipAddr)
        {
            List<DTO_Web_Visit> visits = null;
            if (!string.IsNullOrWhiteSpace(ipAddr))
            {
                try
                {
                    List<Web_Visit> visitsSearched = 
                        await _context.Db_Web_Visit.Where(e => e.Ip_Addr == ipAddr).ToListAsync();

                    visits = Utls.mapper.Map<List<Web_Visit>, List<DTO_Web_Visit>>(visitsSearched);
                    return visits;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw new Exception(e.Message);
                }
            }
            throw new Exception(Errors.unknown_error);
        }
        
        public async Task<List<DTO_Web_Visit>> getVisitsByIpAddr(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DTO_Web_Visit>> getDtoWebVisitsByDate(DateTime date)
        {
            if (!(date > DateTime.Today))
            {
                try
                {
                    List<Web_Visit> visits = await getVisitsByDate(date);
                    return Utls.mapper.Map<List<Web_Visit> , List<DTO_Web_Visit>>(visits);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            throw new Exception(Errors.date_incorrect);
        }
        
        public async Task<List<Web_Visit>> getVisitsByDate(DateTime date)
        {
            List<Web_Visit> visits = 
                await _context.Db_Web_Visit.Where(e => (e.Visit_Date.DayOfYear == date.DayOfYear)).ToListAsync();
            return visits;
        }
        

    }
    

}