using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace GraphLite
{
    /// <summary>
    /// Enumeration of available OData operators.
    /// </summary>
    public enum ODataOperator
    {
        Equals,
        NotEquals,
        LessThan,
        GreaterThan,
        LessThanEquals,
        GreaterThanEquals,
        StartsWith,
        EndsWith,
        Null,
        NotNull,
        In
    }

    /// <summary>
    /// A class that represents an OData query string.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class ODataQuery<TModel>
    {
        private const string TopParameterName = "$top";
        private const string OrderByParameterName = "$orderby";
        private const string FilterParameterName = "$filter";
        private const string SkipTokenParameterName = "$skipToken";

        private int? _top;
        private string _orderClause;
        private string _filterClause;
        private string _skipToken;

        public ODataQuery()
        {
        }

        public ODataQuery<TModel> Top(int top)
        {
            _top = top;
            return this;
        }

        public ODataQuery<TModel> Where<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty value, ODataOperator @operator)
        {
            var filterByProperty = GetColumnName(propertyExpression.Body);
            var valueString = value is string
                ? $"'{value}'"
                : value is bool
                    ? value.ToString().ToLower()
                    : $"{value}";
            var operatorString = GetODataOperatorValue(@operator);

            _filterClause = string.IsNullOrEmpty(_filterClause)
                ? string.Empty
                : _filterClause + " and ";

            _filterClause += $"{filterByProperty} {operatorString} {valueString}";
            return this;
        }

        public ODataQuery<TModel> Where<TItem, TProperty>(
            Expression<Func<TModel, IEnumerable<TItem>>> collectionExpression,
            Expression<Func<TItem, TProperty>> propertyExpression,
            TProperty value, ODataOperator @operator)
        {
            var filterByCollection = GetColumnName(collectionExpression.Body);
            var filterByProperty = GetColumnName(propertyExpression.Body);
            var valueString = value is string
                ? $"'{value}'"
                : value is bool
                    ? value.ToString().ToLower()
                    : $"{value}";
            var operatorString = GetODataOperatorValue(@operator);

            _filterClause = string.IsNullOrEmpty(_filterClause)
                ? string.Empty
                : _filterClause + " and ";

            _filterClause += $"{filterByCollection}/any(x: x/{filterByProperty} {operatorString} {valueString})";

            return this;
        }

        public ODataQuery<TModel> WhereIn<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, params TProperty[] values)
        {
            var filterByProperty = GetColumnName(propertyExpression.Body);
            var valueFunc = new Func<TProperty, string>(value => value is string
                ? $"'{value}'"
                : value is bool
                    ? value.ToString().ToLower()
                    : $"{value}");

            var valuesExpression = string.Join(" or ", values.Select(v => $"{filterByProperty} {GetODataOperatorValue(ODataOperator.Equals)} {valueFunc(v)}"));

            _filterClause = string.IsNullOrEmpty(_filterClause)
                ? string.Empty
                : _filterClause + " and ";
            
            _filterClause +=  $"({valuesExpression})";
            return this;
        }

        public ODataQuery<TModel> OrWhere<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression, TProperty value, ODataOperator @operator)
        {
            var orderByProperty = GetColumnName(propertyExpression.Body);
            var valueString = value is string
                ? $"'{value}'"
                : value is bool
                    ? value.ToString().ToLower()
                    : $"{value}";
            var operatorString = GetODataOperatorValue(@operator);

            _filterClause = string.IsNullOrEmpty(_filterClause)
                ? string.Empty
                : _filterClause + " or ";

            _filterClause += $"{orderByProperty} {operatorString} {value}";
            return this;
        }

        public ODataQuery<TModel> OrderBy(Expression<Func<TModel, object>> propertyExpression)
        {
            var orderByProperty = GetColumnName(propertyExpression.Body);
            _orderClause = string.IsNullOrEmpty(_orderClause) 
                ? orderByProperty
                : string.Join(",", _orderClause, orderByProperty);
            return this;
        }

        public ODataQuery<TModel> OrderByDescending(Expression<Func<TModel, object>> propertyExpression)
        {
            var orderByProperty = GetColumnName(propertyExpression.Body);
            _orderClause = string.IsNullOrEmpty(_orderClause)
                ? orderByProperty
                : string.Join(",", _orderClause, orderByProperty);

            _orderClause += " desc";
            return this;
        }

        public ODataQuery<TModel> SkipToken(string skipToken)
        {
            _skipToken = skipToken;
            return this;
        }

        public override string ToString()
        {
            var queryParameters = HttpUtility.ParseQueryString(string.Empty);
            if (_top.HasValue)
                queryParameters[TopParameterName] = _top.ToString();

            if (_orderClause != null)
                queryParameters[OrderByParameterName] = _orderClause;

            if (_filterClause != null)
                queryParameters[FilterParameterName] = _filterClause;

            if (_skipToken != null)
                queryParameters[SkipTokenParameterName] = $"X'{_skipToken}'";

            return queryParameters.ToString();
        }

        protected static string GetColumnName(Expression exp)
        {
            string columnName = string.Empty;
            Expression memberExpression = exp;
            if (memberExpression is UnaryExpression)
            {
                memberExpression = ((UnaryExpression)memberExpression).Operand;
            }
            while (memberExpression is MemberExpression)
            {
                var mExp = memberExpression as MemberExpression;
                var pi = mExp.Member as PropertyInfo;
                var jsonAttribute = pi?.GetCustomAttribute<JsonPropertyAttribute>();
                var memberName = jsonAttribute?.PropertyName
                    ?? pi?.Name
                    ?? mExp.Member.Name;
                
                if (string.IsNullOrEmpty(columnName))
                    columnName = memberName;
                else
                    columnName = $"{memberName}/{columnName}";
                memberExpression = mExp.Expression;
            }

            return columnName;
        }

        protected string GetODataOperatorValue(ODataOperator @operator)
        {
            switch (@operator)
            {
                case ODataOperator.Equals:
                    return "eq";
                case ODataOperator.NotEquals:
                    return "ne";
                case ODataOperator.LessThan:
                    return "lt";
                case ODataOperator.GreaterThan:
                    return "gt";
                case ODataOperator.LessThanEquals:
                    return "le";
                case ODataOperator.GreaterThanEquals:
                    return "ge";
                case ODataOperator.StartsWith:
                    return "startswith";
                case ODataOperator.EndsWith:
                    return "endswith";
                case ODataOperator.Null:
                    return "eq null";
                case ODataOperator.NotNull:
                    return "ne null";
                case ODataOperator.In:
                default:
                    return string.Empty;
            }
        }
    }
}
