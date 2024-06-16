using MauiAppTest.Shared;
using MauiAppTest.ViewModels;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace MauiAppTest.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly HttpClient _httpClient;
		private readonly string _baseAddress;
		private readonly string _url;
		private readonly JsonSerializerOptions _jsonSerializerSettings;

		public CustomerService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5005" : "https://localhost:7199";
			_url = $"{_baseAddress}";

			_jsonSerializerSettings = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			};
			_jsonSerializerSettings.Converters.Add(new CustomLongToStringConverter());
		}

		public async Task AddAsync(Customer customer)
		{
			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				Debug.WriteLine("No internet access");
			}

			try
			{
				string jsonCustomer = JsonSerializer.Serialize(customer, _jsonSerializerSettings);
				StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/customers", content);
				if (response.IsSuccessStatusCode)
				{
					Debug.WriteLine($"Successfully insert {jsonCustomer}");
				}
				else
				{
					Debug.WriteLine("No http response is available");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error happend {ex.Message}");
			}
		}

		public async Task<IEnumerable<Customer>> GetAllAsync(string filter, int pageSize, int pageNumber)
		{
			var result = new List<Customer>();

			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				Debug.WriteLine("No internet access");
				return result;
			}

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/customers/list");
				if (response.IsSuccessStatusCode)
				{
					string content = await response.Content.ReadAsStringAsync();
					var customers = JsonSerializer.Deserialize<Pagination<Customer>>(content, _jsonSerializerSettings);
					if (customers!.Items.Any())
						result = customers.Items;
				}
				else
				{
					Debug.WriteLine("No http response is available");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error happend {ex.Message}");
			}

			return result;
		}

		public async Task<Customer> GetAsync(long id)
		{
			var result = new Customer();

			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				Debug.WriteLine("No internet access");
				return result;
			}

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/customers/{id}");
				if (response.IsSuccessStatusCode)
				{
					string content = await response.Content.ReadAsStringAsync();
					result = JsonSerializer.Deserialize<Customer>(content, _jsonSerializerSettings);
				}
				else
				{
					Debug.WriteLine("No http response is available");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error happend {ex.Message}");
			}

			return result;
		}

		public async Task RemoveAsync(Customer customer)
		{
			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				Debug.WriteLine("No internet access");
			}

			try
			{
				HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/customers/{customer.Id}");
				if (response.IsSuccessStatusCode)
				{
					Debug.WriteLine($"Successfully remove {customer.Id}");
				}
				else
				{
					Debug.WriteLine("No http response is available");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error happend {ex.Message}");
			}
		}

		public async Task UpdateAsync(Customer customer)
		{
			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				Debug.WriteLine("No internet access");
			}

			try
			{
				string jsonCustomer = JsonSerializer.Serialize(customer, _jsonSerializerSettings);
				StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/customers/{customer.Id}", content);
				if (response.IsSuccessStatusCode)
				{
					Debug.WriteLine($"Successfully Update {jsonCustomer}");
				}
				else
				{
					Debug.WriteLine("No http response is available");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error happend {ex.Message}");
			}
		}
	}
}
