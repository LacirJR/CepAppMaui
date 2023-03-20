using CepApp.Domain.Interfaces;

namespace CepApp;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App(ICepService cepService)
	{
		
		InitializeComponent();

		MainPage = new MainPage(cepService);
	}
}
