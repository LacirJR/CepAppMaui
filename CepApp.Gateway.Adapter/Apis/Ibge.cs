using CepApp.Domain.Adapters;
using CepApp.Entidades.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CepApp.Gateway.Adapter.Apis
{
    public class Ibge : IIbge
    {
        private readonly IHttpService _http;
        public Ibge(IHttpService htpp)
            => _http = htpp;
        public List<DistrictsResponseDto> GetUfs()
            => _http.Get<List<DistrictsResponseDto>>("https://servicodados.ibge.gov.br/api/v1/localidades/estados");
        public List<CitiesResponseDto> GetCities(string uf)
            => _http.Get<List<CitiesResponseDto>>($"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{uf}/municipios");
    }
}
