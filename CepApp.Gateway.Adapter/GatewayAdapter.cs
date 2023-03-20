using CepApp.Domain.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CepApp.Gateway.Adapter
{
    public class GatewayAdapter : IGatewayAdapter
    {
        public GatewayAdapter(IViaCep viacep)
        {
            this.ViaCep = viacep;
        }

        public IViaCep ViaCep { get; set; }
    }
}
