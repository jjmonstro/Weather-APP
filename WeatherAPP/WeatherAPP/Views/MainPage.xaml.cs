using WeatherAPP.Services;
using WeatherAPP.ViewModels;

namespace WeatherAPP.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();
		ResponseService responseService = new ResponseService();
		responseService.GetResponseByIdAsync("São Paulo,br");
	}
}