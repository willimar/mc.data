using mc.core.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mc.core.domain.register.Entity
{
    public class Country: BaseEntity, IEquatable<Country>
    {
        public string Name { get; set; }
        public string Initials { get; set; }
        public string Code { get; set; }

        public override void Dispose()
        {
            
        }

        public bool Equals(Country other)
        {
            if (this.Id.Equals(other.Id))
            {
                return true;
            }

            if (this.Name.Equals(other.Name))
            {
                return true;
            }

            return false;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(this.Name);
        }
    }
}
