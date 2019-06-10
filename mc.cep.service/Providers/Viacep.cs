using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.navigator.domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace mc.cep.service.Providers
{
    public class Viacep : IProviderService<Address>
    { 
        public Method Method => Method.get;

        public Dictionary<string, string> Form => throw new NotImplementedException();

        public string GetUrl(params string[] values)
        {
            return string.Format(@"https://viacep.com.br/ws/{0}/json/", values);
        }

        public Address Parser(string values)
        {//
            try
            {
                var json = JsonConvert.DeserializeObject<dynamic>(values);
                var address = new Address()
                {
                    PostalCode = json.cep,
                    PublicPlace = json.logradouro,
                    Complement = json.complemento,
                    District = json.bairro,
                    City = json.localidade,
                    State = json.uf,
                    Country = "Brasil"
                };
                return address;
            }
            catch
            {
                throw new Exception(values);
            }
        }
    }
}
