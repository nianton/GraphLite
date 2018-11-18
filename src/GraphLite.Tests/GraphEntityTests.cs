using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GraphLite.Tests
{
    [Collection(TestFixtureCollection.Name)]
    public class GraphEntityTests
    {
        readonly TestClientFixture _fixture;

        public GraphEntityTests(TestClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void TestChanges()
        {
            var initialUser = _fixture.TestUser;
            var changes = initialUser.Changes(entity =>
            {
                entity.CompanyName = "New Company Name";
                entity.Country = "New Country Name";
                entity.SetExtendedProperty(_fixture.ExtensionPropertyName, "Yet Another Value");
            });

            var json = JsonConvert.SerializeObject(changes);
        }
    }
}
