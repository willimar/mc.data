using mc.core.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mc.core.domain.register.Entity.Person
{
    public class Address: BaseEntity, IEquatable<Address>
    {
        public string PublicPlace { get; set; }
        public string StreetName { get; set; }
        public string FullStreeName { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string PostalCode { get; set; }
        public string District { get; set; }
        public City City { get; set; }

        public override void Dispose()
        {
            if (this.City != null)
            {
                this.City.Dispose();
            }

        }

        public bool Equals(Address other)
        {
            if (this.Id.Equals(other.Id))
            {
                return true;
            }

            if (this.City.Equals(other.City) &&
                this.District.Equals(other.District) &&
                this.PostalCode.Equals(other.PostalCode) &&
                this.PublicPlace.Equals(other.PublicPlace))
            {
                return true;
            }

            return false;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(this.FullStreeName)
                && !string.IsNullOrEmpty(this.PostalCode)
                && this.City.IsValid();
        }
    }
}
