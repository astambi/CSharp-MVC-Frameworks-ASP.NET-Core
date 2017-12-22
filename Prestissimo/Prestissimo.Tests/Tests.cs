namespace Prestissimo.Tests
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Prestissimo.Data;
    using Prestissimo.Web.Infrastructure.Mapping;
    using System;

    public class Tests
    {
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            if (!testsInitialized)
            {
                Mapper.Initialize(config => config
                    .AddProfile<AutoMapperProfile>());
                testsInitialized = true;
            }
        }

        public PrestissimoDbContext GetDbContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<PrestissimoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new PrestissimoDbContext(dbContextOptions.Options);

            return dbContext;
        }
    }
}
