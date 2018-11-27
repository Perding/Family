using Entity;
using Repository.IReposotiry;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class UsermenuService: IUsermenuIService
    {
        private IUsermenuRepository _usermenuRepository;
        public UsermenuService(IUsermenuRepository usermenuRepository)
        {
            _usermenuRepository = usermenuRepository;
        }
        public async Task<IEnumerable<Usermenu>> Getlist(string uid)
        {
            return await _usermenuRepository.GetUsermenu(uid);
        }
    }
}
