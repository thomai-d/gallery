using FluentAssertions;
using Gallery.Server.Application.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Server.Tests.Application.Routing
{
    public class IdRouteConstraintTests
    {
        [Theory]
        [InlineData("safe")]
        public void Safe_Routes_Should_Match(string id)
        {
            var constraint = new IdRouteConstraint();

            var route = new RouteCollection();

            var result = constraint.Match(
                Substitute.For<HttpContext>(),
                route,
                "id",
                new RouteValueDictionary { { "id", id } },
                RouteDirection.IncomingRequest);

            result.Should().Be(true);
        }

        [Theory]
        [InlineData("unsafe$")]
        [InlineData("..\\")]
        [InlineData("..%5C")]
        [InlineData("ööööööö")]
        public void Unsafe_Routes_Should_Not_Match(string id)
        {
            var constraint = new IdRouteConstraint();

            var route = new RouteCollection();

            var result = constraint.Match(
                Substitute.For<HttpContext>(),
                route,
                "id",
                new RouteValueDictionary { { "id", id } },
                RouteDirection.IncomingRequest);

            result.Should().Be(false);
        }
    }
}
