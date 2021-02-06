using System;
using System.Security.Principal;
using System.Linq;
using Calvary.Data;

namespace Calvary.Providers
{
    public static class ConfigurationKeys
    {
        public static readonly string NamaGereja = "NamaGereja";
        public static readonly string KotaGereja = "KotaGereja";
        public static readonly string PICLKKJ = "PICLKKJ";
        public static readonly string KetuaUmum = "KetuaUmum";
        public static readonly string SekretarisUmum = "SekretarisUmum";
    }

    public class ConfigurationProvider : BaseProvider
    {
        public ConfigurationProvider(CalvaryContext context, IPrincipal principal) : base(context, principal)
        {
        }

        public string GetValue(string key)
        {
            var config = context.Configurations.SingleOrDefault(configuration => configuration.Key == key);
            string result = config == null ? String.Empty : config.Value;            
            return result;
        }

        public T GetValue<T>(string key)
        {
            string result = GetValue(key);
            return (T)Convert.ChangeType(result, typeof(T));
        }


        public void SetValue(string key, string value)
        {
            var configuration = context.Configurations.SingleOrDefault(config => config.Key == key);
            if(configuration != null)
            {
                if (configuration.Value == value)
                    return;
            }
            configuration = configuration ?? new Configuration();
            configuration.Key = key;
            configuration.Value = value;
            configuration.ChangedWhen = DateTime.Now;
            configuration.ChangedWho = principal.Identity.Name;
            context.SaveChanges();            
        }

        public void SetValue<T>(string key, T value)
        {
            SetValue(key, Convert.ToString(value));
        }

        public string this[string key]
        {
            get
            {
                return GetValue(key);
            }
            set
            {
                if (GetValue(key) != value)
                {
                    SetValue(key, value);
                }
            }
        }

    }
}
