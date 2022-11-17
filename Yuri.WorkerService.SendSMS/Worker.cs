using Microsoft.Extensions.Options;

namespace Yuri.WorkerService.SendSMS
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISenderSMS _senderSMS;
        private readonly IOptions<Settings.WorkerSetting> _settings;

        public Worker(ILogger<Worker> logger, ISenderSMS senderSMS, IOptions<Settings.WorkerSetting> settings)
        {
            _logger = logger;
            _senderSMS = senderSMS;
            _settings = settings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int segundos = this._settings.Value.SettingsWorkerSenderSMS.TiempoSegundoExec;
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _senderSMS.EnviarMensajesXPuertos("wjacome");
            }
        }
    }
}