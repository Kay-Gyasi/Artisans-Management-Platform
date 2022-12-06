using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Exceptions;
using AMP.Processors.Messaging;

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
            catch (Exception)
            {
                throw new SmsException($"An error occurred while sending sms to phone: {command.Recipients.First()}");
            }
        }
    }
}