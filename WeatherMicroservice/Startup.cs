using System.Reflection;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenTracing;
using OpenTracing.Util;

namespace WeatherMicroservice;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ITracer>(serviceProvider =>
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var reporter = new RemoteReporter.Builder()
                .WithLoggerFactory(loggerFactory)
                .WithSender(new UdpSender())
                .Build();
            var serviceName = Assembly.GetEntryAssembly()!.GetName().Name;
            var tracer = new Tracer.Builder(serviceName)
                .WithSampler(new ConstSampler(true))
                .WithLoggerFactory(loggerFactory)
                .WithReporter(reporter)
                .Build();
            GlobalTracer.Register(tracer);
            return tracer;
        });

        services.AddOpenTracing();
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherMicroservice", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherMicroservice v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
