using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Data
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {

            var sqlitePath = Path.Combine(AppContext.BaseDirectory, @"..\..\..\db"); 
            Directory.CreateDirectory(sqlitePath);
            var fileName = $"{sqlitePath}\\ShopsRUs.db";
            if (!File.Exists(fileName))
                File.Create(fileName);

            string connectionString = $"Data Source={fileName}";
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
            return services;
        }
    }
}
