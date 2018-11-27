using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DapperData
{
    public interface IDapper
    {
        #region SELECT

        Task<object> GetSql(string sql, object param = null);
        Task<IEnumerable<T>> GetProce<T>(string sql, object param = null);
        Task<Tuple<int, List<T>>> GetProcePage<T>(string sql, object param = null);
        Task<Tuple<List<T1>, List<T2>>> GetProcePageS<T1, T2>(string sql, object param = null);
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlString"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetList<T>(string sqlString, object param=null , CommandType? commandType = CommandType.Text, int? commandTimeout = 180);
       Task<T> GetSinger<T>(string sqlString, object param);

        Task<bool> GetListSql(Dictionary<object,string> dic);
        #endregion

        #region INSERT
        /// <summary>
        /// 单条数据写入 动态模板模式/T
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        Task<bool> Insert(string sqlString, object param = null, CommandType commandType = CommandType.Text, int? commandTimeOut = 5);

        Task<T> Istrue<T>(string sql, object param=null);
        #endregion

        #region UPDATE
        /// <summary>
        /// 数据更新
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        Task<bool> Update(string sqlString, object param, CommandType commandType = CommandType.Text, int? commandTimeOut = 5);
        #endregion

        #region DELETE
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        Task<bool> Delete(string sqlString, object param, CommandType commandType = CommandType.Text, int? commandTimeOut = 5);
        #endregion
    }
}
