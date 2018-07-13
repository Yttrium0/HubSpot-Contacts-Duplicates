using ConsoleApp1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebAPIClientHubspot;

namespace ConsoleApp1 {

    class HubspotClient {
        const string UrlBase = "https://api.hubapi.com/contacts/v1/lists/all/contacts/all/?hapikey=8edee47b-095e-4b0c-8b6e-04b082781704&count=250&vidOffset=";
        private readonly HttpClient client = new HttpClient();


        public static string getUrl(int offset) {
            return $"{UrlBase}{offset}";
        }


        private async Task<string> FetchContacts(int offset) {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var url = getUrl(offset);
            var stringTask = client.GetStringAsync(url);
            var serializer = new DataContractJsonSerializer(typeof(List<Repo>));
            return await stringTask;
        }

        public  async Task<List<Contact>> GetContactsFromHubspot() {
            var continueToFetch = false;
            var offset = 0;
            List<Contact> contacts = new List<Contact>();
            do {
                var contactsString = await FetchContacts(offset);
                RootObject rootContacts = JsonConvert.DeserializeObject<RootObject>(contactsString);
                contacts.AddRange(rootContacts.contacts);
                offset = rootContacts.VidOffset;
                continueToFetch = rootContacts.HasMore;
                Console.WriteLine("Fetch Contacts {0} / Fetch more ? {1}", contacts.Count, continueToFetch);
            } while (continueToFetch);

            return contacts;
        }
    }
}
