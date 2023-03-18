using CepApp.Entidades;
using CepApp.Entidades.Districts;
using RestSharp;
using System.Collections.ObjectModel;


namespace CepApp;

public partial class MainPage : TabbedPage
{
    ObservableCollection<CepViewModel> ListCep = new();
    public ObservableCollection<CepViewModel> ListCeps { get { return ListCep; } }

    public MainPage()
    {
        InitializeComponent();
        ListarUfs();
    }

    private void BuscarCep(object sender, EventArgs e)
    {
        var cep = ((Entry)sender).Text;
        cep = cep.Replace(".", "").Replace("-", "");

        var client = new RestClient();
        var request = new RestRequest($"https://viacep.com.br/ws/{cep}/json/", Method.Get);
        var endereco = client.Execute<ResponseCepDto>(request).Data;

        Logradouro.Text = $"Rua/Avenida: {endereco.logradouro}";
        Bairro.Text = $"Bairro: {endereco.bairro}";
        Cidade.Text = $"Cidade: {endereco.localidade}";
        Estado.Text = $"Estado: {endereco.uf}";
        DDD.Text = $"DDD: {endereco.ddd}";
    }

    public  void ListarUfs()
    {
        try
        {

            var client = new RestClient();
            var request = new RestRequest("https://servicodados.ibge.gov.br/api/v1/localidades/estados", Method.Get);
            var response = client.Execute<List<DistrictsResponseDto>>(request).Data;

            var ufs = response.OrderBy(x => x.sigla).Select(x => x.sigla);

            foreach (var uf in ufs)
            {
                Ufs.Items.Add(uf);
            }
        }
        catch (Exception e)
        {

        }
    }

    private  void Ufs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            string uf = "";
            if (selectedIndex != -1)
            {
                uf = picker.Items[selectedIndex];
            }
            var client = new RestClient();
            var request = new RestRequest($"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{uf}/municipios", Method.Get);
            var response = client.Execute<List<CitiesResponseDto>>(request).Data;
            

            var cities = response.Select(x => x.nome).OrderBy(x => x);
            Cidades.Items.Clear();
            foreach (var city in cities)
            {
                Cidades.Items.Add(city);
            }
        }
        catch (Exception ex)
        {

        }


    }

    private  void LogradouroRequest_Completed(object sender, EventArgs e)
    {
        try
        {
            var logradouro = ((Entry)sender).Text;
            if (Cidades.SelectedIndex != -1 && Ufs.SelectedIndex != -1)
            {
                var cidade = Cidades.Items[Cidades.SelectedIndex];
                var uf = Ufs.Items[Ufs.SelectedIndex];

                var client = new RestClient();
                var request = new RestRequest($"https://viacep.com.br/ws/{uf}/{cidade}/{logradouro}/json/", Method.Get);
                var response = client.Execute<List<ResponseCepDto>>(request).Data;



                ListCeps.Clear();

                foreach (var endereco in response)
                {
                   
                    ListCeps.Add(new CepViewModel
                    {
                        Cep = endereco.cep,
                        Endereco = $"{endereco.logradouro}, {endereco.bairro} - {endereco.localidade}/{endereco.uf}",
                        Detalhes = $"{endereco.complemento}"
                    });

                }
                ListaCep.ItemsSource = ListCeps;

            }

        } 
        catch (Exception ex)
        {

        }
    }
}

