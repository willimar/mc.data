using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.domain.Entity
{
    public enum Status
    {
        Active,
        Blocked,
        Deleted
    }

    public abstract class BaseEntity: IDisposable
    {
        public Guid Id { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastChangeDate { get; set; }
        public Status Status { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
            this.RegisterDate = DateTime.Now;
            this.LastChangeDate = DateTime.Now;
            this.Status = Status.Active;
        }

        public abstract void Dispose();
        public abstract bool IsValid();
    }
}
