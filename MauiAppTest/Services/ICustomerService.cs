using MauiAppTest.ViewModels;

namespace MauiAppTest.Services
{
	public interface ICustomerService
	{
		Task AddAsync(Customer customer);
		Task RemoveAsync(Customer customer);
		Task UpdateAsync(Customer customer);
		Task<Customer> GetAsync(long id);
		Task<IEnumerable<Customer>> GetAllAsync(string filter, int pageSize, int pageNumber);
	}
}
