using MauiAppTest.Pages.CarPages;
using MauiAppTest.Pages.CustomerPages;

namespace MauiAppTest
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(ManageCarPage), typeof(ManageCarPage));
			Routing.RegisterRoute(nameof(CarListPage), typeof(CarListPage));
			Routing.RegisterRoute(nameof(ManageCustomerPage), typeof(ManageCustomerPage));
			Routing.RegisterRoute(nameof(CustomerListPage), typeof(CustomerListPage));
		}
	}
}
