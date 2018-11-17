using System.Linq;

namespace GraphLite
{
    public partial class ExtensionProperty
    {
        public ExtensionProperty()
        {
            ObjectType = "ExtensionProperty";
        }

        /// <summary>
        /// Gets the simple property name of the instance.
        /// NOTE: The B2C internal extension property name has the format: 'extension_[B2C_APP_ID]_propertyName
        /// </summary>
        /// <returns></returns>
        public string GetSimpleName()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return Name;

            var parts = Name.Split('_');
            return parts.Length >= 3
                ? string.Join("_", parts.Skip(2))
                : Name;
        }
    }
}
