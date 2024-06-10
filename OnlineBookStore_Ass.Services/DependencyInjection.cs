using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OnlineBookStore_Ass.Data.Repository.Implementation;
using OnlineBookStore_Ass.Data.Repository.Interface;
using OnlineBookStore_Ass.Services.BooksServices.Interface;
using OnlineBookStore_Ass.Services.BooksServices.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineBookStore_Ass.Services.CacheServices.Interface;
using OnlineBookStore_Ass.Services.CacheServices.Implementation;
using OnlineBookStore_Ass.Services.CartServices.Implementation;
using OnlineBookStore_Ass.Services.LoggingServices.Interface;
using OnlineBookStore_Ass.Services.LoggingServices.Implementation;
using OnlineBookStore_Ass.Services.OnlineBookStore_AssServices.Interface;
using OnlineBookStore_Ass.Services.OnlineBookStore_AssServices.Implementation;
using OnlineBookStore_Ass.Services.PurchasesServices.Interface;
using OnlineBookStore_Ass.Services.PurchasesServices.Implementation;
using OnlineBookStore_Ass.Services.UsersServices.Implementation;

namespace OnlineBookStore_Ass.Services;
public static class DependencyInjection
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IBookServices, BookServices>();
        services.AddTransient<ICacheServices, OnlineBookStore_Ass.Services.CacheServices.Implementation.CacheServices>();
        services.AddTransient<ICartServices, OnlineBookStore_Ass.Services.CartServices.Implementation.CartServices>();
        services.AddTransient<ILoggingServices, OnlineBookStore_Ass.Services.LoggingServices.Implementation.LoggingServices>();
        services.AddTransient<IOnlineBookStoreMainServices, OnlineBookStoreMainServices>();
        services.AddTransient<IPurchaseServices, PurchaseServices>();
        services.AddTransient<IUserServices, UserServices>();

        return services;
    }
}

