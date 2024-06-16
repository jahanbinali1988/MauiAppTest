using MauiAppTest.Services;
using MauiAppTest.ViewModels;
using System.Diagnostics;

namespace MauiAppTest.Pages.CarPages;

[QueryProperty(nameof(ViewModels.Car), "Car")]
public partial class ManageCarPage : ContentPage
{
	private readonly ICarServices _carServices;
	Car _car;
	bool _isNew;

	public Car Car
	{
		get => _car;
		set
		{
			_isNew = IsNew(value);
			_car = value;
			OnPropertyChanged();
		}
	}

	public ManageCarPage(ICarServices carServices)
	{
		InitializeComponent();

		this._carServices = carServices;
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
			await _carServices.AddAsync(Car);

		}
		else
		{
			Debug.WriteLine("---> Update an Item");
			await _carServices.UpdateAsync(Car);
		}

		await Shell.Current.GoToAsync("..");

	}

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		await _carServices.RemoveAsync(Car);
		await Shell.Current.GoToAsync("..");
	}

	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}

	private bool IsNew(Car car)
	{
		if (car.Id == 0)
			return true;
		return false;
	}
}