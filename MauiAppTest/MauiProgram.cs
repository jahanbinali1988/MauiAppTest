using MauiAppTest.Pages.CarPages;
using MauiAppTest.Pages.CustomerPages;
using MauiAppTest.Services;
using Microsoft.Extensions.Logging;

namespace MauiAppTest
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

			builder.Services.AddHttpClient<ICarServices, CarServices>();
			builder.Services.AddHttpClient<ICustomerService, CustomerService>();

			builder.Services.AddSingleton<MainPage>();
			builder.Services.AddTransient<ManageCarPage>();
			builder.Services.AddTransient<CarListPage>();
			builder.Services.AddTransient<ManageCustomerPage>();
			builder.Services.AddTransient<CustomerListPage>();
#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
