using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace GraphLite
{
    public static class DirectoryObjectHelper
    {
        public static object Changes<TEntity>(this TEntity entity, Action<TEntity> action) where TEntity : DirectoryObject
        {
            var initialObject = entity.CloneAsJObject();
            action(entity);
            var finalObject = entity.CloneAsJObject();

            var changes = new JObject();
            foreach (var property in initialObject.Properties())
            {
                var finalProperty = finalObject.Property(property.Name);
                if (!JToken.DeepEquals(property, finalProperty))
                {
                    changes.Add(finalProperty);
                }
            }

            var newPropertiesOnFinalObject = finalObject.Properties()
                .Where(prop => initialObject.Property(prop.Name) == null);

            foreach (var property in newPropertiesOnFinalObject)
            {
                changes.Add(property);
            }

            return changes;
        }

        private static JObject CloneAsJObject(this object entity)
        {
            return JObject.Parse(JsonConvert.SerializeObject(entity));
        }
    }
}
