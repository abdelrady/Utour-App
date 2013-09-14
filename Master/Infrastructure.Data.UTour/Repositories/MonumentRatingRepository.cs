﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Contracts;
using Domain.DataContracts;
using ITI.Common.Utilities.Data.Core;
using ITI.Common.Utilities.Diagnostics.Trace;

namespace ITI.Nlayerd.Infrastructure.Data.UTour.Repositories
{
    public class MonumentRatingRepository : Repository<Monument_Ratings>, IMonumentRatingRepository
    {
        public MonumentRatingRepository(IQueryableUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
        }
    }
}
