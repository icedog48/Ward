using AutoMapper;
using FluentValidation.Results;
using IceLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ward.Model;
using Ward.NancyFx.Resources;

namespace Ward.NancyFx.Automapper.Profiles
{
    public class DomainToResourceMappingProfile : Profile
    {
        public new string ProfileName
        {
            get { return "DomainToResourceMappingProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<User, UserResource>();

            Mapper.CreateMap<ValidationFailure, ValidationError>()
                .ForMember(validationError => validationError.MemberName, config => config.MapFrom(validationFailure => validationFailure.PropertyName))
                .ForMember(validationError => validationError.ErrorMessage, config => config.MapFrom(validationFailure => validationFailure.ErrorMessage));
        }
    }
}