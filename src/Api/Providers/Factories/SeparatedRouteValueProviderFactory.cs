using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Providers.Factories
{
    public class SeparatedRouteValueProviderFactory : IValueProviderFactory
    {
        private readonly string _separator;
        private HashSet<string> _keys;

        public SeparatedRouteValueProviderFactory(string separator)
            : this((IEnumerable<string>)null, separator)
        {
        }

        public SeparatedRouteValueProviderFactory(string key, string separator)
            : this(new List<string> { key }, separator)
        {
        }

        public SeparatedRouteValueProviderFactory(IEnumerable<string> keys, string separator)
        {
            _keys = keys != null ? new HashSet<string>(keys) : null;
            _separator = separator;
        }

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Insert(0,
                new SeparatedRouteValueProvider(_keys,
                    context.ActionContext.HttpContext.GetRouteData().Values,
                    _separator)
            );

            return Task.CompletedTask;
        }

        public void AddKey(string key)
        {
            if (_keys == null)
            {
                _keys = new HashSet<string>();
            }

            _keys.Add(key);
        }
    }
}