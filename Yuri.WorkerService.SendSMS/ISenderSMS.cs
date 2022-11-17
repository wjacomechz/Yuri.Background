namespace Yuri.WorkerService.SendSMS
{
    public interface ISenderSMS
    {
        public Task EnviarMensajesXPuertos(string cliente);
    }
}
