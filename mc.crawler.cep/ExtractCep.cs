using mc.cep.service;
using mc.core.domain.register.Entity.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.crawler.cep
{
    internal class ExtractCep
    {
        private CepService<Address> _cepService;
        private CepPieces _cepPieces;

        public ExtractCep(CepService<Address> cepService, CepPieces cepPieces)
        {
            this._cepService = cepService;
            this._cepPieces = cepPieces;
        }

        public void AdjustFieldValues(Address address)
        {
            var city = this._cepPieces.GetCity(c => c.Name.Equals(address.City.Name));
            var state = this._cepPieces.GetState(c => c.Initials.Equals(address.City.State.Initials));
            var country = this._cepPieces.GetCoutry(c => c.Name.Equals("Brasil"));

            if (!(city is null))
            {
                address.City = city;
            }
            if (!(state is null))
            {
                address.City.State = state;
            }
            if (!(country is null))
            {
                address.City.State.Country = country;
            }
        }

        public Address GetAddress(string cep)
        {
            var address = this._cepService.Get(cep);
            return address;
        }
    }
}
