using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data.Common;
using System.Net.Http;
using Common;
using Entity;
namespace DapperData
{
    public class DapperBase : IDapper
    {
        private IDbConnection _conn;
        private string _connectionString;

        public DapperBase(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection IDbConnection
        {
            get
            {
                _conn = new SqlConnection(_connectionString);

                return _conn;
            }
        }

        public async Task<bool> GetListSql(Dictionary<object, string> dic)
        {
            using (var db = IDbConnection as DbConnection)
            {
                db.Open();
                IDbTransaction transaction = db.BeginTransaction();
                try
                {
                    int row = 0;
                    foreach (KeyValuePair<object, string> item in dic)
                    {
                        row += await db.ExecuteAsync(item.Value, item.Key, transaction, 0, CommandType.Text);
                    }
                    transaction.Commit();
                    if (row > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    LogHelp.Error("GetListSql" + ex.Message);
                    return false;
                }
            }
        }
        public async Task<T> Istrue<T>(string sql, object param = null)
        {
            using (var db = IDbConnection as DbConnection)
            {
                try
                {
                    dynamic b;
                    if (param == null)
                    {
                        b = await db.QueryFirstOrDefaultAsync<T>(sql);
                    }
                    else
                    {
                        b = await db.QueryFirstOrDefaultAsync<T>(sql, param);
                    }

                    //  T s = b as T;
                    return b;
                }
                catch (Exception ex)
                {
                    LogHelp.Error("Istrue" + ex.Message);
                    return default(T);
                }
            }
        }
        public async Task<IEnumerable<T>> GetList<T>(string sqlString, object param, CommandType? commandType = CommandType.Text, int? commandTimeout = 180)
        {
            using (var db = IDbConnection as DbConnection)
            {
                try
                {
                    IEnumerable<T> ts = null;
                    if (param == null)
                    {
                        ts = await db.QueryAsync<T>(sqlString);
                    }
                    else
                    {
                        ts = await db.QueryAsync<T>(sqlString, param);
                    }
                    return ts;

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        public async Task<T> GetSinger<T>(string sqlString, object param = null)
        {
            try
            {

                IEnumerable<T> ts;
                using (var db = IDbConnection as DbConnection)
                {
                    if (param == null)
                    {
                        ts = await db.QueryAsync<T>(sqlString);
                    }
                    else
                    {
                        ts = await db.QueryAsync<T>(sqlString, param);
                    }

                }

                return ts.AsList()[0];
            }
            catch (Exception ex)
            {
                LogHelp.Error("Istrue" + ex.Message);
                throw;
            }
        }
        public async Task<bool> Insert(string sqlString, object param = null, CommandType commandType = CommandType.Text, int? commandTimeOut = 5)
        {
            try
            {
                int row = 0;
                using (var db = IDbConnection as DbConnection)
                {
                    row = await db.ExecuteAsync(sqlString, param);
                }
                if (row > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelp.Error("Istrue" + ex.Message);
                throw;
            }

        }
        public async Task<bool> Update(string sqlString, object param, CommandType commandType = CommandType.Text, int? commandTimeOut = 5)
        {
            try
            {
                int row = 0;
                using (var db = IDbConnection as DbConnection)
                {
                    row = await db.ExecuteAsync(sqlString, param);
                }
                if (row > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelp.Error("Istrue" + ex.Message);
                throw;
            }
        }
        public async Task<bool> Delete(string sqlString, object param, CommandType commandType = CommandType.Text, int? commandTimeOut = 5)
        {
            try
            {
                int row = 0;
                using (var db = IDbConnection as DbConnection)
                {
                    row = await db.ExecuteAsync(sqlString, param);
                }
                if (row > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelp.Error("Istrue" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlString">存储过程名</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetProce<T>(string sqlString, object param = null)
        {
            IEnumerable<T> ts;

            using (var db = IDbConnection as DbConnection)
            {

                try
                {
                    if (param == null)
                    {
                        ts = await db.QueryAsync<T>(sqlString, null, null, 0, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        ts = await db.QueryAsync<T>(sqlString, param, null, 0, commandType: CommandType.StoredProcedure);
                    }

                    return ts;
                }
                catch (Exception ex)
                {
                    LogHelp.Error("Istrue" + ex.Message);
                    throw;
                }

            }

        }
        public async Task<Tuple<int, List<T>>> GetProcePage<T>(string sql, object param = null)
        {
            int count = 0;
            List<T> ts;
            using (var db = IDbConnection as DbConnection)
            {
                try
                {
                    var result = await db.QueryMultipleAsync(sql);
                    count = result.Read<int>().AsList()[0];
                    ts = result.Read<T>().AsList();
                }
                catch (Exception ex)
                {
                    LogHelp.Error("Istrue" + ex.Message);
                    throw;
                }
            }
            return new Tuple<int, List<T>>(count, ts);
        }
        public async Task<Tuple<List<T1>, List<T2>>> GetProcePageS<T1,T2>(string sql, object param = null)
        {
            List<T1> t1;
            List<T2> t2;
            using (var db = IDbConnection as DbConnection)
            {
                try
                {
                    var result = await db.QueryMultipleAsync(sql);
                    t1 = result.Read<T1>().AsList();
                    t2 = result.Read<T2>().AsList();
                }
                catch (Exception ex)
                {
                    LogHelp.Error("Istrue" + ex.Message);
                    throw;
                }
            }
            return new Tuple<List<T1>, List<T2>>(t1, t2);
        }
        public async Task<object> GetSql(string sql, object param = null)
        {
            try
            {
                dynamic ts = null;
                using (var db = IDbConnection as DbConnection)
                {
                    ts = await db.QueryAsync<AStudent, AStudentPay, AStudent>(sql, (AStudents, AStudentPays) =>
                    {
                        AStudents.Owers = AStudentPays;
                        return AStudents;
                    }, splitOn: "ID");

                }
                return ts;
            }
            catch (Exception ex)
            {
                LogHelp.Error(ex.Message);
            }
            return "";
        }
    }
}
