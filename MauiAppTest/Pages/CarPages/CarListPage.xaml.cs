using MauiAppTest.Services;
using MauiAppTest.ViewModels;
using System.Diagnostics;

namespace MauiAppTest.Pages.CarPages;

public partial class CarListPage : ContentPage
{
	private readonly ICarServices _carService;

	public CarListPage(ICarServices carServices)
	{
		InitializeComponent();
		this._carService = carServices;
	}
	protected async override void OnAppearing()
	{
		base.OnAppearing();

		collectionView.ItemsSource = await _carService.GetAllAsync("", 10, 0);
	}

	public async void OnAddBtnClicked(object sender, EventArgs e)
	{
		var navigationParameter = new Dictionary<string, object>
			{
				{ nameof(Car), new Car() }
			};

		await Shell.Current.GoToAsync(nameof(ManageCarPage), navigationParameter);
		Debug.WriteLine("Add button clicked");
	}
	public async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var selectedCar = e.CurrentSelection.FirstOrDefault() as Car;
		var navigationParameter = new Dictionary<string, object>
			{
				{ nameof(Car), selectedCar }
			};
		await Shell.Current.GoToAsync(nameof(ManageCarPage), navigationParameter);
		Debug.WriteLine($"selected car is {selectedCar.Brand} {selectedCar.Model}");
	}
}