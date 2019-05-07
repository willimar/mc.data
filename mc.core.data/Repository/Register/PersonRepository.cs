using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository;
using mc.core.domain.register.Interface.Repository.Person;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.data.Repository.Register
{
    public class PersonRepository: BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(IProvider dbContext) : base(dbContext) { }
    }
}
