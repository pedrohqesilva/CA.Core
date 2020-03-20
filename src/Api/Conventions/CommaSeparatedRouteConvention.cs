using Api.Attributes;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace Api.Conventions
{
    public class CommaSeparatedRouteConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            SeparatedRouteAttribute attribute = null;
            foreach (var parameter in action.Parameters)
            {
                if (parameter.Attributes.OfType<CommaSeparatedAttribute>().Any())
                {
                    if (attribute == null)
                    {
                        attribute = new SeparatedRouteAttribute(",");
                        parameter.Action.Filters.Add(attribute);
                    }

                    attribute.AddKey(parameter.ParameterName);
                }
            }
        }
    }
}