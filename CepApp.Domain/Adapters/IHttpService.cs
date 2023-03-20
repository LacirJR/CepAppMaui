using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CepApp.Domain.Adapters
{
    public interface IHttpService
    {
        T Get<T>(string url);
    }
}
