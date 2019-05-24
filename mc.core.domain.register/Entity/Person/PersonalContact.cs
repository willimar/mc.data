using mc.core.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mc.core.domain.register.Entity.Person
{
    public enum ContactTypes
    {
        phone,
        celphone,
        email,
        blog,
        website
    }

    public class PersonalContact: BaseEntity, IEquatable<PersonalContact>
    {
        public ContactTypes ContactType { get; set; }
        public string Value { get; set; }

        public override void Dispose()
        {
            
        }

        public bool Equals(PersonalContact other)
        {
            if (this.Id.Equals(other.Id))
            {
                return true;
            }

            if (this.Value.Equals(other.Value))
            {
                return true;
            }

            return false;
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
