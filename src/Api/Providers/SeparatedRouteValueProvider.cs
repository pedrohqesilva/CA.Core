using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Providers
{
    public class SeparatedRouteValueProvider : RouteValueProvider
    {
        private readonly HashSet<string> _keys;
        private readonly string _separator;
        private readonly RouteValueDictionary _values;

        public SeparatedRouteValueProvider(RouteValueDictionary values)
            : base(null, values)
        {
        }

        public SeparatedRouteValueProvider(string key, RouteValueDictionary values, string separator)
            : this(new List<string> { key }, values, separator)
        {
        }

        public SeparatedRouteValueProvider(IEnumerable<string> keys, RouteValueDictionary values, string separator)
            : base(BindingSource.Query, values)
        {
            _keys = new HashSet<string>(keys);
            _values = values;
            _separator = separator;
        }

        public override ValueProviderResult GetValue(string key)
        {
            var result = base.GetValue(key);

            if (_keys != null && !_keys.Contains(key))
            {
                return result;
            }

            if (result != ValueProviderResult.None &&
                result.Values.Any(x => x.IndexOf(_separator, StringComparison.OrdinalIgnoreCase) > 0))
            {
                var splitValues = new StringValues(result.Values
                    .SelectMany(x => x.Split(new[] { _separator }, StringSplitOptions.None)).ToArray());

                return new ValueProviderResult(splitValues, result.Culture);
            }

            return result;
        }
    }
}