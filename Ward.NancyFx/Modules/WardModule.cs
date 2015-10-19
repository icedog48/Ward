using Nancy;

using IceLib.NancyFx.Extensions;
using IceLib.NancyFx.Attributes;
using IceLib.NancyFx.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.NancyFx.Modules
{
    public abstract class WardModule : NancyModule
    {
        public const string API_PREFIX = "api";
        public const string API_VERSION = "v1";

        public WardModule()
        {
            var route = new RouteHelper()
                .AddPath(API_PREFIX)
                .AddPath(API_VERSION);

            this.ModulePath = route.ToString();

            this.BindRoutes();
        }

        public WardModule(string modulePath) 
        {
            var route = new RouteHelper()
                .AddPath(API_PREFIX)
                .AddPath(API_VERSION)
                .AddPath(modulePath);

            this.ModulePath = route.ToString();
            
            this.BindRoutes();
        }
    }
}
