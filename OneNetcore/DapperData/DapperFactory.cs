using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
namespace DapperData
{
   public class DapperFactory
    {
        private DapperFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private static readonly object locker = new object();
        private static DapperFactory _instance;
        private IDapper _dapper;
        private IConfiguration _configuration;
        private readonly string CONNECTION_STRING = "ConnectionStrings";
        private readonly string STUDENT_CONNECTION_STRING = "DefaultConnection";
        public static DapperFactory GetInstance(IConfiguration configuration)
        {
            // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
            // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
            // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
            // 双重锁定只需要一句判断就可以了
            if (_instance==null)
            {
                lock (locker)
                {
                    if (_instance==null)
                    {
                     _instance = new DapperFactory(configuration);
                    }
                }
            }
            return _instance;
        }
        public IDapper GetDapper()
        {
            if (_dapper==null)
            {
                var connetionString = string.Empty;
                if (_configuration.GetSection(CONNECTION_STRING)!=null&&_configuration.GetSection(CONNECTION_STRING).GetSection(STUDENT_CONNECTION_STRING)!=null)
                {
                    connetionString = _configuration.GetSection(CONNECTION_STRING).GetSection(STUDENT_CONNECTION_STRING).Value;
                }
                _dapper = new DapperBase(connetionString);
            }
            return _dapper;
        }
    }
}
