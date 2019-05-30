using GenericRepositoryUnitTests.Data.DbEntities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitTests
{
    public class AsyncMethodsTest : BaseTestClass
    {

        [Test, Order(1)]
        public async Task Insert()
        {
            var newPerson = new Person_SoftUpdatableDbEntity
            {
                Name = "musa",
                Surname = "demir"
            };


            var insertResult = await _personSoftUpdatableRepo.InsertAsync(newPerson);
            if (insertResult.ID != 0)
            {
                if (await _personSoftUpdatableRepo.AnyAsync(x => x.ID == insertResult.ID))
                    Assert.Pass();
                Assert.Fail("Inserted results id changed but there is no result with returned id. Returned id: " + insertResult.ID);
            }
            Assert.Fail();
        }
        static object[] DivideCases
        {
            get
            {
                var obj = new List<object>();

                foreach (var id in InitializedEntities)
                    obj.Add(new object[] { id, id });

                obj.Add(new object[] { 0, null });
                return obj.ToArray();
            }
        }
        [Test]
        public async Task Find_ExistsEntity()
        {
            int id = InitializedEntities.FirstOrDefault();
            var findResult = await _personSoftUpdatableRepo.FindAsync(id);

            if (findResult != null && findResult.ID == id)
                Assert.Pass();

            Assert.Fail();
        }
        [Test]
        public async Task Find_NotExistsEntity()
        {
            int id = -1;
            var findResult = await _personSoftUpdatableRepo.FindAsync(id);

            if (findResult != null)
                Assert.Fail();

            Assert.Pass();
        }


        [Test]
        public async Task Any_ExistsEntity()
        {
            int id = InitializedEntities.FirstOrDefault();

            if (await _personSoftUpdatableRepo.AnyAsync(x => x.ID == id))
                Assert.Pass();

            Assert.Fail();
        }
        [Test]
        public async Task Any_NotExistsEntity()
        {
            int id = -1;

            if (await _personSoftUpdatableRepo.AnyAsync(x => x.ID == id))
                Assert.Fail();

            Assert.Pass();
        }
        [Test]
        public async Task UpdateTest()
        {
            int id = InitializedEntities.FirstOrDefault();
            var findResult = await _personSoftUpdatableRepo.FindAsync(id);

            findResult.Name = "updateTest2";
            await _personSoftUpdatableRepo.UpdateAsync(findResult);

            findResult = await _personSoftUpdatableRepo.FindAsync(id);

            Assert.AreEqual(findResult.Name, "updateTest2");
        }
    }
}
