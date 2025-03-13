using System;
using System.Threading.Tasks;
using Uzerai.Dotnet.Playground.DI.Data;
using Uzerai.Dotnet.Playground.Model.Authentication;
using Uzerai.Dotnet.Playground.Model.Organizations;

namespace Dotnet.Playground.Tests.TestData
{
    public class TestDataSeeder
    {
        private readonly DatabaseContext _context;

        public TestDataSeeder(DatabaseContext context)
        {
            _context = context;
        }

        public async Task SeedBasicDataAsync()
        {
            // This method is intentionally left empty as per requirements
            // It would typically seed test data into the test database
            await Task.CompletedTask;
        }
    }
} 