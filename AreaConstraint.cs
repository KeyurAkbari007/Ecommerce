using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

public class AreaConstraint : IRouteConstraint
{
    private readonly string _areaName;

    public AreaConstraint(string areaName)
    {
        _areaName = areaName;
    }

    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        object areaValue;
        if (values.TryGetValue("area", out areaValue) && areaValue != null)
        {
            string area = Convert.ToString(areaValue, CultureInfo.InvariantCulture);
            return string.Equals(area, _areaName, StringComparison.OrdinalIgnoreCase);
        }

        return true; // Always matches if no area is specified
    }
}
