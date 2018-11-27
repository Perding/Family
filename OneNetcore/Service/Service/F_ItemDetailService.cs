using Entity;
using Repository.IReposotiry;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class F_ItemDetailService : IF_ItemDetailIService
    {
        private IF_ItemDetailRepository _repository;

        public F_ItemDetailService(IF_ItemDetailRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 判断编号是否存在
        /// </summary>
        /// <param name="F_ItemCode"></param>
        /// <returns></returns>
        public async Task<F_ItemDetail> IsTrue(F_ItemDetail F_ItemCode)
        {
            return await _repository.IsTrue(F_ItemCode);
        }

        public async Task<F_ItemDetail> IsTrue(string id)
        {
            return await _repository.IsTrue(id);
        }
        public async Task<bool> InserModel(F_ItemDetail model)
        {
            return await _repository.InserModel(model);
        }

        public async Task<IEnumerable<F_ItemDetail>> GetList(string id)
        {
            return await _repository.GetList(id);
        }
        public async Task<IEnumerable<F_ItemDetail>> GetList()
        {
            return await _repository.GetList();
        }
        public async Task<bool> UpdateModel(F_ItemDetail model)
        {
            return await _repository.UpdateModel(model);
        }

        public async Task<bool> DeleteModel(F_ItemDetail model)
        {
            return await _repository.DeleteModel(model);
        }
    }
}
