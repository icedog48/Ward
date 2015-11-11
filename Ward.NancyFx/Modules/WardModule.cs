using Nancy;
using Nancy.ModelBinding;

using IceLib.NancyFx.Attributes;
using IceLib.NancyFx.Helpers;
using IceLib.NancyFx.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.NancyFx.Modules
{
    public abstract class WardModule : APIModule
    {
        public const string API_PREFIX = "api";

        public WardModule()
        {
            var route = new RouteHelper()
                .AddPath(API_PREFIX);

            this.ModulePath = route.ToString();
        }

        public WardModule(string modulePath) 
        {
            var route = new RouteHelper()
                .AddPath(API_PREFIX)
                .AddPath(modulePath);

            this.ModulePath = route.ToString();
        }
    }
}
