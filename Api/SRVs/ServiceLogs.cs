using System;
using System.IO;
using System.Runtime.CompilerServices;
using Api.Context;
using Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.SRVs
{
    public class ServiceLogs
    {
        private readonly IConfiguration _configurations;

        private readonly ApiContext _context;
        //public IConfigurations Configurations { get; }

        public ServiceLogs(ApiContext context, IConfiguration configuration)
        {
            _configurations = configuration;
            _context = context;
            //Configurations = configurations;
        }

        public void AddExcepcion(string message, string className, string methodName, string obj, [CallerLineNumber] int numberNumber = 0)
        {
            try
            {
                string error_log_file = _configurations.GetSection("Error_log_file").Value;
                if (!string.IsNullOrWhiteSpace(error_log_file))
                {
                    using (StreamWriter writer = new(error_log_file, true))
                    {
                        writer.WriteLine(DateTime.Now.ToString() + ": [ln:" + numberNumber + "] " + className + ": " + methodName + "() - " + message + " " + obj + ".");
                    }
                }
            }
            catch (Exception) { }
        }

        public void AddAction(string message, string objectId, Guid userId, string username, string ipClient = "")
        {
            // Logger variables
            System.Diagnostics.StackTrace stackTrace = new (true);
            System.Diagnostics.StackFrame stackFrame = new ();
            string className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            string methodName = stackFrame.GetMethod().Name;

            Logs _logs = new();
            _logs.DateCreated = DateTime.UtcNow;
            _logs.User.UserName = username;
            _logs.Description = message;
            _logs.Info= objectId;
            _logs.IpClient = ipClient;

            _logs.UserId = userId;
            _context.DbLogs.Add(_logs);
            _context.SaveChanges();
        }
    }
}
