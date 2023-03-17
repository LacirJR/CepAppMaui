using CepApp.Entidades;
using CepApp.Entidades.Districts;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Runtime.ConstrainedExecution;

namespace CepApp;

public partial class MainPage : TabbedPage
{
    ObservableCollection<CepViewModel> Cep = new();
    public ObservableCollection<CepViewModel> Ceps { get { return Cep; } }

    public MainPage()
    {
        InitializeComponent();
        ListarUfs();
    }

    private void BuscarCep(object sender, EventArgs e)
    {
        var cep = ((Entry)sender).Text;
        cep = cep.Replace(".", "").Replace("-", "");

        HttpClient client = new HttpClient();
        var result = client.SendAsync(
                    new HttpRequestMessage(HttpMethod.Get, $"https://viacep.com.br/ws/{cep}/json/"))
                    .GetAwaiter()
                    .GetResult();

        var response = result.Content.ReadAsStringAsync().Result;
        var endereco = JsonConvert.DeserializeObject<ResponseCep>(response);

        Logradouro.Text = $"Rua/Avenida: {endereco.logradouro}";
        Bairro.Text = $"Bairro: {endereco.bairro}";
        Cidade.Text = $"Cidade: {endereco.localidade}";
        Estado.Text = $"Estado: {endereco.uf}";
        DDD.Text = $"DDD: {endereco.ddd}";
    }

    public async void ListarUfs()
    {

        HttpClient client = new HttpClient();
        var result = await client.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/estados");

        var response = await result.Content.ReadAsStringAsync();
        var districts = JsonConvert.DeserializeObject<List<DistrictsResponse>>(response);
        var ufs = districts.OrderBy(x => x.sigla).Select(x => x.sigla);

        foreach (var uf in ufs)
        {
            Ufs.Items.Add(uf);
        }
    }

    private async void Ufs_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        string uf = "";
        if (selectedIndex != -1)
        {
            uf = picker.Items[selectedIndex];
        }

        HttpClient client = new HttpClient();
        var result = await client.GetAsync($"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{uf}/municipios");

        var response = await result.Content.ReadAsStringAsync();
        var districts = JsonConvert.DeserializeObject<List<CitiesResponse>>(response);

        var cities = districts.Select(x => x.nome).OrderBy(x => x);

        foreach (var city in cities)
        {
            Cidades.Items.Add(city);
        }

    }

    private async void LogradouroRequest_Completed(object sender, EventArgs e)
    {
        var logradouro = ((Entry)sender).Text;
        if (Cidades.SelectedIndex != -1 && Ufs.SelectedIndex != -1)
        {
            var cidade = Cidades.Items[Cidades.SelectedIndex];
            var uf = Ufs.Items[Ufs.SelectedIndex];
            HttpClient client = new HttpClient();
            var result = await client.GetAsync($"https://viacep.com.br/ws/{uf}/{cidade}/{logradouro}/json/");
            var response = await result.Content.ReadAsStringAsync();
            var enderecos = JsonConvert.DeserializeObject<List<ResponseCep>>(response);


            foreach (var endereco in enderecos)
            {
                var cep = new CepViewModel();
                cep.Endereco = $"{endereco.logradouro}, {endereco.bairro}.";
                cep.Cep = $" {endereco.localidade}/{endereco.uf} - {endereco.cep}";
                Ceps.Add(cep);

            }

            ListaCep.ItemsSource = Ceps;
        }
    }
}

