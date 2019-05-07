using mc.core.domain.register.Entity;
using mc.core.domain.register.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.data.Repository.Register
{
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(IProvider dbContext) : base(dbContext) { }
    }
}
