using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerProducts
{
    public class ReadSettings
    {
        public string? Uid { get; set; }
        public string? Key { get; set; }
        public void AppSettingsJson()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            Uid = configuration.GetSection("FattureInCloudApiUid").Value;
            Key = configuration.GetSection("FattureInCloudApiKey").Value;
            Console.WriteLine($"UID: {Uid}  KEY: {Key}");

        }

    }
}
