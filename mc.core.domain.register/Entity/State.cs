using mc.core.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mc.core.domain.register.Entity
{
    public class State: BaseEntity, IEquatable<State>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Initials { get; set; }
        public virtual Country Country { get; set; }

        public override void Dispose()
        {
            if (this.Country != null)
            {
                this.Country.Dispose();
            }
        }

        public bool Equals(State other)
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
    }
}
