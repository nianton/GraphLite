using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;

namespace GraphLite.Tests
{
    class TestNameCaseOrderer : Xunit.Sdk.ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases.OrderBy(tc => tc.TestMethod.Method, new TestMethodComparer());
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class PriorityAttribute : Attribute
    {
        public PriorityAttribute(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; }
    }


    public class TestMethodComparer : IComparer<IMethodInfo>
    {
        public int Compare(IMethodInfo x, IMethodInfo y)
        {
            var priorityX = x.GetCustomAttributes(typeof(PriorityAttribute))
                .Select(xa => xa.GetNamedArgument<int>("Priority"))
                .FirstOrDefault();

            var priorityY = y.GetCustomAttributes(typeof(PriorityAttribute))
                .Select(ya => ya.GetNamedArgument<int>("Priority"))
                .FirstOrDefault();

            var priorityCompareResult = priorityX.CompareTo(priorityY);

            return priorityCompareResult == 0
                ? x.Name.CompareTo(y.Name)
                : priorityCompareResult;
        }
    }
}
