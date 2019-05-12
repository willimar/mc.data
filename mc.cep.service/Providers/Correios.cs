using mc.core.domain.register.Entity.Person;
using mc.navigator.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.cep.service.Providers
{
    public class Correios : IProviderService<Address>
    {
        private string _cep;

        public Method Method => Method.post;
        public Dictionary<string, string> Form { get { return GetForm(); } }

        public Correios(string cep)
        {
            this._cep = cep;
        }

        public string GetUrl(params string[] values)
        {
            return @"http://www.buscacep.correios.com.br/sistemas/buscacep/resultadoBuscaEndereco.cfm";
        }

        private Dictionary<string, string> GetForm()
        {
            var result = new Dictionary<string, string>(1);
            result["CEP"] = this._cep;
            return result;
        }

        public Address Parser(string json)
        {
            throw new NotImplementedException();
        }
    }
}
