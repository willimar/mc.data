using mc.core.domain.register.ToTools;
using mc.core.repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace mc.repository.cep
{
    public class MySqlCepImportRepository : BaseRepository<MySqlCepImport>
    {
        public MySqlCepImportRepository(IProvider provider) : base(provider) { }
    }
}
