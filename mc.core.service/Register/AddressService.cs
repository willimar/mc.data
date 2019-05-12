using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository.Person;
using mc.core.service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.service.Register
{
    public class AddressService : BaseService<Address>, IAddressService
    {
        public AddressService(IAddressRepository baseRepository) : base(baseRepository)
        {
        }

        public override bool IsValidRecord(Address entity)
        {
            if (string.IsNullOrWhiteSpace(entity.PostalCode) ||
                string.IsNullOrWhiteSpace(entity.PublicPlace) ||
                string.IsNullOrWhiteSpace(entity.District) ||
                string.IsNullOrWhiteSpace(entity.City.Name) ||
                string.IsNullOrWhiteSpace(entity.City.State.Name) ||
                string.IsNullOrWhiteSpace(entity.City.State.Country.Name))
            {
                return false;
            }

            return base.IsValidRecord(entity);
        }
    }
}
