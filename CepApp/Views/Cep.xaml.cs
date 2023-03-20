using CepApp.Domain.Interfaces;

namespace CepApp.Views;

public partial class Cep : ContentPage
{
    private readonly ICepService _cepService;
    public Cep(ICepService cepService)
    {
        _cepService = cepService;
        InitializeComponent();
    }

    private void BuscarCep(object sender, EventArgs e)
    {
        var endereco = _cepService.BuscarEndereco(((Entry)sender));
        Logradouro.Text = $"Rua/Avenida: {endereco.logradouro}";
        Bairro.Text = $"Bairro: {endereco.bairro}";
        Cidade.Text = $"Cidade: {endereco.localidade}";
        Estado.Text = $"Estado: {endereco.uf}";
        DDD.Text = $"DDD: {endereco.ddd}";
    }

}