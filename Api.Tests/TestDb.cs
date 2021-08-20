using System.Collections.Generic;
using Api.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Tests
{
    public abstract class TestDb
    {
        protected ApiContext ContextConstructor( string dbName)
        {
            var options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(dbName).Options;
            var dbContext = new ApiContext(options);

            return dbContext;
        }
    }
}