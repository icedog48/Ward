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

namespace Ward.NancyFx.Tests
{
    public class TestBootstrapper : WardBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<ITokenizer>(new Tokenizer(cfg => cfg.WithKeyCache(new InMemoryTokenKeyStore())));
            
            container.Register<IAuthService>(this.AuthService);
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

        public IAuthService AuthService { get; set; }
    }
}
