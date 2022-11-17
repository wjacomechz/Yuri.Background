using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Yuri.WorkerService.SendSMS
{
    public class SenderSMS : ISenderSMS
    {
        public async Task EnviarMensajesXPuertos(string cliente)
        {
            int failedSMS = 0;
            List<ColaMensajesSMSQueryDto> lista = ColaSMSResultQueryDto(cliente);
            if (lista == null)
                throw new ApplicationException("No hay puertos disponibles para enviar mensajes....");

            List<Task> tasks = new List<Task>();
            foreach (var item in lista)
            {
                tasks.Add(Task.Run(() => {
                    try
                    {
                        DoSendSMS(item.Id, item.Texto, item.Puerto);
                    }
                    catch (Exception)
                    {
                        Interlocked.Increment(ref failedSMS);
                    }
                    
                }));
            }
            Task t = Task.WhenAll(tasks);
            try
            {
                //t.Wait();
                await t;
            }
            catch { }
            if (t.Status == TaskStatus.RanToCompletion)
                Console.WriteLine("All ping attempts succeeded.");
            else if (t.Status == TaskStatus.Faulted)
                Console.WriteLine("{0} ping attempts failed", failedSMS);
            
        }


        private void DoSendSMS(long id, string mensaje, string puerto)
        {
            Console.WriteLine("SMS ENVIADO....");
        }

        private List<ColaMensajesSMSQueryDto> ColaSMSResultQueryDto (string cliente) 
        { 
            List<ColaMensajesSMSQueryDto> cola = new List<ColaMensajesSMSQueryDto>();
            cola.Add(new ColaMensajesSMSQueryDto()
            {
                Id = 1,
                Puerto = "GS-1.1",
                Texto = "Envio de sms 1"
            });
            cola.Add(new ColaMensajesSMSQueryDto()
            {
                Id = 1,
                Puerto = "GS-1.2",
                Texto = "Envio de sms 2"
            });
            cola.Add(new ColaMensajesSMSQueryDto()
            {
                Id = 1,
                Puerto = "GS-1.3",
                Texto = "Envio de sms 3"
            });
            cola.Add(new ColaMensajesSMSQueryDto()
            {
                Id = 1,
                Puerto = "GS-1.3",
                Texto = "Envio de sms 3"
            });
            cola.Add(new ColaMensajesSMSQueryDto()
            {
                Id = 1,
                Puerto = "GS-1.3",
                Texto = "Envio de sms 3"
            });
            cola.Add(new ColaMensajesSMSQueryDto()
            {
                Id = 1,
                Puerto = "GS-1.3",
                Texto = "Envio de sms 3"
            });
            return cola;
        }

        




    }

    public sealed class ColaMensajesSMSQueryDto
    {
        public long Id { get; set; }
        public string Texto { get; set; }
        public string Puerto { get; set; }
    }
}
