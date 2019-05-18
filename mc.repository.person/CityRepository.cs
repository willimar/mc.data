using mc.core.domain.register.Entity;
using mc.core.domain.register.Interface.Repository;
using mc.core.repository;

namespace mc.repository.person
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(IProvider provider) : base(provider) { }
    }
}
