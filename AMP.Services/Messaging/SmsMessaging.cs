using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Messaging;
using Microsoft.Extensions.Configuration;

namespace AMP.Services.Messaging
{
    public class SmsMessaging : ISmsMessaging
    {
        private readonly HttpClient _client;

        public SmsMessaging(IHttpClientFactory factory, IConfiguration configuration)
        {
            _client = factory.CreateClient("SmsClient");
            _client.DefaultRequestHeaders.Add("api-key", configuration["Arkesel:ApiKey"]);
        }

        public async Task Send(SmsCommand command)
        {
            try
            {
                await _client.PostAsJsonAsync("sms/send", command, new CancellationToken());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}