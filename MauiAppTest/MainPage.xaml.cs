using MauiAppTest.Pages.CarPages;
using MauiAppTest.Pages.CustomerPages;
using MauiAppTest.Services;

namespace MauiAppTest
{
	public partial class MainPage : ContentPage
	{
		private readonly ICarServices _carService;

		public MainPage(ICarServices carServices)
		{
			InitializeComponent();
			this._carService = carServices;
		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();
		}

		private async void GoToCarListButton_Clicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync(nameof(CarListPage));
		}

		private async void GoToCustomerListButton_Clicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync(nameof(CustomerListPage));
		}
	}

}
