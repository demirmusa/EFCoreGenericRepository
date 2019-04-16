using Example.Data.DbEntities;
using Example.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example.Business.Interfaces
{    
    public interface IPersonTestService<TPersonDbEntity> where TPersonDbEntity : IPersonDbEntity
    {
        #region sync methods 
        TPersonDbEntity InsertTest();
        TPersonDbEntity UpdateTest();
        TPersonDbEntity AddOrUpdate_AddTest();
        TPersonDbEntity AddOrUpdate_UpdateTest();
        List<TPersonDbEntity> GetAllTest();
        TPersonDbEntity DeleteTest();
        #endregion



        #region async methods
        Task<TPersonDbEntity> InsertAsyncTest();
        Task<TPersonDbEntity> UpdateAsyncTest();
        Task<TPersonDbEntity> AddOrUpdateAsync_AddTest();
        Task<TPersonDbEntity> AddOrUpdateAsync_UpdateTest();
        Task<List<TPersonDbEntity>> GetAllAsyncTest();
        Task<TPersonDbEntity> DeleteAsyncTest();
        #endregion
    }
}
