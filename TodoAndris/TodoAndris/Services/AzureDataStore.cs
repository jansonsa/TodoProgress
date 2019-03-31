using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using TodoAndris.Models;

namespace TodoAndris.Services
{
	public class AzureDataStore : IDataStore<Item>
	{
		HttpClient client;
		IEnumerable<Item> items;

		public AzureDataStore()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

			items = new List<Item>();
		}

		public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
		{
			if (forceRefresh && CrossConnectivity.Current.IsConnected)
			{
				var json = await client.GetStringAsync($"api/item");
				items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
			}

			return items;
		}

		public async Task<Item> GetItemAsync(string id)
		{
			if (id != null && CrossConnectivity.Current.IsConnected)
			{
				var json = await client.GetStringAsync($"api/item/{id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
			}

			return null;
		}

		public async Task<bool> AddItemAsync(Item item)
		{
            try
            {
                if (item == null || !CrossConnectivity.Current.IsConnected)
                    return false;

                var serializedItem = JsonConvert.SerializeObject(item);

                var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
		}

		public async Task<bool> UpdateItemAsync(Item item)
		{
            try
            {
                if (item == null || item.Id == null || !CrossConnectivity.Current.IsConnected)
                    return false;

                var serializedItem = JsonConvert.SerializeObject(item);

                var response = await client.PutAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
		}

		public async Task<bool> DeleteItemAsync(string id)
		{
			if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
				return false;

			var response = await client.DeleteAsync($"api/item/{id}");

			return response.IsSuccessStatusCode;
		}
	}
}