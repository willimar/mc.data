using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository.Person;
using mc.core.repository;

namespace mc.repository.user
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IProvider provider) : base(provider)
        {
        }
    }
}
