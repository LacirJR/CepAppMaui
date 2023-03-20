using CepApp.Domain.Adapters;
using CepApp.Domain.Interfaces;
using CepApp.Entidades;
using CepApp.Entidades.Districts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CepApp.Application.Services
{
    public class CepService : ICepService
    {
        private readonly IViaCep _viacep;
        private readonly IIbge _ibge;
        public CepService(IViaCep viacep, IIbge ibge)
        {
            _viacep = viacep;
            _ibge = ibge;
        }
        public ResponseCepDto BuscarEndereco(Entry picker)
        {
            var cep = picker.Text.Replace(".", "").Replace("-", "");
            return _viacep.BuscarEndereco(cep);

        }

        public IEnumerable<string> ListarUfs()
            => _ibge.GetUfs().OrderBy(x => x.sigla).Select(x => x.sigla);

        public IOrderedEnumerable<string> ListarMunicipios(Picker picker)
        {
            int selectedIndex = picker.SelectedIndex;
            string uf = "";
            if (selectedIndex != -1)
            {
                uf = picker.Items[selectedIndex];
            }
            return _ibge.GetCities(uf).Select(x => x.nome).OrderBy(x => x);
        }

        public ObservableCollection<CepViewModel> BuscarCep(Entry entry, Picker uf, Picker municipio)
        {
            var ceps = new ObservableCollection<CepViewModel>();
            var response = _viacep.BuscarCeps(uf.Items[uf.SelectedIndex], municipio.Items[municipio.SelectedIndex], entry.Text);

            foreach (var endereco in response)
            {

                ceps.Add(new CepViewModel
                {
                    Cep = endereco.cep,
                    Endereco = $"{endereco.logradouro}, {endereco.bairro} - {endereco.localidade}/{endereco.uf}",
                    Detalhes = $"{endereco.complemento}"
                });

            }

            return ceps;
        }
    }
}
