using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;

namespace GraphLite.Tests
{
    class TestNameCaseOrderer : Xunit.Sdk.ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases.OrderBy(tc => tc.TestMethod.Method.Name);
        }
    }
}
