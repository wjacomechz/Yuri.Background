using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Reflection;

namespace Yuri.Background.Transversal.Common
{
    public static class Utilities
    {
        public static void SetCultureInfo()
        {
            SetCultureInfo("es-EC");
        }

        public static void SetCultureInfo(string Name)
        {
            var forceDotCulture = new CultureInfo(Name);
            forceDotCulture.NumberFormat.NumberDecimalSeparator = ".";
            forceDotCulture.NumberFormat.CurrencyDecimalSeparator = ".";
            forceDotCulture.NumberFormat.NumberGroupSeparator = ",";
            forceDotCulture.NumberFormat.CurrencyGroupSeparator = ",";
            forceDotCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            CultureInfo.DefaultThreadCurrentCulture = forceDotCulture;
            CultureInfo.DefaultThreadCurrentUICulture = forceDotCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = forceDotCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = forceDotCulture;
        }

        public static object GetPropertyValues(Object obj, string nameProperty)
        {
            Type t = obj.GetType();
            PropertyInfo[] props = t.GetProperties();

            return props.FirstOrDefault(x => x.Name.Equals(nameProperty)).GetValue(obj);
        }

        public static T LeerAppSettings<T>(Type type, ref string mensaje, string nameFile = "appsettings.json")
        {
            string assemblyPath = Path.GetDirectoryName(type.Assembly.Location);
            var builder = new ConfigurationBuilder()
             .SetBasePath(assemblyPath)
             .AddJsonFile(nameFile)
             .Build();
            var result = builder.Get<T>();
            if (result == null) mensaje = "Archivo appsettings.json inválido";
            return result;
        }
    }
}