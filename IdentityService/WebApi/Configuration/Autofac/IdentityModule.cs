using Autofac;
using WebApi.Controllers;
using Microsoft.Extensions.Options;
using Infrastructure.IdentityRepository;
using Core.Interfaces;
using Domain.Services;

namespace WebApi.Configuration.Autofac
{
    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterGeneric(typeof(ConfigureAppOptions<>)).As(typeof(IConfigureOptions<>)).SingleInstance();
            //builder.RegisterType<TestService>().As<ITestService>();

            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerDependency();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerDependency();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerDependency();
            builder.RegisterType<IdentityDBSeed>().As<IHostedService>();
        }
    }
}
