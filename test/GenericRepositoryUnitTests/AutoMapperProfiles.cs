using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GenericRepositoryUnitTests.Data.DbEntities;

namespace GenericRepositoryUnitTests
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Person_SoftUpdatableDbEntityDto, Person_SoftUpdatableDbEntity>()
                .ForMember(m => m.CreationTime, opt => opt.Ignore())
                .ForMember(m => m.Deleted, opt => opt.Ignore())
                .ForMember(m => m.LastUpdateTime, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
