using EFCore.GenericRepository;
using Example.Business;
using Example.Business.Interfaces;
using Example.Data;
using Example.Data.DbEntities;
using Example.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    /// <summary>
    /// This is an base test class. This initialize db connection and run IPersonTestService with given IPersonDbEntity
    /// </summary>
    /// <typeparam name="TPersonDbEntity"></typeparam>
    public abstract class BaseTestClass<TPersonDbEntity> where TPersonDbEntity : BaseDbEntity, IPersonDbEntity, new()
    {
        IPersonTestService<TPersonDbEntity> _personTest;
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ExampleDbContext>(options =>
             options.UseSqlServer("Data Source=LAPTOP-3O58F4FN;database=efcoregenericrepotestdb;trusted_connection=yes;"));

            services.AddScoped(typeof(IExampleDbContextGenericRepository<>), typeof(ExampleDbContextGenericRepository<>));
            services.AddScoped(typeof(IPersonTestService<>), typeof(PersonTestService<>));


            var provider = services.BuildServiceProvider();

            _personTest = provider.GetRequiredService<IPersonTestService<TPersonDbEntity>>();
        }

        [Test, Order(1)]
        public void InsertTest()
        {
            var result = _personTest.InsertTest();
            TestContext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            Assert.Pass();
        }
        [Test, Order(2)]
        public void UpdateTest()
        {
            var result = _personTest.UpdateTest();
            TestContext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            Assert.Pass();
        }
        [Test, Order(3)]
        public void AddOrUpdate_AddTest()
        {
            var result = _personTest.AddOrUpdate_AddTest();
            TestContext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            Assert.Pass();
        }
        [Test, Order(4)]
        public void AddOrUpdate_UpdateTest()
        {
            var result = _personTest.AddOrUpdate_UpdateTest();
            TestContext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            Assert.Pass();
        }
        [Test, Order(5)]
        public void GetAllTest()
        {
            var result = _personTest.GetAllTest();
            TestContext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            Assert.Pass();
        }
        [Test, Order(6)]
        public void DeleteTest()
        {
            var result = _personTest.DeleteTest();
            TestContext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            Assert.Pass();
        }




        [Test, Order(7)]
        public void InsertAsyncTest()
        {
            Task.Run(async () =>
            {
                var result = await _personTest.InsertAsyncTest();
                TestContext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }).Wait();
            Assert.Pass();
        }
        [Test, Order(8)]
        public void UpdateAsyncTest()
        {
            Task.Run(async () =>
            {
                var result = await _personTest.UpdateAsyncTest();
                TestContext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }).Wait();
            Assert.Pass();
        }
        [Test, Order(9)]
        public void AddOrUpdateAsync_AddTest()
        {
            Task.Run(async () =>
            {
                var result = await _personTest.AddOrUpdateAsync_AddTest();
                TestContext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }).Wait();
            Assert.Pass();
        }
        [Test, Order(10)]
        public void AddOrUpdateAsync_UpdateTest()
        {
            Task.Run(async () =>
            {
                var result = await _personTest.AddOrUpdateAsync_UpdateTest();
                TestContext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }).Wait();
            Assert.Pass();
        }
        [Test, Order(11)]
        public void GetAllAsyncTest()
        {
            Task.Run(async () =>
            {
                var result = await _personTest.GetAllAsyncTest();
                TestContext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }).Wait();
            Assert.Pass();
        }
        [Test, Order(12)]
        public void DeleteAsyncTest()
        {
            Task.Run(async () =>
            {
                var result = await _personTest.DeleteAsyncTest();
                TestContext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }).Wait();
            Assert.Pass();
        }

    }
}