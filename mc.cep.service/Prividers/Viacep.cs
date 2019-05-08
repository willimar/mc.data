using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.navigator.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.cep.service.Prividers
{
    public class Viacep : IProviderService<Address>
    { 
        public Method Method => Method.get;

        public string GetUrl(params string[] values)
        {
            return string.Format(@"https://viacep.com.br/ws/{0}/json/", values);
        }

        public Address Parser(dynamic json)
        {
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
