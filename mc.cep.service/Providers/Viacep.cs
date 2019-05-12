using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.navigator.domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
        {
            var json = JsonConvert.DeserializeObject<dynamic>(values);
            var address = new Address()
            {
                PostalCode = json.cep,
                PublicPlace = json.logradouro,
                Complement = json.complemento,
                District = json.bairro,
                City = new City
                {
                    Name = json.localidade,
                    State = new State()
                    {
                        Initials = json.uf,
                        Country = new Country()
                        {
                            Name = "Brasil",
                            Initials = "BR"
                        }
                    }
                }
            };

            return address;
        }
    }
}
