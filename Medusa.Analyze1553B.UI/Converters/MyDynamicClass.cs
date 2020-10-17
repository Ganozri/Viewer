using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Medusa.Analyze1553B.UI.Converters
{
    class MyDynamicClass : DynamicObject
    {
        private readonly Dictionary<string, object> _dynamicProperties = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _dynamicProperties.Add(binder.Name, value);

            // additional error checking code omitted

            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _dynamicProperties.TryGetValue(binder.Name, out result);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var property in _dynamicProperties)
            {
                sb.AppendLine($"Property '{property.Key}' = '{property.Value}'");
            }

            return sb.ToString();
        }
    }
}
