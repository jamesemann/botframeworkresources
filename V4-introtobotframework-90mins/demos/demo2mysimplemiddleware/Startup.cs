using demo2mysimplemiddleware.Bots;
using demo2mysimplemiddleware.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public Startup(IHostingEnvironment env)
    {
        ContentRootPath = env.ContentRootPath;
    }

    public string ContentRootPath { get; private set; }

    public void ConfigureServices(IServiceCollection services)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(ContentRootPath)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();
        var configuration = builder.Build();
        services.AddSingleton(configuration);

        services.AddBot<SimpleBot>(options =>
        {
            options.CredentialProvider = new ConfigurationCredentialProvider(configuration);

            options.Middleware.Add(new SimpleMiddleware1());
            options.Middleware.Add(new SimpleMiddleware2());
        });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        app.UseStaticFiles();
        app.UseBotFramework();
    }
}