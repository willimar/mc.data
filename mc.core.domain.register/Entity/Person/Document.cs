using mc.core.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mc.core.domain.register.Entity.Person
{
    public class Document: BaseEntity, IEquatable<Document>
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime EmissionDate { get; set; }
        public string Complement { get; set; }

        public bool Equals(Document other)
        {
            if (this.Id.Equals(other.Id))
            {
                return true;
            }

            if (this.Name.Equals(other.Name) && this.Value.Equals(other.Value))
            {
                return true;
            }

            return false;
        }
    }
}
