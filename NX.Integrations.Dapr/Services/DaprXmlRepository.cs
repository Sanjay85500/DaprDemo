using Dapr.Client;
using Microsoft.AspNetCore.DataProtection.Repositories;
using NX.Integrations.Dapr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NX.Integrations.Dapr.Services
{
    public class DaprXmlRepository : IXmlRepository
    {
        private readonly DaprClient _daprClient;
        private readonly string _storeName;
        public DaprXmlRepository(DaprClient daprClient, string storeName)
        {
            _daprClient = daprClient;
            _storeName = storeName;
        }
        public IReadOnlyCollection<XElement> GetAllElements()
        {
            //Console.WriteLine("get all elements : " + _daprClient);
            var data = Task.Run(async () => await GetDataAsync()).Result;
            //Console.WriteLine("data : "+data?.Count ?? "NULL");
            return data.Select(x => XElement.Parse(x)).ToList().AsReadOnly();
        }

        private async Task<List<string>> GetDataAsync()
        {
           return await _daprClient.GetStateAsync<List<string>>(_storeName, Constants.DataProtectionKeysName) ?? new List<string>();
        }

        public async void StoreElement(XElement element, string friendlyName)
        {
            //Console.WriteLine($" friedlyName : {friendlyName}");
            //Console.WriteLine("store elements : " + element.ToString());
            var listElement = await GetDataAsync();
            listElement.Add(element.ToString(SaveOptions.DisableFormatting));
            await _daprClient.SaveStateAsync(_storeName, Constants.DataProtectionKeysName, listElement);
        }
    }
}
