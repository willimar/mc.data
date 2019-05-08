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
            var json = this._navigator.Navigate(new Uri(this._provider.GetUrl(cep)), this._provider.Method);
            return this._provider.Parser(JsonConvert.DeserializeObject<dynamic>(json));
        }
    }
}
