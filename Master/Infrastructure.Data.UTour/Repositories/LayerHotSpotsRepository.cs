using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.Caching;
using Domain.Contracts;
using Domain.DataContracts;
using ITI.Common.Utilities.Data.Core;
using ITI.Common.Utilities.Diagnostics.Trace;

namespace ITI.Nlayerd.Infrastructure.Data.UTour.Repositories
{
    public class LayerHotSpotsRepository : Repository<layerhotspot>, ILayerHotSpotsRepository, IDisposable
    {
        public LayerHotSpotsRepository(IQueryableUnitOfWork unitOfWork, ITraceManager traceManager)
            : base(unitOfWork, traceManager)
        {
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }

    public class CashedLayerHotSpotsRepository : LayerHotSpotsRepository
    {
        public CashedLayerHotSpotsRepository(IQueryableUnitOfWork unitOfWork, ITraceManager traceManager) : base(unitOfWork, traceManager)
        {
        }
        public override IEnumerable<layerhotspot> GetFilteredElements(Expression<Func<layerhotspot, bool>> filter)
        {
            //Func<Expression, Expression, bool> eq = ExpressionEqualityComparer.Instance.Equals;
            //eq(filter, filter);

            string casheKey = "HotSpotEntity-" + Db4objects.Db4o.Linq.Expressions.ExpressionEqualityComparer.Instance.GetHashCode(filter);
            var entity = MemoryCache.Default[casheKey] as IEnumerable<layerhotspot>;
            //foreach (KeyValuePair<string, object> x in MemoryCache.Default)
            //{
                
            //}

            if (entity == null)
            {
                //System.IO.File.AppendAllText("c:\\testCaching.txt", "\r\n\r\n" + filter.GetHashCode().ToString(CultureInfo.InvariantCulture));
                entity = base.GetFilteredElements(filter); // make a call to database and get object
                var cacheItem = new CacheItem(casheKey, entity);
                var policy = new CacheItemPolicy();
                MemoryCache.Default.Add(cacheItem, policy);
            }
            return entity;
        }
    }
}
