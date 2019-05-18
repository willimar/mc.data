using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository.Person;
using mc.core.repository;

namespace mc.repository.person
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(IProvider provider) : base(provider) { }
    }
}
