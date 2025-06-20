using System.Text;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using StackExchange.Redis;
using Vinyanext.Application.Abstractions.Authentication;
using Vinyanext.Application.Abstractions.Databases;
using Vinyanext.Infrastructure.Abstractions.Services;
using Vinyanext.Infrastructure.Authentication;
using Vinyanext.Infrastructure.Authorization;
using Vinyanext.Infrastructure.Database;
using Vinyanext.Infrastructure.Services;
using Vinyanext.Shared.Constants;

namespace Vinyanext.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration, bool isGateway = false)
    {
        services
            .AddServices()
            .AddLocalization()
            .AddCache(configuration)
            .AddDatabase(configuration)
            .AddMongo(configuration)
            .AddHangfire(configuration);

        if (isGateway)
        {
            services
                .AddHealthChecks(configuration)
                .AddAuthenticationInternal(configuration)
                .AddAuthorizationInternal();
        }

        return services;
    }


    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<ITokenProvider, TokenProvider>();
        services.AddSingleton<IPasswordProvider, PasswordProvider>();
        services.AddSingleton<IOpenApiService, OpenApiService>();

        services.AddHttpClient();

        services.AddDataProtection();

        return services;
    }

    private static IServiceCollection AddLocalization(this IServiceCollection services)
    {
        services.AddLocalization(o =>  o.ResourcesPath = "Resources" );
        return services;
    }

    private static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConfig = new ConfigurationOptions
        {
            EndPoints = { configuration.GetSection("Redis:Endpoint").Get<string>()! },
            User = configuration.GetSection("Redis:User").Get<string>()!,
            Password = configuration.GetSection("Redis:Password").Get<string>()!,
            AbortOnConnectFail = false
        };

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConfig));
        services.AddSingleton<IDistributedCache>(serviceProvider =>
        {
            var multiplexer = serviceProvider.GetRequiredService<IConnectionMultiplexer>();
            return new RedisCache(new RedisCacheOptions
            {
                InstanceName = $"vyn-{configuration.GetValue<string>("Env")?.ToLower()}-",
                ConfigurationOptions = redisConfig,
                ConnectionMultiplexerFactory = () => Task.FromResult(multiplexer)
            });
        });
        return services;
    }

    private static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(sp =>
            new MongoClient(configuration.GetConnectionString("MongoVinyanext"))
        );
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("PgsqlVinyanext");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Databases.Schema))
                .UseSnakeCaseNamingConvention());

        services.AddTransient<IApplicationDbContextBase>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddTransient<ChangeDbContext>(sp => dbType =>
        {
            IApplicationDbContextBase context = dbType switch
            {
                DbType.PgsqlVinyanextWrite => sp.GetRequiredService<IApplicationDbContextBase>(),
                DbType.PgsqlVinyanexRead => sp.GetRequiredService<IApplicationDbContextBase>(),
                _ => sp.GetRequiredService<IApplicationDbContextBase>()
            };
            return context;
        });

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddRedis(
                connectionMultiplexerFactory: sp => sp.GetRequiredService<IConnectionMultiplexer>()
            )
            .AddNpgSql(configuration.GetConnectionString("PgsqlVinyanext")!)
            .AddMongoDb(
                clientFactory: sp => sp.GetRequiredService<IMongoClient>()
            );

        return services;
    }

    private static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(options =>
        {
            options
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(
                    configure: configure =>
                    {
                        configure.UseNpgsqlConnection(configuration.GetConnectionString("PgsqlVinyanextHangfire")!);
                    },
                    options: new PostgreSqlStorageOptions
                    {
                        SchemaName = Databases.Schema,
                        PrepareSchemaIfNecessary = true
                    }
                );
        });

        // aqui podemos registrar servidores diferentes para cada api
        services.AddHangfireServer(options =>
        {
            options.WorkerCount = 5;
            options.Queues = [
                "default"
            ];
        });
        //services.AddHangfireServer();

        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddScoped<IUserContext, UserContext>();

        return services;
    }

    private static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddAuthorization();

        services.AddScoped<PermissionProvider>();
        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        return services;
    }
}
