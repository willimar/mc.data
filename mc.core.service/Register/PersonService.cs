using mc.core.domain.Interface.Repository;
using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository.Person;
using mc.core.service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mc.core.service.Register
{
    public class PersonService : BaseService<Person>, IPersonService
    {
        public PersonService(IPersonRepository baseRepository) : base(baseRepository)
        {
        }

        public override bool IsValidRecord(Person entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                return false;
            }

            return base.IsValidRecord(entity); 
        }
    }
}
