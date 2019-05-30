using System;
using EFCore.GenericRepository;
using GenericRepositoryUnitTests.Data;
using GenericRepositoryUnitTests.Data.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using EFCore.GenericRepository.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;

namespace GenericRepositoryUnitTests
{
    public abstract class BaseTestClass
    {
        protected TestGenericAsyncServiceWithDto _testGenericAsyncService;
        protected IGenericRepository<UnitTestDbContext, Person_SoftUpdatableDbEntity> _personSoftUpdatableRepo;
        [SetUp]
        public async Task Setup()
        {
            var services = new ServiceCollection();

            services.AddDbContext<UnitTestDbContext>(options =>
             options.UseSqlServer("Data Source=LAPTOP-3O58F4FN;database=efcoregenericrepobasictestdb;trusted_connection=yes;"));

            services.AddSingleton<TestGenericAsyncServiceWithDto>();
            services.AddGenericRepositoryScoped();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var provider = services.BuildServiceProvider();


            _personSoftUpdatableRepo = provider.GetRequiredService<IGenericRepository<UnitTestDbContext, Person_SoftUpdatableDbEntity>>();
            _testGenericAsyncService = provider.GetRequiredService<TestGenericAsyncServiceWithDto>();
            await Initialize();
        }
        public string GetString<T>(T obj) => JsonConvert.SerializeObject(obj);


        protected static List<int> InitializedEntities { get; private set; }
        public async Task Initialize()
        {
            if (await _personSoftUpdatableRepo.AsQueryable().AnyAsync())
            {
                InitializedEntities = await _personSoftUpdatableRepo.AsQueryable().OrderByDescending(x => x.ID).Take(5).Select(x => x.ID).ToListAsync();
                return;
            }

            InitializedEntities = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                var newPerson = new Person_SoftUpdatableDbEntity
                {
                    Name = "musa" + i,
                    Surname = "demir" + i
                };
                var insertResult = await _personSoftUpdatableRepo.InsertAsync(newPerson);
                InitializedEntities.Add(insertResult.ID);
            }

        }
    }
}