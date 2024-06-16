using MauiAppTest.ViewModels;

namespace MauiAppTest.Services
{
	public interface ICarServices
	{
		Task AddAsync(Car car);
		Task RemoveAsync(Car car);
		Task UpdateAsync(Car car);
		Task<Car> GetAsync(long id);
		Task<IEnumerable<Car>> GetAllAsync(string filter, int pageSize, int pageNumber);
	}
}
