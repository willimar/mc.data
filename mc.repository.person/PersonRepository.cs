using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository.Person;
using mc.core.repository;

namespace mc.repository.person
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(IProvider provider) : base(provider) { }
    }
}
