using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Utilitize.Extensions
{
    public static class ExpressionExtensions
    {

        private class ParamterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> _map;
            public ParamterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this._map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }
            public static Expression Replaceparameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParamterRebinder(map).Visit(exp);
            }
        }


        /// <summary>
        /// Ands the fisrt expression with the second expression in a new one
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Expression<Func<TEntity, bool>> And<TEntity>(this Expression<Func<TEntity, bool>> first, Expression<Func<TEntity, bool>> second)
        {
            return Compose(first, second, Expression.And);
        }

        /// <summary>
        /// Or the fisrt expression with the second expression in a new one
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Expression<Func<TEntity, bool>> Or<TEntity>(this Expression<Func<TEntity, bool>> first, Expression<Func<TEntity, bool>> second)
        {
            return Compose(first, second, Expression.Or);
        }

        #region PrivateMethods
        private static Expression<T> Compose<T>(Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build paramter map (from parameters of second to paramter of first) 
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // Replace parameters in the second lampda expression with parameters from the first
            var secondBody = ParamterRebinder.Replaceparameters(map, second.Body);

            // Apply composation of lampda bodies to parameters from the first expression
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }



        #endregion
    }
}
