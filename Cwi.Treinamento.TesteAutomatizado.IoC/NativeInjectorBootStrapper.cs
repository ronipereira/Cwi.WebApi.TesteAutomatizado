using Cwi.Treinamento.TesteAutomatizado.Domain;
using Cwi.Treinamento.TesteAutomatizado.Domain.Companies;
using Cwi.Treinamento.TesteAutomatizado.Domain.Employees;
using Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Mediator;
using Cwi.Treinamento.TesteAutomatizado.Infra.Repositories;
using Cwi.Treinamento.TesteAutomatizado.Infra.Resources;
using Cwi.Treinamento.TesteAutomatizado.Repository.QueryRepositories;
using Cwi.Treinamento.TesteAutomatizado.Repository.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Serilog;

namespace Cwi.Treinamento.TesteAutomatizado.IoC
{
    /// <summary>
    /// Define a classe responsável pela injeção de dependência.
    /// </summary>
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var log = Log.ForContext(typeof(NativeInjectorBootStrapper));

            log.Information("Injetando dependências");

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddMediatR(typeof(Employee).Assembly);

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeCompanyRepository, EmployeeCompanyRepository>();
            services.AddScoped<IEmployeeCompanyQueryRepository, EmployeeCompanyQueryRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<IUnitOfWork<IEmployeeDbUnitOfWork>>(x =>
            {
                string connectionString = configuration["ConnectionStrings:Employee"];
                return new UnitOfWork<IEmployeeDbUnitOfWork>(new DapperPostgreContext(connectionString));
            });

            var serviceProvider = services.BuildServiceProvider();

            ResourceFactory.Factory = serviceProvider.GetService<IStringLocalizerFactory>();
            ResourceFactory.SetAssembly(typeof(GlobalizationConstants).Assembly.FullName);
        }
    }
}
