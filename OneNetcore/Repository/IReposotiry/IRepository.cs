using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IReposotiry
{
    public interface IRepository<T>
    {
        /// <summary>
        /// 取所有学生
        /// </summary>
        /// <returns></returns>
       Task<IEnumerable<T>> GetList(string sql);

        /// <summary>
        /// 获取某学生信息
        /// </summary>
        /// <returns></returns>
        Task<T> GetStudent(string sql,T model);

        /// <summary>
        /// 写入学生信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<bool> Insert(string sql,T list);

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Task<bool> Update(string sql,T student);

        /// <summary>
        /// 删除学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(string sql,T id);
    }
}
