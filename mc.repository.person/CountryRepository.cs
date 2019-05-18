using mc.core.domain.register.Entity;
using mc.core.domain.register.Interface.Repository;
using mc.core.repository;

namespace mc.repository.person
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(IProvider provider) : base(provider) { }
    }
}
