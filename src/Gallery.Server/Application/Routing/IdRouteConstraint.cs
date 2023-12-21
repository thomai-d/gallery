
using Gallery.Server.Domain;
using System.Globalization;

namespace Gallery.Server.Application.Routing
{
    /// <summary>
    /// Route constraint that only allows A-Z, a-z, 0-9 and the dash '-'.
    /// </summary>
    public class IdRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.TryGetValue(routeKey, out var routeValue))
                return false;

            return routeValue is string id
                && Id.IsValid(id);
        }
    }
}
