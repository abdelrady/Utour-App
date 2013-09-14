using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace ITI.Common.Utilities.Domain.Core.Specification
{
    /// <summary>
    /// True specification
    /// </summary>
    /// <typeparam name="TEntity">Type of entity in this specification</typeparam>
    [DataContract(IsReference = true)]
#if !SILVERLIGHT
    [Serializable]
#endif 
    public class TrueSpecification<TEntity>
        : Specification<TEntity>
        where TEntity : class,new()
    {
        #region -- Specification overrides --

        /// <summary>
        /// <see cref="ITI.Common.Utilities.Domain.Core.Specification.Specification{TEntity}"/>
        /// </summary>
        /// <returns><see cref="ITI.Common.Utilities.Domain.Core.Specification.Specification{TEntity}"/></returns>
        public override System.Linq.Expressions.Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            Expression<Func<TEntity, bool>> trueExpression = t => true;
            return trueExpression;
        }

        #endregion
    }
}
