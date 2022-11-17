using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuri.WorkerService.SendSMS.Settings
{
    public sealed class WorkerSetting
    {
        public string Prueba { get; set; }
        public ConfigWorker SettingsWorkerSenderSMS { get; set; }

        public WorkerSetting()
        {
            SettingsWorkerSenderSMS = new ConfigWorker();
            Prueba = string.Empty;
        }
    }

    public sealed class ConfigWorker
    {
        public int TiempoSegundoExec { get; set; }
        public ConfigWorker()
        {

        }
    }

}
