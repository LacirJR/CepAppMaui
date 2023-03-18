using CepApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CepApp.Domain.Adapters
{
    public interface IViaCep
    {
        ResponseCepDto BuscarEndereco(string cep);
        ResponseCepDto BuscarCep(string uf, string cidade, string logradouro);

    }
}
