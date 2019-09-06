using mc.core.domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.domain.register.Entity.Person
{
    public class User : BaseEntity, IEquatable<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid PersonId { get; set; }

        public override void Dispose()
        {
            
        }

        public bool Equals(User other)
        {
            var isUserName = this.UserName.Equals(other.UserName);
            var isUserId = this.Id.Equals(other.Id);

            return isUserId || isUserName;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.Email)
                && !string.IsNullOrWhiteSpace(this.Password)
                && !string.IsNullOrWhiteSpace(this.PersonId.ToString())
                && !string.IsNullOrWhiteSpace(this.UserName)
                && !string.IsNullOrWhiteSpace(this.Id.ToString());
        }
    }
}
