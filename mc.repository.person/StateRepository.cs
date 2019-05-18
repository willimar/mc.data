using mc.core.domain.register.Entity;
using mc.core.domain.register.Interface.Repository;
using mc.core.repository;

namespace mc.repository.person
{
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(IProvider provider) : base(provider) { }
    }
}
