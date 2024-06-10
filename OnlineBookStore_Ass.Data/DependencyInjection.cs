using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OnlineBookStore_Ass.Data.Repository.Implementation;
using OnlineBookStore_Ass.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Data;

    public static class DependencyInjection
{
        public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration)
        {
        string connectionParams = configuration.GetConnectionString("DefaultConnection");
        services.AddScoped<IDbContext>(provider => new DbContext(connectionParams));
        services.AddTransient<IRepository, OnlineBookStore_Ass.Data.Repository.Implementation.Repository>();
        return services;
        }
    }

