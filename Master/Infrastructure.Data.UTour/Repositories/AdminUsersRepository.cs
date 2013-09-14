using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITI.Common.Utilities.Data.Core;
using Domain.DataContracts;
using Domain.Contracts;
using ITI.Common.Utilities.Diagnostics.Trace;


namespace ITI.Nlayerd.Infrastructure.Data.UTour.Repositories
{
    public class AdminUsersRepository : Repository<Admin_Users>, IAdminUsersRepository
    {
        public AdminUsersRepository(IQueryableUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
        }
    }
}
