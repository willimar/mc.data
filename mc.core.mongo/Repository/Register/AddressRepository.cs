using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository;
using mc.core.domain.register.Interface.Repository.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.mongo.Repository.Register
{
    public class AddressRepository: BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(IProvider dbContext) : base(dbContext) { }
    }
}
