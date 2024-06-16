using MauiAppTest.ViewModels;
using MauiAppTest.Services;

namespace MauiAppTest.Pages.CustomerPages;

public partial class CustomerListPage : ContentPage
{
	private readonly ICustomerService _customerService;

	public CustomerListPage(ICustomerService customerServices)
	{
		InitializeComponent();
		this._customerService = customerServices;
	}
	protected async override void OnAppearing()
	{
		base.OnAppearing();

		collectionView.ItemsSource = await _customerService.GetAllAsync("", 10, 0);
	}

	public async void OnAddBtnClicked(object sender, EventArgs e)
	{
		var navigationParameter = new Dictionary<string, object>
			{
				{ nameof(Customer), new Customer() }
			};

		await Shell.Current.GoToAsync(nameof(ManageCustomerPage), navigationParameter);
	}
	public async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedCustomer = e.CurrentSelection.FirstOrDefault() as Customer;
		var navigationParameter = new Dictionary<string, object>
			{
				{ nameof(Customer), selectedCustomer }
			};
		await Shell.Current.GoToAsync(nameof(ManageCustomerPage), navigationParameter);
	}
}