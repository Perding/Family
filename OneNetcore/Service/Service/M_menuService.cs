using Entity;
using Repository.IReposotiry;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class M_menuService: IM_menuIService
    {
        private IM_menuRepository _MenuRepository;
        public M_menuService(IM_menuRepository m_MenuRepository)
        {
            _MenuRepository = m_MenuRepository;
        }
        public async Task<IEnumerable<M_menu>> Getlist()
        {
            return await _MenuRepository.Getlist();
        }

        public async Task<IEnumerable<M_menu>> Getlist(string uid)
        {
            return await _MenuRepository.Getlist(uid);
        }

        public async Task<IEnumerable<M_menu>> GetChildrelist(string uid)
        {
            return await _MenuRepository.GetChildrelist(uid);
        }
    }
}
