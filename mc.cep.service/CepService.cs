using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.navigator.domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.cep.service
{
    public class CepService<TEntity> where TEntity : class, new()
    {
        private INavigator _navigator;
        private IProviderService<TEntity> _provider;

        public CepService(INavigator navigator, IProviderService<TEntity> provider)
        {
            this._navigator = navigator;
            this._provider = provider;
        }

        public TEntity Get(string cep)
        {
            if (this._provider.Method == Method.post)
                this._navigator.Form = this._provider.Form;

            this._navigator.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            this._navigator.Headers.Add("Accept-Language", "pt-BR,pt;q=0.8,en-US;q=0.5,en;q=0.3");
            this._navigator.Headers.Add("Connection", "Keep-alive");
            this._navigator.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:66.0) Gecko/20100101 Firefox/66.0");

            var response = this._navigator.Navigate(new Uri(this._provider.GetUrl(cep)), this._provider.Method);
            return this._provider.Parser(response);
        }
    }
}
