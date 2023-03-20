using CepApp.Domain.Interfaces;
using CepApp.Entidades;
using CepApp.Entidades.Districts;
using RestSharp;
using System.Collections.ObjectModel;


namespace CepApp;

public partial class MainPage : TabbedPage
{


    private readonly ICepService _cepService;
    public MainPage(ICepService cepService)
    {
        _cepService = cepService;
        this.Children.Add(new Views.Cep(cepService));
        this.Children.Add(new NavigationPage(new Views.Endereco(cepService)) { Title = "Endereço", IconImageSource= "map_marker.svg" });
        InitializeComponent();
    }

   
    
}

