using CepApp.Entidades;
using CepApp.Entidades.Districts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CepApp.Domain.Interfaces
{
    public interface ICepService
    {
        ResponseCepDto BuscarEndereco(Entry picker);
        IEnumerable<string> ListarUfs();
        IOrderedEnumerable<string> ListarMunicipios(Picker picker);
        ObservableCollection<CepViewModel> BuscarCep(Entry entry, Picker uf, Picker municipio);
    }
}
