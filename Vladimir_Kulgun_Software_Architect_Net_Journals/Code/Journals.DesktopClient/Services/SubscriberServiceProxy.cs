using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Journals.Common;
using Journals.DesktopClient.Models;

namespace Journals.DesktopClient.Services
{
    class SubscriberServiceProxy : ISubscriberService
    {
        private readonly ISettings _settings;
        private readonly IUserSettings _userSettings;

        public SubscriberServiceProxy(ISettings settings, IUserSettings userSettings)
        {
            _settings = settings;
            _userSettings = userSettings;
        }

        public async Task<int> Login(string userName)
        {
            using (var client = CreateClient())
            {
                var response = await client.PostAsync("api/subscribers/login", new {userName});
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<int>();
            }
        }

        public async Task<IEnumerable<Journal>> GetJournals(int userId)
        {
            using (var client = CreateClient())
            {
                var response = await client.GetAsync($"api/subscribers/{userId}/journals");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<IEnumerable<Journal>>();
            }
        }

        public async Task<byte[]> GetJournal(int userId, int journalId)
        {
            byte[] content;
            using (var client = CreateClient())
            {
                var response = await client.GetAsync($"api/subscribers/{userId}/journals/{journalId}");
                response.EnsureSuccessStatusCode();

                content = await response.Content.ReadAsAsync<byte[]>();
            }

            return CryptoUtils.Decrypt(content, _userSettings.UserName);
        }

        /// <summary>
        ///     Creates the HTTP client.
        /// </summary>
        /// <returns></returns>
        protected virtual IHttpClient CreateClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(_settings.WebApiHost) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return new HttpClientWrapper(client);
        }
    }
}