using MauiAppTest.Shared;
using MauiAppTest.ViewModels;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace MauiAppTest.Services
{
	public class CarServices : ICarServices
	{
		private readonly HttpClient _httpClient;
		private readonly string _baseAddress;
		private readonly string _url;
		private readonly JsonSerializerOptions _jsonSerializerSettings;

		public CarServices(HttpClient httpClient)
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

		public async Task AddAsync(Car car)
		{
			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				Debug.WriteLine("No internet access");
			}

			try
			{
				string jsonCar = JsonSerializer.Serialize(car, _jsonSerializerSettings);
				StringContent content = new StringContent(jsonCar, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/cars", content);
				if (response.IsSuccessStatusCode)
				{
					Debug.WriteLine($"Successfully insert {jsonCar}");
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

		public async Task<IEnumerable<Car>> GetAllAsync(string filter, int pageSize, int pageNumber)
		{
			var result = new List<Car>();

			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				Debug.WriteLine("No internet access");
				return result;
			}

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/cars/list");
				if (response.IsSuccessStatusCode)
				{
					string content = await response.Content.ReadAsStringAsync();
					var cars = JsonSerializer.Deserialize<Pagination<Car>>(content, _jsonSerializerSettings);
					if (cars!.Items.Any())
						result = cars.Items;
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

		public async Task<Car> GetAsync(long id)
		{
			var result = new Car();

			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				Debug.WriteLine("No internet access");
				return result;
			}

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/cars/{id}");
				if (response.IsSuccessStatusCode)
				{
					string content = await response.Content.ReadAsStringAsync();
					result = JsonSerializer.Deserialize<Car>(content, _jsonSerializerSettings);
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

		public async Task RemoveAsync(Car car)
		{
			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				Debug.WriteLine("No internet access");
			}

			try
			{
				HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/cars/{car.Id}");
				if (response.IsSuccessStatusCode)
				{
					Debug.WriteLine($"Successfully remove {car.Id}");
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

		public async Task UpdateAsync(Car car)
		{
			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				Debug.WriteLine("No internet access");
			}

			try
			{
				string jsonCar = JsonSerializer.Serialize(car, _jsonSerializerSettings);
				StringContent content = new StringContent(jsonCar, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/cars/{car.Id}", content);
				if (response.IsSuccessStatusCode)
				{
					Debug.WriteLine($"Successfully Update {jsonCar}");
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
