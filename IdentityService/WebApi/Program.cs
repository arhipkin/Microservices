using Autofac;
using Autofac.Extensions.DependencyInjection;
using WebApi.Configuration;
using WebApi.Configuration.Autofac;
using WebApi.Filters;
using WebApi.Middleware;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using Infrastructure.IdentityRepository;
using Microsoft.EntityFrameworkCore;
using Core.Models.Entities;
using Microsoft.AspNetCore.Identity;
using WebApi.Settings;
using Microsoft.AspNetCore.Hosting;
using Domain.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    var databaseSettings = builder.Configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

    // Register Autofac as DI service provider
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new IdentityModule());
        });

    builder.Services.AddDbContext<IdentityAppDBContext>(options =>
    {
        options.UseNpgsql(builder.Configuration["ConnectionStrings:LocalConnectionString"]);

        if (databaseSettings?.QueryLoggingEnabled ?? false)
        {
            options.EnableSensitiveDataLogging();
            options.LogTo(Log.Logger.Information, new[] { DbLoggerCategory.Database.Command.Name }); 
        }
    });

    builder.Services.AddIdentityCore<AppUser>()
        .AddRoles<AppRole>()
        .AddEntityFrameworkStores<IdentityAppDBContext>()
        .AddSignInManager<SignInManager<AppUser>>()
        .AddUserManager<UserManager<AppUser>>()
        .AddRoleManager<RoleManager<AppRole>>();
        //.AddDefaultTokenProviders();

    JsonConvert.DefaultSettings = () =>
    {
        var settings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = builder.Environment.IsDevelopment() ? Formatting.Indented : Formatting.None
        };
        settings.Converters.Add(new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() });
        return settings;
    };
    builder.Services.AddCors();

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ForMediatrRegistration>());

    builder.Services.AddOptions();
    builder.Services.AddSettings(builder.Configuration);

    // Add services to the container.
    builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ValidateModelFilter>();
            options.Filters.Add<CacheControlFilter>();
        }).AddNewtonsoftJson();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));

    builder.Services.AddAutoMapper(typeof(Program));

    var tokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey = new X509SecurityKey(new X509Certificate2(@"identity_public.cer")),
    };

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opt => opt.TokenValidationParameters = tokenValidationParameters);

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    app.UseSerilogRequestLogging();

    app.UseAppExceptionHandler();
    app.UseOptionsVerbHandler();

    app.UseSwagger();
    app.UseSwaggerUI();    

    // Uncomment this line to enable https redirection
    //app.UseHttpsRedirection();
    app.UseRouting();

    app.UseCors(builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true)
    );

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    using var serviceScoped = app.Services.CreateScope();
    var identityDb = serviceScoped.ServiceProvider.GetService<IdentityAppDBContext>();

    identityDb?.Database.MigrateAsync().Wait();

    app.Run();
}
catch (Exception ex)
{
    // Fix exception when working with ef core migrations. See: https://stackoverflow.com/questions/70247187/microsoft-extensions-hosting-hostfactoryresolverhostinglistenerstopthehostexce
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Logger.Fatal(ex, "An unhandled exception occurred. The application will be closed");
}
finally
{
    Log.Information("Shut down complete...");
    Log.CloseAndFlush();
}