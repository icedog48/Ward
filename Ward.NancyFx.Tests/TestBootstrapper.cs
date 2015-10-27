using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Nancy;
using Nancy.Authentication.Token;
using Nancy.Authentication.Token.Storage;
using Nancy.TinyIoc;
using Nancy.Testing.Fakes;
using Nancy.Testing;
using Ward.Service.Interfaces;
using Moq;
using Ward.Model;
using IceLib.Storage;
using Ward.Service;
using System.Reflection;
using IceLib.Core.Model.Mapping;
using Ward.NancyFx.Resources.Mapping;
using Ward.NancyFx.Resources;
using Ward.NancyFx.Modules;
using Nancy.Bootstrapper;
using Ward.NancyFx.Automapper;

namespace Ward.NancyFx.Tests
{
    public class TestBootstrapper : WardBootstrapper
    {
        public IRepository<User> UserRepository { get; set; }

        public TestBootstrapper()
        {
            AutoMapperConfig.RegisterMappings();
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            container.Register<IRepository<User>>(this.UserRepository);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<ITokenizer>(new Tokenizer(cfg => cfg.WithKeyCache(new InMemoryTokenKeyStore())));
        }

        protected override IRootPathProvider RootPathProvider
        {
            get
            {
                var assemblyFilePath =
                    new Uri(typeof(WardBootstrapper).Assembly.CodeBase).LocalPath;

                var assemblyPath =
                    Path.GetDirectoryName(assemblyFilePath);

                var rootPath =
                    PathHelper.GetParent(assemblyPath, 3);

                rootPath =
                    Path.Combine(rootPath, @"Ward.NancyFx");

                FakeRootPathProvider.RootPath = rootPath;

                return new FakeRootPathProvider();
            }
        }
    }
}
