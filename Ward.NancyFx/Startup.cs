using Owin;
using Ward.NancyFx.Automapper;

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        AutoMapperConfig.RegisterMappings();

        app.UseNancy();
    }
}