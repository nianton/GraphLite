using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GraphLite
{
    public static class UserQueryExtensions
    {
        public static ODataQuery<User> WhereExtendedProperty(this ODataQuery<User> query, string propertyName, object value, ODataOperator @operator)
        {
            if (!(query is UserQuery))
            {
                throw new InvalidOperationException("Create a UserQuery instance via the GraphApiClient.UserQueryCreateAsync() in order to user Extended property filters.");
            }                

            return ((UserQuery)query).WhereExtendedProperty(propertyName, value, @operator);
        }

        public static ODataQuery<User> OrWhereExtendedProperty(this ODataQuery<User> query, string propertyName, object value, ODataOperator @operator)
        {
            if (!(query is UserQuery))
            {
                throw new InvalidOperationException("Create a UserQuery instance via the GraphApiClient.UserQueryCreateAsync() in order to user Extended property filters.");
            }

            return ((UserQuery)query).OrWhereExtendedProperty(propertyName, value, @operator);
        }
    }

    public sealed class UserQuery : ODataQuery<User>
    {
        private readonly string _extensionApplicationId;

        internal UserQuery(string extensionApplicationId)
        {
            _extensionApplicationId = extensionApplicationId;
        }

        public UserQuery WhereExtendedProperty(string propertyName, object value, ODataOperator @operator)
        {
            AppendFilterClause(propertyName, value, @operator, false);
            return this;
        }

        public UserQuery OrWhereExtendedProperty(string propertyName, object value, ODataOperator @operator)
        {
            AppendFilterClause(propertyName, value, @operator, true);
            return this;
        }

        private void AppendFilterClause(string propertyName, object value, ODataOperator @operator, bool useOr)
        {
            var filterByProperty = $"extension_{_extensionApplicationId.Replace("-", string.Empty)}_{propertyName}";
            var valueString = value is string
                ? $"'{value}'"
                : value is bool
                    ? value.ToString().ToLower()
                    : $"{value}";
            var operatorString = GetODataOperatorValue(@operator);
            var filterClause = $"{filterByProperty} {operatorString} {valueString}";
            AppendFilterClause(filterClause, useOr);
        }
    }
}
