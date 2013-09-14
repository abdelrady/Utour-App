﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Contracts;
using ITI.Common.Utilities.Data.Core;
using Domain.DataContracts;
using ITI.Common.Utilities.Diagnostics.Trace;

namespace ITI.Nlayerd.Infrastructure.Data.UTour.Repositories
{
    public class MonumentPhotosRepository : Repository<Monuments_Photos>, IMonumentPhotosRepository
    {
        public MonumentPhotosRepository(IQueryableUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
        }
    }
}
