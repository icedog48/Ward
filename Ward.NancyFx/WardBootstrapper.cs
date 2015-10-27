using IceLib.Core.Model.Mapping;
using IceLib.Model;
using IceLib.NancyFx.Attributes;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ward.NancyFx.Resources.Mapping;
using Ward.NancyFx.Modules;
using Ward.Service;
using Ward.Service.Interfaces;

namespace Ward.NancyFx
{
    public class WardBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            
            container.AutoRegister( new Assembly[] {
                typeof(Entity).Assembly, //IceLib.Core
                typeof(HttpVerbAttribute).Assembly, //IceLib.NancyFx
                typeof(IAuthService).Assembly //Ward.Service
            });

            container.Register(typeof(IMapper<,>), typeof(WardMapper<,>)).AsMultiInstance();
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            TokenAuthentication.Enable(pipelines, new TokenAuthenticationConfiguration(container.Resolve<ITokenizer>()));
        }
    }
}
