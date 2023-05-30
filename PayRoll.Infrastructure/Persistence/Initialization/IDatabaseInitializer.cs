using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Infrastructure.Persistence.Initialization
{
    internal interface IDatabaseInitializer
    {
        Task InitializeDatabasesAsync(CancellationToken cancellationToken);
        //Task InitializeApplicationDbForTenantAsync(ExTenantInfo tenant, CancellationToken cancellationToken, bool reload = true);
    }
}
