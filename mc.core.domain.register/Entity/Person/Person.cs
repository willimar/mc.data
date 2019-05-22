using mc.core.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mc.core.domain.register.Entity.Person
{
    public enum Gender
    {
        male,
        female
    }

    public class Person : BaseEntity, IEquatable<Person>
    {        
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public virtual IEnumerable<PersonalContact> PersonalContacts { get; set; }
        public virtual IEnumerable<Person> Dependents { get; set; }    
        public virtual IEnumerable<Address> Addresses { get; set; }
        public virtual IEnumerable<Document> Documents { get; set; }

        public override void Dispose()
        {
            if ((this.PersonalContacts != null) && this.PersonalContacts.Any())
            {
                foreach (var item in this.PersonalContacts)
                {
                    item.Dispose();
                }
                this.PersonalContacts = null;
            }

            if ((this.Dependents != null) && this.Dependents.Any())
            {
                foreach (var item in this.Dependents)
                {
                    item.Dispose();
                }
                this.Dependents = null;
            }

            if ((this.Addresses != null) && this.Addresses.Any())
            {
                foreach (var item in this.Addresses)
                {
                    item.Dispose();
                }
                this.Addresses = null;
            }

            if ((this.Documents != null) && this.Documents.Any())
            {
                foreach (var item in this.Documents)
                {
                    item.Dispose();
                }
                this.Documents = null;
            }
        }

        public bool Equals(Person other)
        {
            if (this.Id.Equals(other.Id))
            {
                return true;
            }

            var documents = from dOri in this.Documents
                            join dSrc in other.Documents on dOri equals dSrc
                            select new { dOri.Value };

            if (documents.Any())
            {
                return true;
            }

            return false;
        }
    }
}
