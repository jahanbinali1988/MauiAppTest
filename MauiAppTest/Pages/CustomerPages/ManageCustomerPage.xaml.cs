using MauiAppTest.Services;
using MauiAppTest.ViewModels;
using System.Diagnostics;

namespace MauiAppTest.Pages.CustomerPages;

[QueryProperty(nameof(ViewModels.Customer), "Customer")]
public partial class ManageCustomerPage : ContentPage
{
	private readonly ICustomerService _customerService;
	Customer _customer;
	bool _isNew;

	public Customer Customer
	{
		get => _customer;
		set
		{
			_isNew = IsNew(value);
			_customer = value;
			OnPropertyChanged();
		}
	}

	public ManageCustomerPage(ICustomerService carServices)
	{
		InitializeComponent();

		this._customerService = carServices;
		BindingContext = this;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		DeleteBtn.IsEnabled = !this._isNew;
	}

	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		if (_isNew)
		{
			Debug.WriteLine("---> Add new Item");
			await _customerService.AddAsync(Customer);

		}
		else
		{
			Debug.WriteLine("---> Update an Item");
			await _customerService.UpdateAsync(Customer);
		}

		await Shell.Current.GoToAsync("..");

	}

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		await _customerService.RemoveAsync(Customer);
		await Shell.Current.GoToAsync("..");
	}

	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}

	private bool IsNew(Customer customer)
	{
		if (customer.Id == 0)
			return true;
		return false;
	}
}