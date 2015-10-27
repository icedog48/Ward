using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.NancyFx.Automapper;
using Xunit;

namespace Ward.NancyFx.Tests
{
    public class AutomapperFixture
    {
        public AutomapperFixture()
        {

        }

        [Fact(DisplayName = "Should register all maps")]
        public void Should_register_all_maps()
        {
            AutoMapperConfig.RegisterMappings();

            Mapper.AssertConfigurationIsValid();
        }
    }
}
