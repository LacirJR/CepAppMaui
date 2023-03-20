using CepApp.Domain.Interfaces;
using CepApp.Entidades;
using CepApp.Entidades.Districts;
using RestSharp;
using System.Collections.ObjectModel;


namespace CepApp;

public partial class MainPage : TabbedPage
{
    ObservableCollection<CepViewModel> ListCep = new();
    public ObservableCollection<CepViewModel> ListCeps { get { return ListCep; } }

    private readonly ICepService _cepService;
    public MainPage(ICepService cepService)
    {
        _cepService = cepService;
        InitializeComponent();
        ListarUfs();
    }

   
    public void ListarUfs()
    {
        try
        {
            var ufs = _cepService.ListarUfs();
            Ufs.Items.Clear();
            foreach (var uf in ufs)
            {
                Ufs.Items.Add(uf);
            }
        }
        catch
        {
            //TODO: Implementar tratamento
        }
    }

    private void Ufs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var cities = _cepService.ListarMunicipios((Picker)sender);
            Cidades.Items.Clear();
            foreach (var city in cities)
            {
                Cidades.Items.Add(city);
            }
        }
        catch
        {
            //TODO: Implementar tratamento
        }


    }

    private void LogradouroRequest_Completed(object sender, EventArgs e)
    {
        try
        {
            if (Cidades.SelectedIndex != -1 && Ufs.SelectedIndex != -1)
            {
                ListaCep.ItemsSource = _cepService.BuscarCep(((Entry)sender), Ufs, Cidades);
            }
        }
        catch
        {
            //TODO: Implementar tratamento
        }
    }
}

