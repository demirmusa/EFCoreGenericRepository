using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GenericRepositoryUnitTests.GenericServicesTests
{
    public class TestGenericAsyncServices : BaseTestClass
    {
        [Test]
        public async Task UpdateTest()
        {
            int id = InitializedEntities.FirstOrDefault();
            var findResult = await _testGenericAsyncService.Get(id);

            Random rnd = new Random();
            string newName= "updateTest" + rnd.Next();

            findResult.Name = newName;
            await _testGenericAsyncService.Update(findResult);

            findResult = await _testGenericAsyncService.Get(id);

            Assert.AreEqual(newName, findResult.Name);
        }
    }
}
