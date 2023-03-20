using CepApp.Entidades.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CepApp.Domain.Adapters
{
    public interface IIbge
    {
        List<DistrictsResponseDto> GetUfs();
        List<CitiesResponseDto> GetCities(string uf);
    }
}
