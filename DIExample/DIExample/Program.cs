using Autofac;
using Autofac.Extensions.DependencyInjection;
using ServiceContracts;
using Services;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddControllersWithViews();
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerLifetimeScope();
    // containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().SingleInstance();
});


/*builder.Services.Add(new ServiceDescriptor(
    typeof(ICitiesService),
    typeof(CitiesService),
    ServiceLifetime.Scoped
));
builder.Services.AddTransient<ICitiesService, CitiesService>();*/
/*builder.Services.AddScoped<ICitiesService, CitiesService>();*/
/*builder.Services.AddSingleton<ICitiesService, CitiesService>();*/

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();