using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EFCore.GenericRepository.GenericServices;
using EFCore.GenericRepository.Interfaces;
using GenericRepositoryUnitTests.Data;
using GenericRepositoryUnitTests.Data.DbEntities;

namespace GenericRepositoryUnitTests
{
    public class TestGenericAsyncService : GenericAsyncService<UnitTestDbContext, Person_SoftUpdatableDbEntity>
    {
        public TestGenericAsyncService(IGenericRepository<UnitTestDbContext, Person_SoftUpdatableDbEntity> genericRepo) : base(genericRepo)
        {
        }
    }
    public class TestGenericAsyncServiceWithDto : GenericAsyncService<UnitTestDbContext, Person_SoftUpdatableDbEntity, Person_SoftUpdatableDbEntityDto>
    {
        public TestGenericAsyncServiceWithDto(IGenericRepository<UnitTestDbContext, Person_SoftUpdatableDbEntity> genericRepo, IMapper mapper) : base(genericRepo, mapper)
        {
        }
    }
}
