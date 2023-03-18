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
        {
            _http = htpp;
        }

        public ResponseCepDto BuscarEndereco(string cep)
        {
            string url = $"https://viacep.com.br/ws/{cep}/json/";
            return _http.Get<ResponseCepDto>(url);
        }

        public ResponseCepDto BuscarCep(string uf, string cidade, string logradouro)
        {
            string url = $"https://viacep.com.br/ws/{uf}/{cidade}/{logradouro}/json/";

            return _http.Get<ResponseCepDto>(url);
        }
    }
}
