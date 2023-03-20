using CepApp.Domain.Adapters;
using CepApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CepApp.Gateway.Adapter.Apis
{
    public class ViaCep : IViaCep
    {
        private readonly IHttpService _http;
        public ViaCep(IHttpService htpp)
            => _http = htpp;


        public ResponseCepDto BuscarEndereco(string cep)
            => _http.Get<ResponseCepDto>($"https://viacep.com.br/ws/{cep}/json/");


        public ResponseCepDto BuscarCep(string uf, string cidade, string logradouro)
            => _http.Get<ResponseCepDto>($"https://viacep.com.br/ws/{uf}/{cidade}/{logradouro}/json/");

    }
}
