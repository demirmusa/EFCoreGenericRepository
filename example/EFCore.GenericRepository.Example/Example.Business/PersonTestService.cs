using EFCore.GenericRepository;
using Example.Business.Interfaces;
using Example.Data;
using Example.Data.DbEntities;
using Example.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Business
{

    /// <summary>
    /// this is a test service for SoftUpdatableDbEntity  
    /// </summary>
    public class PersonTestService<TPersonDbEntity> : IPersonTestService<TPersonDbEntity>
        where TPersonDbEntity : BaseDbEntity, IPersonDbEntity, new()
    {
        IExampleDbContextGenericRepository<TPersonDbEntity> _accountRepo;
        public PersonTestService(
            IExampleDbContextGenericRepository<TPersonDbEntity> accountRepo
            )
        {
            _accountRepo = accountRepo;
        }
        #region sync methods 
        public TPersonDbEntity InsertTest()
        {
            return _accountRepo.Insert(
                 new TPersonDbEntity
                 {
                     Name = "Musa",
                     Surname = "Demir"
                 });
        }

        public TPersonDbEntity UpdateTest()
        {
            var inserted = _accountRepo.AsQueryable().FirstOrDefault();//get first one which is not sign deleted
            inserted.Name = "Updated name";
            return _accountRepo.Update(inserted);
        }
        public TPersonDbEntity AddOrUpdate_AddTest() =>
            _accountRepo.AddOrUpdate(
                new TPersonDbEntity
                {
                    Name = "selinay",
                    Surname = "Aktas"
                });

        public TPersonDbEntity AddOrUpdate_UpdateTest()
        {
            var inserted = _accountRepo.AsQueryable().FirstOrDefault(x => x.Name == "selinay");

            inserted.Name = "Selinay";

            return _accountRepo.AddOrUpdate(inserted);
        }


        public List<TPersonDbEntity> GetAllTest() =>
             _accountRepo.AsQueryable(getDeleted: true).ToList();


        public TPersonDbEntity DeleteTest()
        {
            var dbResult = _accountRepo.AsQueryable().FirstOrDefault();
            return _accountRepo.Delete(dbResult.ID);
        }

        #endregion



        #region async methods
        public async Task<TPersonDbEntity> InsertAsyncTest() =>
           await _accountRepo.InsertAsync(
               new TPersonDbEntity
               {
                   Name = "Musa Async",
                   Surname = "Demir"
               });

        public async Task<TPersonDbEntity> UpdateAsyncTest()
        {
            var inserted = await _accountRepo.AsQueryable().FirstOrDefaultAsync();//not deleted first

            inserted.Name = "updated async";

            return await _accountRepo.UpdateAsync(inserted);
        }
        public async Task<TPersonDbEntity> AddOrUpdateAsync_AddTest() =>
            await _accountRepo.AddOrUpdateAsync(
                new TPersonDbEntity
                {
                    Name = "async ",
                    Surname = "AddOrUpdateAsync_AddTest"
                });

        public async Task<TPersonDbEntity> AddOrUpdateAsync_UpdateTest()
        {
            var inserted = await _accountRepo.AsQueryable().FirstOrDefaultAsync(x => x.Name.Contains("sync"));

            inserted.Name = "Async";
            inserted.Surname = "AddOrUpdateAsync_UpdateTest";

            return await _accountRepo.AddOrUpdateAsync(inserted);
        }

        public async Task<List<TPersonDbEntity>> GetAllAsyncTest() =>
           await _accountRepo.AsQueryable(getDeleted: true).ToListAsync();

        public async Task<TPersonDbEntity> DeleteAsyncTest()
        {
            var dbResult = await _accountRepo.AsQueryable().FirstOrDefaultAsync();
            return await _accountRepo.DeleteAsync(dbResult.ID);
        }

        #endregion



    }
}
