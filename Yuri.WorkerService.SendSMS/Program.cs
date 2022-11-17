using Microsoft.Extensions.Configuration;
using Yuri.Background.Transversal.Common;
using Yuri.WorkerService.SendSMS;
using Yuri.WorkerService.SendSMS.Settings;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.Configure<WorkerSetting>(configuration.GetSection(nameof(WorkerSetting)));
        services.AddHostedService<Worker>().AddSingleton<ISenderSMS, SenderSMS>();
    })
    .Build();
await host.RunAsync();
