using Api.Providers.Factories;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class SeparatedRouteAttribute : Attribute, IResourceFilter
    {
        private readonly SeparatedRouteValueProviderFactory _factory;

        public SeparatedRouteAttribute(string separator)
        {
            _factory = new SeparatedRouteValueProviderFactory(separator);
        }

        public SeparatedRouteAttribute(string key, string separator)
        {
            _factory = new SeparatedRouteValueProviderFactory(key, separator);
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.ValueProviderFactories.Insert(0, _factory);
        }

        public void AddKey(string key)
        {
            _factory.AddKey(key);
        }
    }
}