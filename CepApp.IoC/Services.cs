using CepApp.Application.Services;
using CepApp.Domain.Adapters;
using CepApp.Domain.Interfaces;
using CepApp.Gateway.Adapter;
using CepApp.Gateway.Adapter.Apis;

namespace CepApp.IoC
{
    public static class Services
    {
        public static MauiAppBuilder AddServices(this MauiAppBuilder builder)
        {

            builder.Services.AddSingleton<IHttpService, HttpService>();
            builder.Services.AddSingleton<IViaCep, ViaCep>();
            builder.Services.AddSingleton<IIbge, Ibge>();
            builder.Services.AddSingleton<ICepService, CepService>();

            return builder;
        }

    }
}