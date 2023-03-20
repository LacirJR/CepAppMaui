using CepApp.Domain.Adapters;
using CepApp.Entidades.Districts;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CepApp.Gateway.Adapter
{
    public class HttpService : IHttpService
    {
        public T Get<T>(string url)
        {
            var response = new RestClient().Execute<T>(new RestRequest(url, Method.Get));

            if (response.IsSuccessful && response.Data != null)
                return response.Data;


            throw new WebException($"Não foi possível realizar o Get na url: {url}");
        }
    }
}
