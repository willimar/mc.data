using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository.Person;
using mc.core.exception.ValuesException;
using mc.navigator.domain.Interfaces;
using System;
using System.Linq;
using System.Net.Http.Headers;

namespace mc.cep.service
{
    public class CepService
    {
        private readonly INavigator _navigator;
        private readonly IProviderService<Address> _provider;
        private readonly IAddressRepository _repository;

        public CepService(INavigator navigator, IProviderService<Address> provider, IAddressRepository repository)
        {
            this._navigator = navigator;
            this._provider = provider;
            this._repository = repository;
        }

        public Address Get(string value)
        {
            var cep = value.Replace("-", string.Empty);

            if (cep.Length != 8)
            {
                throw new InvalidValueException(nameof(cep));
            }

            cep = cep.Insert(5, "-");

            var list = this._repository.GetData(a => a.PostalCode.Equals(cep));

            if (list.Any())
            {
                return list.FirstOrDefault();
            }
            else
            {
                if (this._provider.Method == Method.post)
                    this._navigator.Form = this._provider.Form;

                this._navigator.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                this._navigator.Headers.Add("Accept-Language", "pt-BR,pt;q=0.8,en-US;q=0.5,en;q=0.3");
                this._navigator.Headers.Add("Connection", "Keep-alive");
                this._navigator.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:66.0) Gecko/20100101 Firefox/66.0");
                //this._navigator.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var response = this._navigator.Navigate(new Uri(this._provider.GetUrl(cep)), this._provider.Method);
                var address = this._provider.Parser(response);
                response = string.Empty;
                
                if (address.IsValid())
                {
                    this._repository.AppenData(address);
                    return address;
                }
                else
                {
                    return null;
                }
            }            
        }
    }
}
