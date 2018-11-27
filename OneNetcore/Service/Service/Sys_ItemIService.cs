using Entity;
using Repository.IReposotiry;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class Sys_ItemIService: ISys_ItemIService
    {
        private ISys_ItemRepository _repository;

        public Sys_ItemIService(ISys_ItemRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Sys_Item>> GetList()
        {
            return await _repository.GetList();
        }
        public async Task<IEnumerable<Sys_Item>> GetList(string id)
        {
            return await _repository.GetList(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<List<Node>> GetListNode(List<Sys_Item> list)
        {
            return await Task.Run(() =>
            {
                List<Node> nodes = new List<Node>();
                foreach (Sys_Item item in list)
                {
                    Node n = new Node();
                    n.id = item.F_ID;
                    if (item.F_ParentID=="0")
                    {
                        n.isParent = true;
                    }
                    else
                    {
                        if (item.F_Encode == "key1" || item.F_Encode == "key2" || item.F_Encode == "key3")
                        {
                            n.isParent = true;
                        }
                        else
                        {
                            n.isParent = false;
                        }  
                    }
                   
                    n.name = item.F_FullName;
                    n.pId = item.F_ParentID;
                    n.open = true;
                    if (item.F_Layer==3)
                    {
                       n.click = "SetlAYER('" + item.F_ID + "')";
                    }
                    else if (item.F_Encode == "0")
                    {
                        n.click = "SetlAYER('" + item.F_ID+"')";
                    }
                    else if (item.F_Encode == "1")
                    {
                        n.click = "SetlGuid('" + item.F_ID + "')";
                    }
                    else if (item.F_Encode == "key1" || item.F_Encode == "key2" || item.F_Encode == "key3")
                    {

                        n.click = "setNewOrgId('" + item.F_ID + "','"+item.F_Encode+"')";
                    }
                    else
                    {
                        n.click = "liebei('" + item.F_ID + "')";
                    }
                    nodes.Add(n);
                }
                return nodes;
            });
        }

        public async Task<bool> InserModel(Sys_Item model)
        {
            return await _repository.InserModel(model);
        }

        public async Task<Sys_Item> IsTrue(Sys_Item model)
        {
            return await _repository.IsTrue(model);
        }

        public async Task<bool> UpdateModel(Sys_Item model)
        {

            return await _repository.UpdateModel(model);
        }

        public async Task<bool> DeleteModel(Sys_Item model)
        {
            return await _repository.DeleteModel(model);
        }
    }

}
