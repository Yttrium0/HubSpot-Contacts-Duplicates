using ConsoleApp1;
using ConsoleApp1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace WebAPIClientHubspot {
    public static class Program {

        const string ContactsJsonFile = "./contacts.json";
        public static int Main(string[] arg) {
            DoOperation();
            Console.WriteLine("\nThe End !");
            Console.ReadKey(true);
            return 0;
        }

        public static async void DoOperation() {

            var contacts = GetContactsFromFile();
            if (contacts.Count  == 0) {
                var hubspot = new HubspotClient();
                contacts = await hubspot.GetContactsFromHubspot();
                SaveContactsInFile(contacts);
                Console.WriteLine($"Fetch Contacts {contacts.Count}");
            }
            Console.WriteLine($"Contacts Count {contacts.Count}");

            var anonymous = contacts.FindAll(c => c.IsAnonymous());
            Console.WriteLine($"There are {anonymous.Count} anonymous");
            anonymous.ForEach(d => {
                Console.WriteLine($" * {d.ToString()}");
                Writer.ImportDataToFile($"\r\n * {d.ToString()}");
            });

            contacts.ForEach(c => {
                var duplicates = contacts.FindAll(c1 => c.IsDuplicateOf(c1));
                if (duplicates.Count > 0) {
                    Console.WriteLine($"{c.ToString()} has duplicates :");
                    duplicates.ForEach(d => {
                        Console.WriteLine($" * {d.ToString()}");
                        Writer.ImportDataToFile($"\r\n * {d.ToString()}");
                    });
                }
            });

        }

        public static void SaveContactsInFile(List<Contact> contacts) {
            File.WriteAllText(ContactsJsonFile, JsonConvert.SerializeObject(contacts));
        }

        public static List<Contact> GetContactsFromFile() {
            try {
                var content = File.ReadAllText(ContactsJsonFile);
                return JsonConvert.DeserializeObject<List<Contact>>(content);
            } catch  {
                return new List<Contact>();
            }
        }

    }
}