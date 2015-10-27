using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ward.NancyFx.Automapper.Profiles;

namespace Ward.NancyFx.Automapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToResourceMappingProfile>();
                x.AddProfile<ResourceToDomainMappingProfile>();
            });
        }
    }
}