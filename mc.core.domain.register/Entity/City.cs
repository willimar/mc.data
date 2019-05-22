using mc.core.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mc.core.domain.register.Entity
{
    public class City: BaseEntity, IEquatable<City>
    {
        public string Name { get; set; }
        public string Initials { get; set; }
        public string Code { get; set; }
        public virtual State State { get; set; }

        public override void Dispose()
        {
            if (this.State != null)
            {
                this.State.Dispose();
            }
        }

        public bool Equals(City other)
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
