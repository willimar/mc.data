using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository.Person;
using mc.core.service;
using mc.user.service.Interface;

namespace mc.user.service.Register
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IUserRepository baseRepository) : base(baseRepository)
        {
        }
    }
}
