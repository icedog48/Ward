using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ward.Model;
using Ward.NancyFx.Resources;

namespace Ward.NancyFx.Automapper.Profiles
{
    public class ResourceToDomainMappingProfile : Profile
    {
        public new string ProfileName
        {
            get { return "ResourceToDomainMappingProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<UserResource, User>();
        }
    }
}