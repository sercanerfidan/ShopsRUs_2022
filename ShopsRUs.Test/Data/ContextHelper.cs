using Microsoft.EntityFrameworkCore;
using ShopsRUs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Test.Data
{
    public static class ContextHelper
    {
        public static ApplicationDbContext Create(string dbName = "")
        {
            if (string.IsNullOrEmpty(dbName))
            {
                dbName = Guid.NewGuid().ToString();
            }
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;


            return new ApplicationDbContext(options);
        }
    }
}

