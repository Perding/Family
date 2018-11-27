using DapperData;
using Entity;
using Microsoft.Extensions.Configuration;
using Repository.IReposotiry;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Repository.Reposotiry
{
    public class M_menuRepository: IM_menuRepository
    {
        private DapperFactory _factory;
        private IDapper _dapper;
        public M_menuRepository(IConfiguration configuration) 
        {
            _factory = DapperFactory.GetInstance(configuration);
            _dapper = _factory.GetDapper();
        }
        public async Task<IEnumerable<M_menu>> Getlist()
        {
            return await _dapper.GetList<M_menu>("select * from dbo.M_menu where M_IsEnable=1  order by M_PartentID, M_sortid");
        }
        public async Task<IEnumerable<M_menu>> Getlist(string uid)
        {
            string sql = "";
            if (uid=="")
            {
                sql = "select * from M_menu where M_IsEnable=1 and M_Layer=1  order by M_PartentID,M_sortid";
            }
            else
            {
                sql = "select M_menu.* from dbo.Usermenu inner join M_menu on Usermenu.m_id=M_menu.M_ID where M_menu.M_IsEnable=1 and M_Layer=1 and u_id=@u_id order by   M_menu.M_PartentID, M_menu.M_sortid";
            }
            return await _dapper.GetList<M_menu>(sql,new { u_id=uid });
        }

        public async Task<IEnumerable<M_menu>> GetChildrelist(string uid)
        {
            string sql = "";
            if (uid=="")
            {
                sql = "select * from  M_menu  where M_IsEnable=1   and m_partentID='4E326D83-C673-4639-9589-5D06AD02D321' order by  M_PartentID,M_sortid";
            }
            else
            {
                sql = "select M_menu.* from dbo.Usermenu inner join M_menu on Usermenu.m_id=M_menu.M_ID where M_menu.M_IsEnable=1  and u_id=@u_id and Usermenu.m_partentID='4E326D83-C673-4639-9589-5D06AD02D321' order by   M_menu.M_PartentID, M_menu.M_sortid";
            }
            return await _dapper.GetList<M_menu>(sql, new { u_id = uid });
        }
    }
}
